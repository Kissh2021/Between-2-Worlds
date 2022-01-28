using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*** Singleton ***/
    private static GameObject _instance;

    public static GameManager getInstance()
    {
        if (!_instance)
        {
            _instance = new GameObject("@GameManager");
            _instance.AddComponent<GameManager>();
            DontDestroyOnLoad(_instance);
        }
        return _instance.GetComponent<GameManager>();
    }

    /*** Manager ***/

    public DimentionsManager dm;

    public GameManager()
    {
        dm = new DimentionsManager();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
