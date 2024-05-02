using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnWinGame = new UnityEvent();
    public float winDuration = 2.0f;

    private GameObject[] _musicBoxes;
    private AudioSource[] _audioSources;
    private float _timer = 0;
    private bool _allMaxVolume = false;

    private bool _IsWin = false;

    void Start()
    {
        _musicBoxes = GameObject.FindGameObjectsWithTag( "MusicBox" );
        _audioSources = new AudioSource[_musicBoxes.Length];

        for ( int i = 0; i < _musicBoxes.Length; i++ )
        {
            _audioSources[i] = _musicBoxes[i].GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if ( _IsWin )
        {
            return;
        }

        CheckAllMaxVolume();

        if ( _allMaxVolume )
        {
            _timer += Time.deltaTime;
            Debug.Log( _timer );
        }
        else
        {
            _timer = 0;
        }

        if ( _timer >= winDuration )
        {
            OnWinGame.Invoke();
        }
    }


    private void CheckAllMaxVolume()
    {
        foreach ( AudioSource audioSource in _audioSources)
        {
            if (audioSource.volume < 0.98f )
            {
                Debug.Log( "Pas au Volume max" );

                _allMaxVolume = false;
                break;
            }
            else
            {
                _allMaxVolume = true;
            }
        }
    }

    public void SetTimeScale( float timeScale )
    {
        Time.timeScale = timeScale;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}