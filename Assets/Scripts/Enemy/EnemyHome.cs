using UnityEngine;
using System.Collections;

public class EnemyHome : MonoBehaviour {

	public GameObject enemy;
	GameObject home;

	// Use this for initialization
	void Start () {
		home = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (enemy.transform.position, home.transform.position);
<<<<<<< HEAD
		//Vector2 directionVector = OtherToParticleVector2(other);
		//float gravityForce = home.GetComponent<Rigidbody2D>().mass * .1f;
		//home.GetComponent<Rigidbody2D>().AddForce(directionVector.normalized * force,ForceMode2D.Force);
=======
		Vector2 directionVector = home.transform.position - enemy.transform.position;
		float gravityForce = home.GetComponent<Rigidbody2D>().mass * 1f;
		enemy.GetComponent<Rigidbody2D>().AddForce(directionVector.normalized * gravityForce, ForceMode2D.Force);
>>>>>>> e86d29fee4f8ae8ddfd8a7e93d8ca61b1c3ec45d
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			home = other.gameObject;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		home = gameObject;
	}
}
