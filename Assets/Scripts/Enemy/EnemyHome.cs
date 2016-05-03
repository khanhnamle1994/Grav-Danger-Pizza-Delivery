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
		//Vector2 directionVector = OtherToParticleVector2(other);
		//float gravityForce = home.GetComponent<Rigidbody2D>().mass * .1f;
		//home.GetComponent<Rigidbody2D>().AddForce(directionVector.normalized * force,ForceMode2D.Force);
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
