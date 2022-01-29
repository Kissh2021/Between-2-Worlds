using System.Collections;
using UnityEngine;

public class Utils
{
    public static IEnumerator WaitForFrames(int frames)
    {
        while(frames > 0)
        {
            frames--;
            yield return new WaitForFixedUpdate();
        }
    }
}