using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SquareWaveGenerator : MonoBehaviour
{
    [SerializeField] private float frequency = 440f;          // Frequency of the square wave in Hz
    [SerializeField] private int sampleRate = 48000;          // Sample rate (samples per second)
    [SerializeField] private float decay = .3f;               // Duration of the fade-out in seconds
    [SerializeField] private int octave = 0;

    private float phase;                    // Tracks the phase of the square wave
    private float amplitude = 1.0f;         // Amplitude of the wave (for fade-out)
    private bool isPlaying = false;         // Tracks if the sound should be playing

    private float fadeElapsed = 0f;         // Tracks the elapsed time for fading

    private void Update()
    {
        if (Input.anyKey)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    isPlaying = true;
                    fadeElapsed = 0f;
                    amplitude = 1.0f;
                    frequency = Mathf.Pow(2, octave) * (Frequencies.Keys.ContainsKey(keyCode) ? Frequencies.Keys[keyCode] : Frequencies.tuning);
                }
            }
        }

        // If playing, increment the fade elapsed time
        if (isPlaying)
        {
            fadeElapsed += Time.deltaTime;

            // Once the fade duration is over, stop playing
            if (fadeElapsed >= decay)
            {
                fadeElapsed = decay;
                isPlaying = false;
            }
        }
    }

    

    private void OnAudioFilterRead(float[] data, int channels)
    {
        float increment = frequency * 2f * Mathf.PI / sampleRate;
        int maxHarmonics = Mathf.FloorToInt(sampleRate / (2 * frequency)); // Number of harmonics to include

        for (int i = 0; i < data.Length; i += channels)
        {
            if (!isPlaying)
            {
                // Fill buffer with silence if not playing
                for (int channel = 0; channel < channels; channel++)
                {
                    data[i + channel] = 0f;
                }
                continue;
            }

            // Reset the sample value
            float sample = 0f;

            // Generate the square wave using a limited number of harmonics
            for (int n = 1; n <= maxHarmonics; n += 2)
            {
                sample += (1f / n) * Mathf.Sin(n * phase);
            }

            // Scale the sample by the amplitude and the factor for square wave
            amplitude = 1f - fadeElapsed / decay;
            sample *= amplitude * (4f / Mathf.PI);

            // Apply the sample to all channels
            for (int channel = 0; channel < channels; channel++)
            {
                data[i + channel] = sample;
            }

            // Increment the phase and wrap around to keep it in the range [0, 2π]
            phase += increment;
            if (phase > 2f * Mathf.PI)
            {
                phase -= 2f * Mathf.PI;
            }
        }
    }
}
