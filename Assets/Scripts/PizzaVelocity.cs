using UnityEngine;
using System.Collections;

public class PizzaVelocity : MonoBehaviour {

	public Vector2 initialVelocity;
	Vector2 velocity;

	// Use this for initialization
	void Start () {
		velocity = initialVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += new Vector3(velocity.x, velocity.y);
	}

	public void AddForce(Vector2 newForce)
	{
		velocity += newForce;
	}
}
