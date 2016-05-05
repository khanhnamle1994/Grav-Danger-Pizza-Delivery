using UnityEngine;
using System.Collections;
using System;

public class EnemyChase : BaseEnemy {

    public Vector2 homePosition;
    protected GameObject targetObject;

    protected new void Start()
    {
        base.Start();
        homePosition = gameObject.transform.position;
    }

    void Update()
    {
        switch(currentState)
        {
            case EnemyState.state.GoHome:
                GoHome();
                break;
            case EnemyState.state.PlayerNear:
                PlayerNear();
                break;
            case EnemyState.state.PlayerFar:
                PlayerFar();
                break;
            case EnemyState.state.PlayerRecentlyHit:
                PlayerRecentlyHit();
                break;
            default:
                break;
        }
    }


    protected void GoHome()
    {
        GravMoveTowards(homePosition);
    }

    protected void PlayerNear()
    {
        
        GravMoveTowards(targetObject.transform.position);
    }

    protected void PlayerFar()
    {
        Stop();
        ArrayList value = new ArrayList();
        value.Add(EnemyState.state.GoHome);
        value.Add(5f);
        StartCoroutine("DelayedChangeToState", value);
    }

    IEnumerator DelayedChangeToState(ArrayList values)
    {
        EnemyState.state toState = (EnemyState.state) values[0];
        float waitTime = (float) values[1];
        yield return new WaitForSeconds(waitTime);
        currentState = toState;
    }

    protected void PlayerRecentlyHit()
    {

    }

    
    private bool recentlyDamangedPlayer = false;

    protected override void NearPlayerEnemyAI(GameObject other)
    {
        if (currentState != EnemyState.state.PlayerRecentlyHit)
            currentState = EnemyState.state.PlayerNear;
        targetObject = other;
    }

    protected override void FarPlayerEnemyAI(GameObject other)
    {
        if (currentState != EnemyState.state.PlayerRecentlyHit)
            currentState = EnemyState.state.PlayerFar;
    }

    protected override void CollideWithPlayer(GameObject other)
    {
        
        if (!recentlyDamangedPlayer)
        {
            other.GetComponent<PizzaInventory>().DecreaseHealth();
            Stop();
            MoveAways(other.transform.position);
            recentlyDamangedPlayer = true;
            currentState = EnemyState.state.PlayerRecentlyHit;
            StartCoroutine("HandleDamage");
        }
    }

    IEnumerator HandleDamage()
    {
        yield return new WaitForSeconds(2);
        recentlyDamangedPlayer = false;
        currentState = EnemyState.state.GoHome;
    }

}
