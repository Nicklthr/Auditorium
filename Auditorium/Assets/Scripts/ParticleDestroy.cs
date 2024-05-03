using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private Rigidbody2D _rb;
    private TrailRenderer _tr;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<TrailRenderer>();
        
    }

    void Update()
    {
        if ( _rb.velocity.magnitude < 0.1 )
        {
            ObjectPool.Disable( gameObject );

            _tr.emitting = false;
            _rb.velocity = Vector3.zero;

        }
    }
}
