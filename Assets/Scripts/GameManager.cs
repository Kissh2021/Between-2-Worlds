using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private Dimentions _dimention;

    public static GameManager getInstance()
    {
        if (!_instance)
            _instance = new GameManager();
        return _instance;
    }

    public GameManager()
    {
        this._dimention = Dimentions.Afthlea;
    }

    public void wrap()
    {
        if (this._dimention == Dimentions.Afthlea)
            this._dimention = Dimentions.Wysteria;
        else
            this._dimention = Dimentions.Afthlea;
    }

    public Dimentions dimention
    {
        get { return _dimention; }
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
