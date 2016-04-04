using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {

	float physicalRadius, gravityRadius;
    public float mass;
	float gravityForce;

    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        
	}

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.mass = mass;
    }
	
	// Update is called once per frame
	void Update () {
		gravityForce = rb2d.mass * .01f;
		/*foreach (Collider collider in Physics.OverlapSphere(gameObject.transform.position, gravityRadius)) {
			// calculate direction from target to me
			Vector3 forceDirection = gameObject.transform.position - collider.transform.position;

			// apply force on target towards me
			collider.GetComponent<Rigidbody>().AddForce (forceDirection.normalized * gravityForce);
		}*/
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


			other.gameObject.GetComponent<PizzaVelocity>().AddForce(gravitate.normalized * gravityForce / distance / distance);
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
        Debug.Log(other.gameObject.name);
		if (other.gameObject.tag == "Player")
		{
            GameObject.FindObjectOfType<PlanetSpawner>().GetComponent<PlanetSpawner>().StopGrowing();

			Destroy (gameObject);
		}
	}

	void OnMouseDown ()
	{
		Destroy (gameObject);
	}

	public void IncreaseMass (){
		rb2d.mass += .1f;
		//Debug.Log (mass);
	}

    public void Explode()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 difference = gameObject.transform.position - player.transform.position;
        difference.z = 0;

        float distance = difference.magnitude;

        //other.gameObject.GetComponent<Rigidbody2D> ().AddForce (forceDirection.normalized * gravityForce);
        Debug.Log("try explode "+distance);
        if(distance < 1000f)
        {
            player.GetComponent<PizzaVelocity>().AddForce(-difference * 10f / distance / distance);
            Debug.Log("add force called");
        }
            
    }
}
