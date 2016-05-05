using UnityEngine;
using System.Collections;

public class PizzaVelocity : MonoBehaviour {

	public Vector2 initialVelocity;

	public Rigidbody2D rg2d { get; protected set; }

	// Use this for initialization
	void Start () {
		rg2d = GetComponent<Rigidbody2D>();
		rg2d.velocity = initialVelocity;
	}

	public void AddForce(Vector2 newForce)
	{
		rg2d.AddForce(newForce, ForceMode2D.Force);
	}


}