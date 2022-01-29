using System.Collections;
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

    public int dimensionTransitionFrames = 10;

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
        StartCoroutine(warpCoroutine());
    }

    private IEnumerator warpCoroutine()
    {
        dm.transition = DimensionsManager.Transition.In;
        Debug.Log(dm.transition);
        yield return StartCoroutine(Utils.WaitForFrames(dimensionTransitionFrames));

        dm.warp();
        dm.transition = DimensionsManager.Transition.Out;
        Debug.Log(dm.transition);
        yield return StartCoroutine(Utils.WaitForFrames(dimensionTransitionFrames));

        dm.transition = null;
    }
}
