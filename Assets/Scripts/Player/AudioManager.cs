using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private StudioEventEmitter death;
    [SerializeField]
    private StudioEventEmitter respawn;

    private PlayerBehaviour m_player;
    
    void Start()
    {
        m_player = GetComponent<PlayerBehaviour>();
        m_player.beforeDieEvent.AddListener(death.Play);
        m_player.dieEvent.AddListener(respawn.Play);
    }
    
}
