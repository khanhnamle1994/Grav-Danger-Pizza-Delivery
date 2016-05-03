using UnityEngine;
using System.Collections;

public class PointToGoal : MonoBehaviour {

	public GameObject goal;
	public GameObject player;
	SpriteRenderer arrow;

	// Use this for initialization
	void Start () {
		goal = GameObject.FindGameObjectWithTag ("Finish");
		player = GameObject.FindGameObjectWithTag ("Player");
		arrow = gameObject.GetComponent<SpriteRenderer> ();
		arrow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
