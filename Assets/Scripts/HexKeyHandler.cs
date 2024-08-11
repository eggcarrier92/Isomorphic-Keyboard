using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HexKeyHandler : MonoBehaviour
{
    [SerializeField] private WaveGenerator generator;
    [SerializeField] private Note note;
    [SerializeField] private int octave = 4;

    private PolygonCollider2D _collider;
    private bool wasTouching = false;

    private void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        foreach (var touch in Input.touches)
        {
            if (!_collider.OverlapPoint(Camera.main.ScreenToWorldPoint(touch.position)))
            {
                if (wasTouching && !Input.touches.ToList().Exists(x => _collider.OverlapPoint(Camera.main.ScreenToWorldPoint(x.position))))
                {
                    wasTouching = false;
                    generator.StopPlaying(Frequencies.Notes[note] * Mathf.Pow(2, octave - 4));
                }
                continue;
                
            }
            if (touch.phase == TouchPhase.Began)
            {
                generator.StartPlaying(Frequencies.Notes[note] * Mathf.Pow(2, octave - 4));
                wasTouching = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                generator.StopPlaying(Frequencies.Notes[note] * Mathf.Pow(2, octave - 4));
                wasTouching = false;
            }
        }
    }

    //private void OnMouseDown()
    //{
    //    generator.StartPlaying(Frequencies.Notes[note] * Mathf.Pow(2, octave - 4));
    //}

    //private void OnMouseUp()
    //{
    //    generator.StopPlaying(Frequencies.Notes[note] * Mathf.Pow(2, octave - 4));
    //}
}
