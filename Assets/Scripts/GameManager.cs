using System.Collections;
using FMODUnity;
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

    public DimensionsManager dm;

    public float dimensionTransitionDuration = 0.2f;

    void Awake()
    {
        instance.dm = new DimensionsManager();
        instance.dm.warpEvent.AddListener(() =>
            {
                Debug.Log($"Dimension : {instance.dm.dimension}");
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

    public void warp()
    {
        if(dm.transition == null)
        {
            StartCoroutine(warpCoroutine());
        }
    }

    private IEnumerator warpCoroutine()
    {
        var sfx = gameObject.AddComponent<StudioEventEmitter>();
        dm.transition = DimensionsManager.Transition.In;
        dm.transitionEvent.Invoke();
        Debug.Log(dm.transition);
        yield return new WaitForSeconds(dimensionTransitionDuration / 2); 

        dm.warp();
        dm.transition = DimensionsManager.Transition.Out;
        Debug.Log(dm.transition);
        yield return new WaitForSeconds(dimensionTransitionDuration / 2);
        
        dm.transition = null;
    }
}
