using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {

	float physicalRadius, gravityRadius, mass;
	float gravityForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gravityForce = mass * .0000001f;
		/*foreach (Collider collider in Physics.OverlapSphere(gameObject.transform.position, gravityRadius)) {
			// calculate direction from target to me
			Vector3 forceDirection = gameObject.transform.position - collider.transform.position;

			// apply force on target towards me
			collider.GetComponent<Rigidbody>().AddForce (forceDirection.normalized * gravityForce);
		}*/
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			Vector3 gravitate3D = gameObject.transform.position - other.transform.position;
			Debug.Log (gravitate3D);
			Vector2 gravitate = new Vector2 (gravitate3D.x, gravitate3D.y);
			//other.gameObject.GetComponent<Rigidbody2D> ().AddForce (forceDirection.normalized * gravityForce);
			other.gameObject.GetComponent<PizzaVelocity>().AddForce(gravitate * gravityForce);
		}
	}

	public void IncreaseMass (){
		mass += .1f;
		//Debug.Log (mass);
	}
}
