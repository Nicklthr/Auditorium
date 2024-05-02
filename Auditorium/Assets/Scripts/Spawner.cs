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

        RandomizeParticleVisuals( particle );

        Rigidbody2D particleRb = particle.GetComponent<Rigidbody2D>();
        particleRb.velocity = transform.right * particleSpeed;
    }

    void RandomizeParticleVisuals(GameObject particle)
    {
        if (particle == null)
        {
            return;
        }

        Transform note1 = particle.transform.Find("note_1");
        Transform note2 = particle.transform.Find("note_2");

        if (note1 == null || note2 == null)
        {
            return;
        }

        if (Random.Range(0, 2) == 0)
        {
            note1.gameObject.SetActive(true);
            note2.gameObject.SetActive(false);
        }
        else
        {
            note1.gameObject.SetActive(false);
            note2.gameObject.SetActive(true);
        }
    }

}