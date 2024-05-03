using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ColorChanger : MonoBehaviour
{
    public Gradient[] gradients = new Gradient[3];
    public float spawnRate = 5.0f;

    private Gradient _gradient;
    private float _chrono = 0;

    private void Start()
    {
        _gradient = gradients[0];
    }

    private void Update()
    {       
        _chrono += Time.deltaTime;

        if ( _chrono >= spawnRate )
        {
            _chrono = 0;
            _gradient = gradients[Random.Range( 0, gradients.Length )];
        }

    }

    private void OnTriggerEnter2D( Collider2D collision )
    {

        GameObject particle = collision.gameObject;

        if (particle == null)
        {
            return;
        }

        RandomizeParticleTrailColor( particle, _gradient );
    }


    void RandomizeParticleTrailColor( GameObject particle, Gradient gradient )
    {
        if (particle == null)
        {
            return;
        }

        TrailRenderer trailRenderer = particle.GetComponent<TrailRenderer>();

        if ( trailRenderer == null )
        {
            return;
        }

        trailRenderer.colorGradient = gradient;
    }

}
