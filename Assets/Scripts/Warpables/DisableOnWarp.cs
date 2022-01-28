using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnWarp : MonoBehaviour, IWarpable
{
    [SerializeField]
    private DimentionsManager.Dimentions _dimention;
    public void Warp()
    {
        gameObject.SetActive(GameManager.instance.dm.dimention == _dimention);
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
