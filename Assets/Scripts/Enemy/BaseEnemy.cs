using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour {

    Rigidbody2D rb2d;
    public Vector2 homePosition;
    public float normalSpeed = 5f;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    
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
        Vector2 towardsDest = Vector2Minus(rb2d.position,dest) * 0.7f;
        rb2d.AddForce(towardsDest);
    }

    protected void MoveTowards(Vector2 dest)
    {
        Vector2 towardsDest = Vector2Minus(rb2d.position, dest);
        towardsDest.Normalize();
        rb2d.AddForce(towardsDest * normalSpeed);
    }

    protected void MoveAways(Vector2 dest)
    {
        Vector2 towardsDest = Vector2Minus(rb2d.position, dest);
        towardsDest.Normalize();
        rb2d.AddForce(towardsDest * normalSpeed * -1f);
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
