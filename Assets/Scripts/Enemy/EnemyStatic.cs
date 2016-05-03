using UnityEngine;
using System.Collections;
using System;

public class EnemyStatic : BaseEnemy {

    protected override void NearPlayerEnemyAI(GameObject other)
    {
        MoveTowards(other.transform.position);
    }

    protected override void FarPlayerEnemyAI(GameObject other)
    {
        Stop();
    }

}
