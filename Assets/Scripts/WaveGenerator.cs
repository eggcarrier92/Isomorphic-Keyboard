using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WaveGenerator : MonoBehaviour
{
    //[SerializeField] private float frequency = 440f;          // Frequency of the square wave in Hz
    [SerializeField] private int sampleRate = 48000;          // Sample rate (samples per second)
    [SerializeField] private float decay = .3f;               // Duration of the fade-out in seconds
    [SerializeField] private float attack = .05f;
    [SerializeField] private int octave = 0;

    public void StartPlaying(float frequency)
    {
        // if the note is played again while it's still fading we reset its fading instead of creating a new one
        if (activeNotes.Exists(x => x.Frequency == frequency))
        {
            Note note = activeNotes.Find(x => x.Frequency == frequency);
            note.Fading = false;
            note.FadeSamplesRemaining = Mathf.CeilToInt(sampleRate * attack);
        }
        else
        {
            Note note = new(frequency);
            activeNotes.Add(note);
            note.FadeSamplesRemaining = Mathf.CeilToInt(sampleRate * attack);
        }
    }

    public void StopPlaying(float frequency)
    {
        if (activeNotes.Exists(x => x.Frequency == frequency))
        {
            Note note = activeNotes.Find(x => x.Frequency == frequency);
            float fadeStartPos = 1f;
            if (note.FadeSamplesRemaining > 0)
            {
                fadeStartPos = 1f - note.FadeSamplesRemaining / (sampleRate * attack);
            }
            Debug.Log(fadeStartPos);
            note.Fading = true;
            note.FadeSamplesRemaining = Mathf.CeilToInt(fadeStartPos * sampleRate * decay);
        }
    }

    private class Note
    {
        public float Frequency { get; }
        public float Amplitude { get; set; }
        public float Phase { get; set; }
        public bool Fading { get; set; }
        public int FadeSamplesRemaining { get; set; }
        public Note(float frequency)
        {
            Frequency = frequency;
            Amplitude = 1f;
            Fading = false;
        }
    }

    private readonly List<Note> activeNotes = new();

    private void Awake()
    {
        Application.targetFrameRate = -1;
    }

    private void Update()
    {

        for (int i = 0; i < activeNotes.Count; i++)
        {
            // Once the fade duration is over, stop playing
            if (activeNotes[i].Fading && activeNotes[i].FadeSamplesRemaining <= 0)
            {
                activeNotes.Remove(activeNotes[i]);
                i--;
            }
        }
    }

    

    private void OnAudioFilterRead(float[] data, int channels)
    {
        try
        {
            foreach (var note in activeNotes)
                GenerateWave(data, channels, note);
        }
        catch (InvalidOperationException)
        {
            foreach (var note in activeNotes)
                GenerateWave(data, channels, note);
        }
    }

    private void GenerateWave(float[] data, int channels, Note note)
    {
        int maxHarmonics = Mathf.FloorToInt(sampleRate / (2 * note.Frequency)); // Number of harmonics to include
        float increment = note.Frequency * 2f * Mathf.PI / sampleRate;
        for (int i = 0; i < data.Length; i += channels)
        {
            // Reset the sample value
            float sample = 0f;

            // Generate the square wave using a limited number of harmonics
            for (int n = 1; n <= maxHarmonics; n += 2)
            {
                sample += (1f / n) * Mathf.Sin(n * note.Phase);
            }

            // Scale the sample by the amplitude and the factor for square wave
            if (note.Fading)
            {
                note.Amplitude = note.FadeSamplesRemaining / (decay * sampleRate);
                note.FadeSamplesRemaining--;
            }
            else if (note.FadeSamplesRemaining > 0)
            {
                note.Amplitude = 1f - note.FadeSamplesRemaining / (attack * sampleRate);
                note.FadeSamplesRemaining--;
            }
            else
                note.Amplitude = 1f;
            sample *= 0.2f * note.Amplitude * (4f / Mathf.PI);

            // Apply the sample to all channels
            for (int channel = 0; channel < channels; channel++)
            {
                data[i + channel] += sample;
            }

            // Increment the phase and wrap around to keep it in the range [0, 2π]
            note.Phase += increment;
            if (note.Phase > 2f * Mathf.PI)
            {
                note.Phase -= 2f * Mathf.PI;
            }
        }
    }
}
