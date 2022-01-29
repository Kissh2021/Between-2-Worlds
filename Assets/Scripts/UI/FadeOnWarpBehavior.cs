using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOnWarpBehavior : MonoBehaviour
{
    private Image m_image;
    private void Start()
    {
        m_image = GetComponent<Image>();
    }

    private void Update()
    {
        float opacity = 0f;
        switch (GameManager.instance.dm.transition)
        {
            case DimensionsManager.Transition.In:
                opacity = m_image.color.a + ((float)1 / GameManager.instance.dimensionTransitionFrames);

                if (opacity >= 1)
                    opacity = 1;
                break;
            case DimensionsManager.Transition.Out:
                opacity = m_image.color.a - ((float)1 / GameManager.instance.dimensionTransitionFrames);

                if (opacity >= 1)
                    opacity = 1;
                break;
        }
        Debug.Log(opacity);
        m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, opacity);
    }
}
