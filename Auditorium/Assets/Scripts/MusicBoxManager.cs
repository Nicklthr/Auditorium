using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxManager : MonoBehaviour
{
    public SpriteRenderer[] bars;
    public AudioSource audioSource;

    public Color activeColor = Color.white;
    public Color inactiveColor = Color.gray;

    public float volumeLost = 0.1f;
    public float volumeLostRate = 0.01f;

    public float volumeUp = 0.1f;

    private float _chrono = 0;

    void Start()
    {
            audioSource.volume = 0;
            BarVolumeUpdate(audioSource.volume);

    }

    void Update()
    {
        
        _chrono += Time.deltaTime;
        
        if ( _chrono >= volumeLostRate )
        {
            audioSource.volume -= volumeLost * Time.deltaTime;
        }
        else
        {
            _chrono += Time.deltaTime;
        }

        BarVolumeUpdate(audioSource.volume);
    }

    private void BarVolumeUpdate( float volume )
    {
        if ( audioSource != null && bars.Length > 0 )
        {
            for  (int i = 0; i < bars.Length; i++ )
            {
                if ( volume > i / ( float )bars.Length )
                {
                    bars[i].color = activeColor;
                }
                else
                {
                    bars[i].color = inactiveColor;
                }
            }
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.CompareTag("Particle"))
        {
            audioSource.volume += volumeUp;
            BarVolumeUpdate(audioSource.volume);
        }
    }
}