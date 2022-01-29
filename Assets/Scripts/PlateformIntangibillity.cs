using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformIntangibillity : MonoBehaviour
{
    private Transform m_playerT;
    private Collider2D m_collider;
    
    void Start()
    {
        m_playerT = FindObjectOfType<PlayerBehaviour>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
