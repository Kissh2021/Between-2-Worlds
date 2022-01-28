using UnityEngine;
using UnityEngine.Events;

public class DimentionsManager
{
    public enum Dimentions
    {
        Afthlea = 0,
        Wysteria = 1
    }

    public UnityEvent warpEvent;

    private Dimentions _dimention = Dimentions.Afthlea;

    public DimentionsManager(){
        warpEvent = new UnityEvent();
    }

    public void warp()
    {
        if (_dimention == Dimentions.Afthlea)
            _dimention = Dimentions.Wysteria;
        else
            _dimention = Dimentions.Afthlea;

        warpEvent.Invoke();
    }

    public Dimentions dimention
    {
        get { return _dimention; }
    }
}