using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DimensionsManager
{
    public enum Dimensions
    {
        Afthlea = 0,
        Wysteria = 1
    }

    public enum Transition
    {
        In,
        Out
    }

    public UnityEvent warpEvent = new UnityEvent();

    private Dimensions _dimension = Dimensions.Afthlea;

    public Transition? transition = null;

    public void warp()
    {
        if (_dimension == Dimensions.Afthlea)
            _dimension = Dimensions.Wysteria;
        else
            _dimension = Dimensions.Afthlea;

        warpEvent.Invoke();
    }

    public Dimensions dimension
    {
        get { return _dimension; }
    }
}