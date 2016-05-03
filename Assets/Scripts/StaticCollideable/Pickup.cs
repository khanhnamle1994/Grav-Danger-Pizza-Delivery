using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public string itemName;
	public int amount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PizzaInventory> ().IncrementAmount (itemName, amount);
			Destroy (gameObject);
		}
	}
}
