using FMODUnity;
using System.Collections;
using UnityEngine;

public class MusicTransitionOnWarp : MonoBehaviour
{
    private float parameter = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.dm.transitionEvent.AddListener(() =>
        {
            Debug.Log("Start music transition");
            StartCoroutine(transitionCoroutine());
        });
    }

    private IEnumerator transitionCoroutine()
    {
        int direction = GameManager.instance.dm.dimension == DimensionsManager.Dimensions.Afthlea ? 1 : -1;

        Debug.Log($"From {GameManager.instance.dm.dimension}");

        while (GameManager.instance.dm.transition != null)
        {
            parameter += direction * (Time.deltaTime / GameManager.instance.dimensionTransitionDuration);
            parameter = Mathf.Clamp01(parameter);
            RuntimeManager.StudioSystem.setParameterByName("Dimension", parameter);
            yield return null;
        }
    }
}
