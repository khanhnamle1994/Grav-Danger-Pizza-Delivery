using UnityEngine;
using System.Collections;

public class PizzaVelocity : MonoBehaviour {

	public Vector2 initialVelocity;

    Rigidbody2D rg2d;

	// Use this for initialization
	void Start () {
        rg2d = GetComponent<Rigidbody2D>();
        rg2d.velocity = initialVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.position += new Vector3(velocity.x, velocity.y);
	}

	public void AddForce(Vector2 newForce)
	{
        rg2d.velocity += newForce;
	}
}
