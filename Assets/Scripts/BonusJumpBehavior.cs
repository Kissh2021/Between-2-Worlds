using System.Collections.Generic;
using UnityEngine;

public class BonusJumpBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> _childrens = new List<GameObject>();
    private bool _active = true;
    private PlayerBehaviour _player;
    void Start()
    {
        _player = FindObjectOfType<PlayerBehaviour>();

        if (_player)
        {
            _player.groundedEvent.AddListener(() => active = true);
        }

        for (var i = 0; i < transform.childCount; i++)
        {
            _childrens.Add(transform.GetChild(i).gameObject);
        }
    }

    private bool active
    {
        get { return _active; }
        set
        {
            foreach (var child in _childrens)
            {
                child.SetActive(value);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<PlayerBehaviour>();

        if (_active && _player)
        {
            active = false;
            _player.AddBonusJump();
        }
    }
}
