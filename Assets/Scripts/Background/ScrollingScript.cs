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
	public bool isLooping = true;

	Rigidbody2D rb2d;

	//private List<Transform> backgroundPart;
	//List<Vector2> Dirs = new List<Vector2>();

    
	public Renderer center;
	private List <GameObject> backgroundClones; // not used right now
    private List<GameObject> clones = new List<GameObject>();

	//public List<Renderer> surrounding; // not used right now
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

    private bool isReAllocatingCenter;
    private float lastTimePlayerInCenter;

	// Use this for initialization
	void Start () {


        /*
		if (isLooping) {
			// Get all children of the layer with a renderer
			backgroundPart = new List<Transform> ();
			for (int i = 0; i < transform.childCount; i++) {
				Transform child = transform.GetChild (i);
				if (child.GetComponent<Renderer>() != null)
					backgroundPart.Add (child);
			}
		}
        /**/

        isReAllocatingCenter = false;


        rb2d = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();

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
            bt.Pos = pos;
            clones.Add(clone);
        }

        BackgroundTile center_bt = center.gameObject.AddComponent<BackgroundTile>();
        center_bt.Pos = new Position("CC");

        lastTimePlayerInCenter = 0f;

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
            // if center no longer visible by camera and is not currently reallocating the center
            if (center.IsVisibileFrom(Camera.main) == false )//&& isReAllocatingCenter == false)
            {
                isReAllocatingCenter = true;

                // find backgroundPart where visible
                GameObject new_center = null;
                
                
                // if longer than 4 seconds not in center, force reallocate center
                if (lastTimePlayerInCenter>2f)
                {
                    foreach (GameObject go in clones)
                    {
                        Vector3 camera_pos = Camera.main.transform.position;
                        camera_pos.z = center.bounds.center.z;
                        if (go.GetComponent<Renderer>().bounds.Contains(camera_pos))
                        {
                            Debug.Log("player in " + go.GetComponent<BackgroundTile>().posVisible);
                            new_center = go;
                            ReAllocateCenter(new_center);
                        }
                    }
                }

                int count = 0;
                foreach (GameObject go in clones)
                {
                    if (go.GetComponent<Renderer>().IsVisibileFrom(Camera.main))
                    {
                        new_center = go;
                        count++;
                    }
                }
                if (count == 1)
                    ReAllocateCenter(new_center);
                lastTimePlayerInCenter += Time.deltaTime;
                isReAllocatingCenter = false;
            }
            else
                lastTimePlayerInCenter = 0f;
        }
	}

    void ReAllocateCenter(GameObject new_center)
    {
        BackgroundTile center_bt = new_center.GetComponent<BackgroundTile>();
        Position new_center_pos = center_bt.Pos;

        Renderer old_center = center;
        center = new_center.GetComponent<Renderer>();

        clones.Remove(new_center);
        clones.Add(old_center.gameObject);

        foreach(GameObject clone in clones)
        {
            Debug.Log("===========");
            BackgroundTile bt = clone.GetComponent<BackgroundTile>();
            if (bt.Pos.ShouldMove(new_center_pos))
            {
                // Modify clone position to oppoiste
                Debug.Log("old position " + bt.Pos.ToString());
                Position pos = bt.Pos;
                pos.ShiftOpposite();
                bt.Pos = pos;
                TeleportTo(clone, pos);
                Debug.Log("new position " + bt.Pos.ToString());
                //Destroy(clone);
            }
            else
            {
                // just modifiy position kept if tile not moved
                // can just mmodifiy the reference, don't need to use SetPos
                Debug.Log("old position "+bt.Pos.ToString());
                Position pos = bt.Pos;
                pos.ShiftString( Position.OppositeDir( new_center_pos.GetDir()));
                bt.Pos = pos;
                Debug.Log("new position " + bt.Pos.ToString());
            }
            
        }


        // center tile never moved just position reallocated
        //BackgroundTile old_center_bt = old_center.gameObject.GetComponent<BackgroundTile>();
        //old_center_bt.Pos.ShiftString(Position.OppositeDir(new_center_pos.GetDir()));

        center_bt.Pos = new Position("CC");

    }


	GameObject CreateBackgroundClone(GameObject original, Position p)
	{
		GameObject clone = Instantiate(original);
		TeleportTo (clone, p);
		return clone;
	}

	void TeleportTo(GameObject go, Position p){
        Vector2 new_center = FetchNewCenter(p);
		go.transform.position = new Vector3(new_center.x,new_center.y,1);
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
    static Dictionary<string, string> opposite = new Dictionary<string, string>() { { "N", "S" }, { "S", "N" }, { "C", "C" }, { "E", "W" }, { "W", "E" } };

    /// <summary>
    /// The dirs grid.
    /// 	NW NC NE
    /// 	CW CC CE
    /// 	SW SC SE
    /// </summary>
    static List<string> dirsxArray = new List<string> { "SW", "SC", "SE", "CW","CC", "CE", "NW", "NC", "NE" };

    public Position(string v, string h){
		vertical = v;
		horizontal = h;
	}

	public Position (string pos) : this (pos.Substring (0, 1), pos.Substring (1, 1))
	{
		
	}

    public Position(Position pos) : this(pos.ToString())
    {

    }

	public string GetVertical(){
		return vertical;
	}

	public string GetHorizontal(){
		return horizontal;
	}

	public string GetDir(){
		return GetVertical () + GetHorizontal ();
	}

    
    public override string ToString()
    {
        return GetDir();
    }

    // Shift the Position by the characters
    /// <summary>
    /// The dirs grid.
    /// 	NW NC NE
    /// 	CW CC CE
    /// 	SW SC SE
    /// If new_center == CE then shift using CW
    /// 	   NE NC
    /// 	   CW CE
    /// 	   SW SC    
    /// </summary>
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
    
    /// <summary>
    ///     Return true if current should move from the new center allocated
    ///         
    ///     If CE passed in then every Position with   *W should return true   NW CW
    ///     If NW passed in then every Position withe either S* or *E should return true
    /// 
    /// </summary>
    /// <param name="new_center_pos">The new center position</param>
    /// <returns></returns>
    public bool ShouldMove(Position new_center_pos)
    {
        string h = new_center_pos.GetHorizontal();
        string v = new_center_pos.GetVertical();

        // if new_center_pos horizontal direction
        // isn't Center and the current horizontal
        // is the same as the opposite of new_center_pos horizontal
        // then return true
        if (
                (
                    (h != "C") && (GetHorizontal() == Position.Opposite(h))
                )
                ||
                (
                    (v != "C") && (GetVertical() == Position.Opposite(v))
                )
            )
            return true;
        else
        {
            if (GetDir() == "NW")
            {
                //Debug.Log(new_center_pos);
                //Debug.Log((h != "C"));
                //Debug.Log((h != "C") && (GetHorizontal() == Position.Opposite(h)));
                //Debug.Log((v != "C") && (GetVertical() == Position.Opposite(v)));

                //throw new Exception("rawr");
            }
           
            return false;
        }
    }

    public void ShiftOpposite()
    {
        vertical = Opposite(GetVertical());
        horizontal = Opposite(GetHorizontal());
    }

    static string Opposite(string s)
    {
        return opposite[s];
    }

    public static string OppositeDir(string s)
    {
        if (s.Length == 2)
            return opposite[s.Substring(0, 1)] + opposite[s.Substring(1, 1)];
        else
            throw new Exception("String passed in must be two characters long");
    }

    public static int ConvertDirection(string s)
	{
		return h[s];
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