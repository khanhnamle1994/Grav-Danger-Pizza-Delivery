using UnityEngine;

public class BackgroundTile : MonoBehaviour {

    private Position _pos;
    public Position Pos
    {
        get
        {
            return _pos;
        }
        set
        {
            _pos = value;
            posVisible = _pos.GetDir();
        }
    }
    public string posVisible;
}
