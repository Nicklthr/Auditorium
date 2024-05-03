using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Spwaner : MonoBehaviour
{
    //public GameObject particlePrefab;
    public float spawnRate = 1.0f;
    public float spawnRadius = 5.0f;
    public float particleSpeed = 5.0f;

    public bool changeColor = false;
    public Gradient gradient = new Gradient();

    private float _chrono = 0;

    void Start()
    {
        
    }
    void Update()
    {
        _chrono += Time.deltaTime;

        if ( _chrono >= spawnRate )
        {
            SpawnParticle();
            _chrono = 0;
        }
    }

    void SpawnParticle()
    {
          Vector2 spawnPos = ( Vector2 )transform.position + Random.insideUnitCircle * spawnRadius;
          GameObject particle = ObjectPool.Get();

          //GameObject particle = Instantiate(particlePrefab, spawnPos, Quaternion.identity);

          particle.SetActive( true );


          StartCoroutine( DelayEmitting( particle, spawnPos ) );

    }

    IEnumerator DelayEmitting( GameObject particle, Vector2 spawnPos )
    {
        particle.transform.position = spawnPos;

        RandomizeParticleVisuals( particle );
        RandomizeParticleSize( particle );

        Rigidbody2D particleRb = particle.GetComponent<Rigidbody2D>();
        particleRb.velocity = transform.right * particleSpeed;

        yield return new WaitForEndOfFrame();

        if (changeColor)
        {
            RandomizeParticleTrailColor( particle, gradient );
        }

        particle.GetComponent<TrailRenderer>().emitting = true;
    }

    void RandomizeParticleVisuals( GameObject particle )
    {
        if (particle == null)
        {
            return;
        }

        Transform note1 = particle.transform.Find( "note_1" );
        Transform note2 = particle.transform.Find( "note_2" );

        if (note1 == null || note2 == null)
        {
            return;
        }

        if (Random.Range(0, 2) == 0)
        {
            note1.gameObject.SetActive( true );
            note2.gameObject.SetActive( false );
        }
        else
        {
            note1.gameObject.SetActive( false );
            note2.gameObject.SetActive( true );
        }
    }

    // fonction pour changer la taille du gameObject note_1 ou note_2 de manière aléatoire 0.49619f, 1.0f 
    void RandomizeParticleSize( GameObject particle )
    {
        if (particle == null)
        {
            return;
        }

        Transform note1 = particle.transform.Find( "note_1" );
        Transform note2 = particle.transform.Find( "note_2" );

        if (note1 == null || note2 == null)
        {
            return;
        }

        float randomSize = Random.Range( 0.49619f, 1.0f );

        if (note1.gameObject.activeSelf)
        {
            note1.localScale = new Vector3( randomSize, randomSize, 1 );
        }
        else
        {
            note2.localScale = new Vector3( randomSize, randomSize, 1 );
        }
    }

    // fonction pour changer les deux couleur du dégradé du trailRenderer de la particle
    void RandomizeParticleTrailColor( GameObject particle, Gradient gradient)
    {
        if (particle == null)
        {
            return;
        }

        TrailRenderer trailRenderer = particle.GetComponent<TrailRenderer>();

        if (trailRenderer == null)
        {
            return;
        }

        trailRenderer.colorGradient = gradient;
    }

}