using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*** Singleton ***/
    private static GameObject _instance;

    public static GameManager instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject("@GameManager");
                _instance.AddComponent<GameManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance.GetComponent<GameManager>();
        }
    }

    /*** Manager ***/

    public DimentionsManager dm;

    void Awake()
    {
        instance.dm = new DimentionsManager();
        instance.dm.warpEvent.AddListener(() =>
        {
            Debug.Log($"Dimention : {instance.dm.dimention}");
        }
        );
    }
    // Start is called before the first frame update
    void Start()
    {
        instance.dm.warpEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
    }

}
