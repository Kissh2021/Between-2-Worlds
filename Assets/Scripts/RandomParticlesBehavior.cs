using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomParticlesBehavior : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> particles = new List<GameObject>();
    [SerializeField]
    private float minInterval = 2f, maxInterval = 3f;

    private SpriteRenderer _sprite;

    private bool _canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_canSpawn)
        {
            _canSpawn = false;
            StartCoroutine(spawnParticle());
        }
    }

    private void OnEnable()
    {
        _canSpawn = true;
    }

    private IEnumerator spawnParticle()
    {
        yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        GameObject particle = Instantiate(particles[Random.Range(0, particles.Count - 1)], transform);
        Vector2 position = new Vector2(
            Random.Range(_sprite.bounds.min.x, _sprite.bounds.max.x),
            Random.Range(_sprite.bounds.min.y, _sprite.bounds.max.y)
        );
        particle.transform.position = position;
        _canSpawn = true;
    }
}
