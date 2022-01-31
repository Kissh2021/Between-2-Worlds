using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterEvent : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onTriggerEnterEvent.Invoke();
        }
    }
}
