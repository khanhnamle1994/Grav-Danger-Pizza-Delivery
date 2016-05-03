using UnityEngine;
using System.Collections;

public class AsteroidReflect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Rigidbody2D rb2d = other.GetComponent<Rigidbody2D>();
            rb2d.velocity = rb2d.velocity * -0.5f;
            other.GetComponent<PizzaInventory>().DecreaseHealth();
        }
    }



}
