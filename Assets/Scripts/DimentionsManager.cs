public class DimentionsManager
{
    public enum Dimentions
    {
        Afthlea = 0,
        Wysteria = 1
    }

    private Dimentions _dimention = Dimentions.Afthlea;

    public void warp()
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
}