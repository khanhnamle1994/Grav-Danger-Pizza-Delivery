using UnityEngine;
using System.Collections;

public abstract class Particle : MonoBehaviour {

	float gravityRadius;
	float affectiveForce;

	//PizzaVelocity pv;

	// Use this for initialization
	void Start () {
		//pv = GameObject.FindGameObjectWithTag ("Player").GetComponent<PizzaVelocity>();
	}

	void Awake()
	{
        throw new UnityException("deprecated particle");
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerStay2D (Collider2D other)
	{
		// if player within planet effective gravity radius then
		// start add force to the player
		if(other.gameObject.tag == "Player")
		{
			Vector3 gravitate3D = gameObject.transform.position - other.transform.position;
			Debug.Log (gravitate3D);
			// vector from player position to the planet position
			Vector2 gravitate = new Vector2 (gravitate3D.x, gravitate3D.y);


			float distance = gravitate.magnitude;

			//other.gameObject.GetComponent<Rigidbody2D> ().AddForce (forceDirection.normalized * gravityForce);


			//pv.AddForce(gravitate.normalized * gravityForce / distance / distance);
			//pv.AddForce2(gravitate.normalized * gravityForce);

			//Affect();
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		Debug.Log(other.gameObject.name);
		if (other.gameObject.tag == "Player")
		{
			GameObject.FindObjectOfType<ParticleSpawner>().GetComponent<ParticleSpawner>().StopGrowing();
			//Explode ();
			Destroy (gameObject);
		}
	}

	void OnMouseDown ()
	{
		Destroy (gameObject);
	}
}
