using System.Collections.Generic;
using UnityEngine;

public static class Frequencies
{
    public const float tuning = 440f;

    public static Dictionary<KeyCode, float> Keys = new()
    {
        { KeyCode.Z, C4 },
        { KeyCode.S, Cs4 },
        { KeyCode.X, D4 },
        { KeyCode.D, Ds4 },
        { KeyCode.C, E4 },
        { KeyCode.V, F4 },
        { KeyCode.G, Fs4 },
        { KeyCode.B, G4 },
        { KeyCode.H, Gs4 },
        { KeyCode.N, A5 },
        { KeyCode.J, As5 },
        { KeyCode.M, B5 },
        { KeyCode.Comma, C5 },
        { KeyCode.L, Cs5 },
        { KeyCode.Period, D5 },
        { KeyCode.Semicolon, Ds5 },
        { KeyCode.Slash, E5 },

    };

    public static float A4  => tuning;
    public static float As4 => tuning * Mathf.Pow(1.0594631f, 1);
    public static float B4  => tuning * Mathf.Pow(1.0594631f, 2);
    public static float C4  => tuning * Mathf.Pow(1.0594631f, 3);
    public static float Cs4 => tuning * Mathf.Pow(1.0594631f, 4);
    public static float D4  => tuning * Mathf.Pow(1.0594631f, 5);
    public static float Ds4 => tuning * Mathf.Pow(1.0594631f, 6);
    public static float E4  => tuning * Mathf.Pow(1.0594631f, 7);
    public static float F4  => tuning * Mathf.Pow(1.0594631f, 8);
    public static float Fs4 => tuning * Mathf.Pow(1.0594631f, 9);
    public static float G4  => tuning * Mathf.Pow(1.0594631f, 10);
    public static float Gs4 => tuning * Mathf.Pow(1.0594631f, 11);
    public static float A5  => tuning * Mathf.Pow(1.0594631f, 12);
    public static float As5  => tuning * Mathf.Pow(1.0594631f, 13);
    public static float B5  => tuning * Mathf.Pow(1.0594631f, 14);
    public static float C5  => tuning * Mathf.Pow(1.0594631f, 15);
    public static float Cs5  => tuning * Mathf.Pow(1.0594631f, 16);
    public static float D5  => tuning * Mathf.Pow(1.0594631f, 17);
    public static float Ds5  => tuning * Mathf.Pow(1.0594631f, 18);
    public static float E5  => tuning * Mathf.Pow(1.0594631f, 19);
}
