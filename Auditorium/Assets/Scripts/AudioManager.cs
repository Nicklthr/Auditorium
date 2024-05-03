using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[Serializable]
public class  MySlider
{
    public Slider s;
    public string paramNames;
}

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public MySlider[] sliders;

    public void Start()
    {
        //PlayerPrefs.DeleteAll();
        LoadVolume();
        
    }

    public void SetMasterVolume( float value )
    {
        float decibel = Mathf.Log10( value ) * 20f;
        audioMixer.SetFloat( "MasterVolume", decibel );

        SaveVolume("MasterVolume", value);

    }

    public void SetVFXVolume( float value )
    {
        float decibel = Mathf.Log10( value ) * 20f;
        audioMixer.SetFloat( "SFXVolume", decibel );

        SaveVolume("SFXVolume", value);
    } 

    public void SetMusicVolume(float value)
    {
        float decibel = Mathf.Log10( value ) * 20f;
        audioMixer.SetFloat( "MusicVolume", decibel );

        SaveVolume("MusicVolume", value);

    }

    public void SaveVolume( string volumeName, float value )
    {
        PlayerPrefs.SetFloat( volumeName, value);
    }

    // fonction switch pour voir si les volumes sont sauvegardés ou non et si oui les appliquer
    public void LoadVolume()
    {
        // Parcours de tous les sliders pour voir si les volumes sont sauvegardés

        foreach ( MySlider slider in sliders )
        {
            if ( PlayerPrefs.HasKey( slider.paramNames ) )
            {
                float value = PlayerPrefs.GetFloat( slider.paramNames );
                slider.s.value = value;

                switch ( slider.paramNames )
                {
                    case "MasterVolume":
                        SetMasterVolume( value );
                        break;
                    case "SFXVolume":
                        SetVFXVolume( value );
                        break;
                    case "MusicVolume":
                        SetMusicVolume( value );
                        break;
                }
            }
        }
    }
}
