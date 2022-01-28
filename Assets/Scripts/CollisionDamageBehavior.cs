using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable[] components = collision.gameObject.GetComponents<IDamageable>();
        foreach(var component in components)
        {
            component.Hit();
        }
    }
}
