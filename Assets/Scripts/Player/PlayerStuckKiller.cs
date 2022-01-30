using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerStuckKiller : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeDie = 0.7f;

    private float m_timer = 0f;
    
    private bool m_sometingInPlayer = false;

    private PlayerBehaviour player;

    private void Start()
    {
        player = GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        if (m_sometingInPlayer)
        {
            if (m_timer > timeBeforeDie)
            {
                (player as IDamageable).Hit();
            }
            m_timer += Time.deltaTime;
        }
        else
        {
            m_timer = 0f;
        }

    }

    private void FixedUpdate()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, Vector2.up, 1.7f);

        if (hits.Any(x => x.collider.gameObject != player.gameObject))
        {
            m_sometingInPlayer = hits.Any(x => !x.collider.isTrigger);
        }
        else
        {
            m_sometingInPlayer = false;
        }
    }
}
