using UnityEngine;
using System.Collections;

public class Repulsor : Gravitor
{
    protected override bool isAwayCenterForce{
        get{
            return true;
        }
    }
}
