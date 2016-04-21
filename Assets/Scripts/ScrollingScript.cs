using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

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
	List<Vector2> Dirs = new List<Vector2>();


	public Renderer center;
	public List<Renderer> surrounding;
	private Hashtable phoneGrid;

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
		Debug.Log (backgroundPart.Count);
		rb2d = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();


		for (int k = -1; k <= 1; k++) {
			for (int j = -1; j <= 1; j++) {
				Dirs.Add (new Vector2 (j, k));
			}
		}
		// (-1,-1),(0,-1),....

		phoneGrid = new Hashtable ();
		phoneGrid.Add (0, null);

		// assign phoneGrid 1 to 9
		for (int l = 0; l < Dirs.Count; l++) {
			phoneGrid.Add (l+1, Dirs [l]);
		}
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

		if (isLooping) {
			Debug.Log (backgroundPart.FirstOrDefault ().GetComponent<Renderer> ().IsVisibileFrom (Camera.main));



			// if center no longer visible
			if (center.IsVisibileFrom (Camera.main) == false) {

				// find backgroundPart where visible
			}
		}
	}
}


