using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGravityControlBehavior : MonoBehaviour
{
    private IDictionary<Rigidbody2D, float> _bodies = new Dictionary<Rigidbody2D, float>();

    [SerializeField]
    private float gravityScale = 0f;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D[] bodies = collision.gameObject.GetComponents<Rigidbody2D>();
        foreach(var body in bodies)
        {
            if(!_bodies.ContainsKey(body))
            {
                _bodies.Add(body, body.gravityScale);
                body.gravityScale = gravityScale;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D[] bodies = collision.gameObject.GetComponents<Rigidbody2D>();
        foreach (var body in bodies)
        {
            if(_bodies.ContainsKey(body))
            {
                body.gravityScale = _bodies[body];
                _bodies.Remove(body);
            }
        }
    }
}
