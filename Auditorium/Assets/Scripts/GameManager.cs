using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnWinGame = new UnityEvent();
    public float winDuration = 2.0f;

    public float fadeDuration = 1.5f;

    public CanvasGroup fadeCanvas;
    public string winSceneName = "WinScreen";

    public bool nextLevelUnlocked = false;

    public enum NextLevel
    {
        LEVEL_1,
        LEVEL_2,
        LEVEL_3,
        LEVEL_4
    }
    public NextLevel nextLevel;

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
            //Debug.Log( _timer );
        }
        else
        {
            _timer = 0;
        }

        if ( _timer >= winDuration )
        {
            OnWinGame.Invoke();
            _IsWin = true;
        }
    }


    private void CheckAllMaxVolume()
    {
        foreach ( AudioSource audioSource in _audioSources)
        {
            if (audioSource.volume < 0.9f )
            {
                //Debug.Log( "Pas au Volume max" );

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


    public void LoadScene( string sceneName )
    {
        StartCoroutine( FadeCanvasAndLoadScene( sceneName ) );
    }

    // coroutine pour faire le fade canavs puis changer de scene après 1 seconde
    public IEnumerator FadeCanvasAndLoadScene( string sceneName )
    {
        yield return FadeCanvas( true );
        yield return new WaitForSeconds( 1.0f );

        UnityEngine.SceneManagement.SceneManager.LoadScene( sceneName );
    }

    #region Fade Out Gameplay Elements Functions

    public void FadeOutGameplayElements()
    {
        GameObject[] centerZones = GameObject.FindGameObjectsWithTag( "CenterZone" );

        StartCoroutine( FadeOutMusicBoxesCoroutine() );
        StartCoroutine( FadeOutAudioSources() );
        StartCoroutine( FadeOutCenterZonesCoroutine( centerZones ) );

        // récupéer tous les composant CircleShape de toute les GameObjects avec le tag "ForceBox" et lancer la coroutine FadeCircle
       CircleShape[] circleShapes = GameObject.FindObjectsOfType<CircleShape>();

        foreach ( CircleShape circleShape in circleShapes )
        {
            circleShape.StartFadeCircle();
        }

        if( nextLevelUnlocked )
        {
            StartCoroutine( FadeCanvasAndLoadScene( nextLevel.ToString() ) );
        }else
        {
            StartCoroutine( FadeCanvasAndLoadScene( winSceneName ) );
        }
    }
    

    // coroutine pour baisser progressivement l'alpha des sprites des gameObject dans le tableau centerZones sur 1,5 secondes
    private IEnumerator FadeOutCenterZonesCoroutine(GameObject[] centerZones )
    {
        float duration = 1.5f;
        float timer = 0;

        while ( timer < duration )
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;

            foreach ( GameObject centerZone in centerZones )
            {
                SpriteRenderer spriteRenderer = centerZone.GetComponent<SpriteRenderer>();

                Color color = spriteRenderer.color;
                color.a = 1 - ratio;
                spriteRenderer.color = color;

                if ( color.a <= 0 )
                {
                    centerZone.SetActive( false );
                }
            }

            yield return null;
        }
    }

    // fonction IEnumerator pour baisser progressivement l'alpha de tous les musicBoxes et les désactiver à la fin sur 1,5 secondes
    private IEnumerator FadeOutMusicBoxesCoroutine()
    {
        float duration = 1.5f;
        float timer = 0;

        while ( timer < duration )
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;

            foreach ( GameObject musicBox in _musicBoxes )
            {
                //récupérer tous les SpriteRenderer enfants de musicBox
                SpriteRenderer[] spriteRenderers = musicBox.GetComponentsInChildren<SpriteRenderer>();

                foreach ( SpriteRenderer spriteRenderer in spriteRenderers )
                {
                    Color color = spriteRenderer.color;
                    color.a = 1 - ratio;
                    spriteRenderer.color = color;

                    // quand a = 0, désactiver le SpriteRenderer
                    if ( color.a <= 0 )
                    {
                        spriteRenderer.gameObject.SetActive( false );
                    }
                }
            }

            yield return null;
        }

        // fait une pose de 1 seconde
        yield return new WaitForSeconds( 1.0f );
    }

    // fonction coroutine pour baisser progressivement le volume de tous les audioSources sur 1,5 secondes
    private IEnumerator FadeOutAudioSources()
    {
        float duration = 1.5f;
        float timer = 0;

        while ( timer < duration )
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;

            foreach ( AudioSource audioSource in _audioSources )
            {
                audioSource.volume = 1 - ratio;

                // quand le volume = 0, désactiver l'audioSource
                if ( audioSource.volume <= 0 )
                {
                    audioSource.gameObject.SetActive( false );
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds( 1.0f );
    }

    #endregion

    // fonction coroutine pour baisser ou augmenter progressivement l'alpha du CanvasGroup fadeCanvas sur 1 seconde
    public IEnumerator FadeCanvas( bool fadeIn )
    {
        // vérifier que le fadeCanvas est actif
        if ( !fadeCanvas.gameObject.activeInHierarchy )
        {
            fadeCanvas.gameObject.SetActive( true );
        }
        
        float timer = 0;

        yield return new WaitForSeconds( fadeDuration );

        while ( timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float ratio = timer / fadeDuration;

            if ( fadeIn )
            {
                fadeCanvas.alpha = ratio;
            }
            else
            {
                fadeCanvas.alpha = 1 - ratio;
            }

            yield return null;
        }

    }
}