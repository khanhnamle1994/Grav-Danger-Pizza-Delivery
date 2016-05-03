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
        Vector3 dir = goal.transform.position - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime*5f);
    }
}
