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
		Vector2 directionVector = home.transform.position - enemy.transform.position;
		float gravityForce = home.GetComponent<Rigidbody2D>().mass * 1f;
		enemy.GetComponent<Rigidbody2D>().AddForce(directionVector.normalized * gravityForce, ForceMode2D.Force);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			home = other.gameObject;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			if (enemy.GetComponent<EnemyBody> ().home != null) {
				home = enemy.GetComponent<EnemyBody> ().home;
			} else
				home = gameObject;
		}
	}
}
