using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReloader : MonoBehaviour
{
    bool audioResumed = false;

    public void ResumeAudio()
    {
        if (!audioResumed)
        {
            var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
            Debug.Log(result);
            result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
            Debug.Log(result);
            audioResumed = true;
        }
    }
    private void Awake()
    {
        ResumeAudio();
    }
}
