using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnWarp : MonoBehaviour, IWarpable
{
    [SerializeField]
    private DimensionsManager.Dimensions _dimension;
    [SerializeField]
    private bool destroyParticles = true;

    public void Warp()
    {
        gameObject.SetActive(GameManager.instance.dm.dimension == _dimension);
        if(destroyParticles)
        {
            foreach (var particle in GetComponentsInChildren<ParticleSystem>())
            {
                Destroy(particle.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.dm.warpEvent.AddListener(Warp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
