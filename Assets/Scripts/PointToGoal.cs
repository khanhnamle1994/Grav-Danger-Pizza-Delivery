using UnityEngine;
using System.Collections;

public class PointToGoal : MonoBehaviour {

	public GameObject goal;
	public GameObject player;
	SpriteRenderer arrow;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		goal = GameObject.FindGameObjectWithTag ("Finish");
		player = GameObject.FindGameObjectWithTag ("Player");
		arrow = gameObject.GetComponent<SpriteRenderer> ();
		//arrow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		direction = goal.transform.position - player.transform.position;
		Debug.DrawRay (player.transform.position, direction);

		float angle = Vector2.Angle(goal.transform.position, player.transform.position);
		Vector3 cross = Vector3.Cross(goal.transform.position, player.transform.position);
		if (cross.z < 0)
			angle = 360 - angle;

		gameObject.transform.eulerAngles = new Vector3(0, 0, angle);
	}
}
