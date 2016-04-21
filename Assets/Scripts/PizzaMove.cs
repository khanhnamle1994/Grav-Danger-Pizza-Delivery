using UnityEngine;
using System.Collections;


public class PizzaMove : MonoBehaviour {

    Rigidbody2D rg2d;

	// Use this for initialization
	void Start () {
        rg2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

	    
	}
    void FixedUpdate()
    {
        rg2d.MoveRotation(Angle360(Vector2.right, rg2d.velocity));
		rg2d.velocity = Friction (rg2d.velocity);
		Debug.Log ("velocity " + rg2d.velocity);
    }

    float Angle360(Vector2 from, Vector2 to)
    {
        // only returns from 0 to 180
        float angle = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);
        if (cross.z < 0)
            angle = 360 - angle;
        Debug.Log("Angle: " + angle);
        return angle;
    }

	Vector2 Friction(Vector2 velocity)
	{
		float magnitude = velocity.magnitude;
		if (magnitude > 15f)
			return velocity * 0.9f;
		else
			return velocity;
	}

}
