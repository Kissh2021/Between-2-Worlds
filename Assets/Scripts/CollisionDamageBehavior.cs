using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageBehavior : MonoBehaviour
{
    private enum Type
    {
        Enter,
        Exit
    }

    [SerializeField]
    private Type type = Type.Enter;

    private bool trigger;

    private void Start()
    {
        trigger = GetComponent<Collider2D>().isTrigger;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == Type.Enter && !trigger)
            checkCollision(collision.collider);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (type == Type.Exit && !trigger)
            checkCollision(collision.collider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (type == Type.Enter && trigger)
            checkCollision(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (type == Type.Exit && trigger)
            checkCollision(collision);
    }

    private void checkCollision(Collider2D collision)
    {
        IDamageable[] components = collision.gameObject.GetComponents<IDamageable>();
        foreach (var component in components)
        {
            component.Hit();
        }
    }
}
