using UnityEngine;
using System.Collections;
using System;

public class EnemyChase : BaseEnemy {


    private float lastHitPlayerTime = 0f;
    private bool recentlyDamangedPlayer = false;

    protected override void NearPlayerEnemyAI(GameObject other)
    {
        if(!recentlyDamangedPlayer)
            GravMoveTowards(other.transform.position);
    }

    protected override void FarPlayerEnemyAI(GameObject other)
    {
        if(!recentlyDamangedPlayer)
            Stop();
    }

    protected override void CollideWithPlayer(GameObject other)
    {
        
        if (!recentlyDamangedPlayer)
        {
            other.GetComponent<PizzaInventory>().DecreaseHealth();
            Stop();
            MoveAways(other.transform.position);
            recentlyDamangedPlayer = true;
        }
    }

    IEnumerator HandleDamage()
    {
        yield return new WaitForSeconds(3);
        recentlyDamangedPlayer = false;
    }

}
