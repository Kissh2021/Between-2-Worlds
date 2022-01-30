using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Interfaces;
using JetBrains.Annotations;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerStuckKiller : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeDie = 0.7f;

    private float m_timer = 0f;
    
    private bool m_sometingInPlayer = false;

    private PlayerBehaviour player;

    private bool urDed;

    private void Start()
    {
        player = GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        if (m_sometingInPlayer)
        {
            if (m_timer > timeBeforeDie && !urDed)
            {
                urDed = true;
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
        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, Vector2.up, 1.3f);
        Debug.DrawRay(player.transform.position, Vector2.up * 1.3f);

        if (hits.Any())
        {
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject != gameObject && !hit.collider.isTrigger)
                {
                    m_sometingInPlayer = true;
                }
                else
                {
                    m_sometingInPlayer = false;
                }
            } 
        }
    }
}
