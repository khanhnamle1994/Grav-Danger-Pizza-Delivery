using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ScrollingScript : MonoBehaviour {

	// Credit given to: pixelnest.io parallax-scrolling
	// Modified from only one x direction
	// to all four directions as well as handling destruction
	// and recreation of background parts

	// scrolling speed
	public Vector2 speed = new Vector2(2,2);

	// moving direction
	public Vector2 direction = new Vector2(1,0);

	/// <summary>
	/// if Background is Inifinite
	/// </summary>
	public bool isLooping = false;

	Rigidbody2D rb2d;

	private List<Transform> backgroundPart;

	// Use this for initialization
	void Start () {
		if (isLooping) {
			// Get all children of the layer with a renderer
			backgroundPart = new List<Transform> ();
			for (int i = 0; i < transform.childCount; i++) {
				Transform child = transform.GetChild (i);
				if (child.GetComponent<Renderer>() != null)
					backgroundPart.Add (child);
			}
		}
		Debug.Log (backgroundPart);
		rb2d = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		// move in direction of the camera
		direction = rb2d.velocity.normalized;

		Vector3 movement = new Vector3 (
			                   speed.x * direction.x,
			                   speed.y * direction.y,
			                   0);
		movement *= Time.deltaTime;
		transform.Translate (movement);


	}
}


