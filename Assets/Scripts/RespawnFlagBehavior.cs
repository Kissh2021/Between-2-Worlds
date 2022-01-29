using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFlagBehavior : MonoBehaviour
{
    private RespawnManager _rm;
    private ParticleSystem _particles;

    [SerializeField]
    private Sprite flaggedSprite;
    [SerializeField]
    private int OrderInLayer = 15;

    private void Start()
    {
        _rm = FindObjectOfType<RespawnManager>();
        _particles = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBehaviour>()) {
            _rm.respawnFlag = this;
            GetComponent<Collider2D>().enabled = false;
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

            if(!sr)
            {
                sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sortingOrder = 15;
            }

            sr.sprite = flaggedSprite;
            _particles.Play(); 
        }
    }
}
