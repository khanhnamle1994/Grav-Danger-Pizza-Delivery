using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;

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
	//List<Vector2> Dirs = new List<Vector2>();


	public Renderer center;
	private List <GameObject> backgroundClones; // not used right now
    private List<GameObject> clones = new List<GameObject>();

	public List<Renderer> surrounding; // not used right now
	private Hashtable phoneGrid; // not used right now

	/// <summary>
	/// The dirs grid.
	/// 	NW NC NE
	/// 	CW CC CE
	/// 	SW SC SE
	/// </summary>
	private List<string> dirsArray;

	// holds the width and height of the center background
	private float width;
	private float height;



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

		/*
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

		dirsArray = new List<string>{"","SW","SC","SE","CW","CC","CE","NW","NC","NE"};
		*/

		width = center.bounds.size.x;
		height = center.bounds.size.y;


		List<string> dirsArray = new List<string>{"SW","SC","SE","CW","CE","NW","NC","NE"};
        //List<Transform> clones;
		foreach(string dir in dirsArray)
		{
            Position pos = new Position(dir);
			GameObject clone = CreateBackgroundClone(center.gameObject,pos);
			clone.transform.parent = gameObject.transform;
            BackgroundTile bt = clone.AddComponent<BackgroundTile>();
            bt.SetPos(pos);
            clones.Add(clone);
        }
        Debug.Log(clones[0].ToString());


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
                foreach (GameObject go in clones)
                {
                    if(go.GetComponent<Renderer>().IsVisibileFrom(Camera.main ))
                    {
                        ReAllocateCenter(go);
                        break;
                    }
                }
            }
		}
	}

    void ReAllocateCenter(GameObject new_center)
    {
        Position new_center_pos = new_center.GetComponent<BackgroundTile>().GetPos();
        


    }


	GameObject CreateBackgroundClone(GameObject original, Position p)
	{
		GameObject clone = Instantiate(original);
		TeleportTo (clone, p);
		return clone;
	}

	void TeleportTo(GameObject go, Position p){
		go.transform.position = FetchNewCenter (p);
	}

	Vector2 FetchNewCenter(Position p){
		float new_x = center.bounds.center.x;
		float new_y = center.bounds.center.y;
		new_x += width * Position.ConvertDirection (p.GetHorizontal ());
		new_y += height * Position.ConvertDirection (p.GetVertical ());
		return new Vector2 (new_x, new_y);
	}
}


public class Position {

	string vertical;
	string horizontal;
	static Dictionary<string,int> h = new Dictionary<string,int>(){{"N",1},{"S",-1},{"C",0},{"E",1},{"W",-1}};

	public Position(string v, string h){
		vertical = v;
		horizontal = h;
	}

	public Position (string pos) : this (pos.Substring (0, 1), pos.Substring (1, 1))
	{
		
	}

	public string GetVertical(){
		return vertical;
	}

	public string GetHorizontal(){
		return horizontal;
	}

	public string GetPosition(){
		return GetVertical () + GetHorizontal ();
	}

	// Shift the Position by the characters
	public void Shift(string shift_v, string shift_h)
	{
		vertical = ShiftVertical (vertical, shift_v);
		horizontal = ShiftHorizontal (horizontal, shift_h);
	}

	public void ShiftString(string shift_v_h)
	{
		if (shift_v_h.Length == 2)
			Shift (shift_v_h.Substring (0,1), shift_v_h.Substring (1,1));
		else
			throw new Exception ("Shift passed in must be two characters long");
	}

	public static int ConvertDirection(string s)
	{
		return h [s];
	}
			
	static string ShiftVertical(string v, string shift)
	{
		if (shift == "N")
			return ShiftUp (v);
		else if (shift == "S")
			return ShiftDown (v);
		else if (shift == "C")
			return v;
		else
			throw new Exception ("Shift Vertical doesn't exist");
	}

	static string ShiftHorizontal(string h, string shift)
	{
		if (shift == "W")
			return ShiftLeft (h);
		else if (shift == "E")
			return ShiftRight (h);
		else if (shift == "C")
			return h;
		else
			throw new Exception ("Shift Vertical doesn't exist");
	}

	static string ShiftUp(string v)
	{
		if (v == "S")
			return "C";
		else if (v == "C")
			return "N";
		else //if (v == "N")
			throw new Exception ("Cannot go higher than N");
	}

	static string ShiftDown(string v)
	{
		if (v == "N")
			return "C";
		else if (v == "C")
			return "S";
		else //if (v == "S")
			throw new Exception ("Cannot go lower than S");
	}

	static string ShiftRight(string h)
	{
		if (h == "W")
			return "C";
		else if (h == "C")
			return "E";
		else //if (h == "E")
			throw new Exception ("Cannot go right of E");
	}

	static string ShiftLeft(string h)
	{
		if (h == "E")
			return "C";
		else if (h == "C")
			return "W";
		else //if (h == "W")
			throw new Exception ("Cannot go left of W");
	}

}