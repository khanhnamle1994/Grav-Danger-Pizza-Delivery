using UnityEngine;
using System.Collections;
using System;

public class Slowor : Velocitor {
    public override float velocityMultipler
    {
        get
        {
            return -10f;
        }
    }
}
