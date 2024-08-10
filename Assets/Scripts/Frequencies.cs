using System.Collections.Generic;
using UnityEngine;

public static class Frequencies
{
    public const float tuning = 440f;

    //public static Dictionary<KeyCode, float> Keys = new()
    //{
    //    { KeyCode.Z, C5 },
    //    { KeyCode.S, Cs5 },
    //    { KeyCode.X, D5 },
    //    { KeyCode.D, Ds5 },
    //    { KeyCode.C, E5 },
    //    { KeyCode.V, F5 },
    //    { KeyCode.G, Fs5 },
    //    { KeyCode.B, G5 },
    //    { KeyCode.H, Gs5 },
    //    { KeyCode.N, A5 },
    //    { KeyCode.J, As5 },
    //    { KeyCode.M, B5 },
    //    { KeyCode.Comma, C5 },
    //    { KeyCode.L, Cs5 },
    //    { KeyCode.Period, D5 },
    //    { KeyCode.Semicolon, Ds5 },
    //    { KeyCode.Slash, E5 },
    //};

    public static Dictionary<Note, float> Notes = new()
    {
        { Note.A, A4 },
        { Note.As, As4 },
        { Note.B, B4 },
        { Note.C, C5 },
        { Note.Cs, Cs5 },
        { Note.D, D5 },
        { Note.Ds, Ds5 },
        { Note.E, E5 },
        { Note.F, F5 },
        { Note.Fs, Fs5 },
        { Note.G, G5 },
        { Note.Gs, Gs5 }
    };

    public static float A4  => tuning;
    public static float As4 => tuning * Mathf.Pow(1.0594631f, 1);
    public static float B4  => tuning * Mathf.Pow(1.0594631f, 2);
    public static float C5  => tuning * Mathf.Pow(1.0594631f, 3);
    public static float Cs5 => tuning * Mathf.Pow(1.0594631f, 4);
    public static float D5  => tuning * Mathf.Pow(1.0594631f, 5);
    public static float Ds5 => tuning * Mathf.Pow(1.0594631f, 6);
    public static float E5 =>  tuning * Mathf.Pow(1.0594631f, 7);
    public static float F5  => tuning * Mathf.Pow(1.0594631f, 8);
    public static float Fs5 => tuning * Mathf.Pow(1.0594631f, 9);
    public static float G5 =>  tuning * Mathf.Pow(1.0594631f, 10);
    public static float Gs5 => tuning * Mathf.Pow(1.0594631f, 11);
}

public enum Note
{
    A, As, B, C, Cs, D, Ds, E, F, Fs, G, Gs
}