using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour {

    public class EnemyState
    {
        public enum state { Home, GoHome, PlayerNear, PlayerRecentlyHit, PlayerFar}
    }



    protected Rigidbody2D rb2d;
    public float normalSpeed = 5f;
    public EnemyState.state currentState = EnemyState.state.Home;

    // Use this for initialization
    protected void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
	
	

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            NearPlayerEnemyAI(other.gameObject);
        }
    }

    protected abstract void NearPlayerEnemyAI(GameObject other);

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FarPlayerEnemyAI(other.gameObject);
        }
    }

    protected abstract void FarPlayerEnemyAI(GameObject other);


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            CollideWithPlayer(col.gameObject);
        }
    }

    protected abstract void CollideWithPlayer(GameObject other);

    // Move towards destination
    // with faster speed the farther one is from the desination
    protected void GravMoveTowards(Vector2 dest)
    {
        Vector2 towardsDest = Vector2Minus(rb2d.position,dest) * rb2d.mass *0.25f;
        rb2d.AddForce(towardsDest);
    }

    protected void MoveTowards(Vector2 dest)
    {
        Vector2 towardsDest = Vector2Minus(rb2d.position, dest);
        towardsDest.Normalize();
        rb2d.AddForce(towardsDest * normalSpeed * rb2d.mass, ForceMode2D.Force);
    }

    protected void MoveAways(Vector2 dest)
    {
        Vector2 towardsDest = Vector2Minus(rb2d.position, dest);
        towardsDest.Normalize();
        rb2d.AddForce(towardsDest * normalSpeed * -1f, ForceMode2D.Force);
    }

    Vector2 Vector2Minus(Vector2 beg, Vector2 end)
    {
        return end - beg;
    }

    protected void Stop()
    {
        rb2d.velocity = Vector2.zero;
    }
}
