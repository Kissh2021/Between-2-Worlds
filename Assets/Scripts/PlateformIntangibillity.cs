using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Platform/Plateforme Intangibillity")]
public class PlateformIntangibillity : MonoBehaviour
{
    private Transform m_playerT;
    private Collider2D m_collider;
    
    void Start()
    {
        m_playerT = FindObjectOfType<PlayerBehaviour>().transform;
        if (!TryGetComponent<Collider2D>(out m_collider))
        {
            m_collider = GetComponentInChildren<Collider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Dot(Vector2.up, m_playerT.position - m_collider.transform.position) > 0)
        {
            m_collider.enabled = true;
        }
        else
        {
            m_collider.enabled = false;
        }
    }
}
