using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Spwaner : MonoBehaviour
{
    public GameObject particlePrefab;
    public float spawnRate = 1.0f;
    public float spawnRadius = 5.0f;
    public float particleSpeed = 5.0f;

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
        GameObject particle = Instantiate( particlePrefab, spawnPos, Quaternion.identity );

        Rigidbody2D particleRb = particle.GetComponent<Rigidbody2D>();
        particleRb.velocity = transform.right * particleSpeed;
    }
}