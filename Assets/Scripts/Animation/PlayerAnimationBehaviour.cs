using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBehaviour))]
public class PlayerAnimationBehaviour : MonoBehaviour
{
    private Animator animator;
    private PlayerBehaviour pb;
    
    
    void Start()
    {
        pb = GetComponent<PlayerBehaviour>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
