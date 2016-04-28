using UnityEngine;
using System.Collections;

public class BackgroundTile : MonoBehaviour {

    Position pos;

    public void SetPos(Position p)
    {
        pos = p;
    }

    public Position GetPos()
    {
        return pos;
    }
}
