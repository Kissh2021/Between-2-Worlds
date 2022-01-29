using Cinemachine;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{   
    [SerializeField]
    private RespawnFlagBehavior _respawnFlag;

    private PlayerBehaviour _player;

    public RespawnFlagBehavior respawnFlag
    {
        get { return _respawnFlag; }
        set
        {
            _respawnFlag = value;
        }
    }

    public void respawn()
    {
        _player.transform.position = _respawnFlag.transform.position;
        _player.gameObject.SetActive(true);
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerBehaviour>();

        if (!_player)
        {
            Debug.LogError("Respawn manager need a PlayerBehaviour");
            return;
        }

        if (!_respawnFlag)
        {
            GameObject go = new GameObject("SpawnFlag");
            go.transform.position = _player.transform.position;
            _respawnFlag = go.AddComponent<RespawnFlagBehavior>();
        }
           
        _player.dieEvent.AddListener(respawn);
    }
}
