using UnityEngine;
using System.Collections;

public class EnemyBody : MonoBehaviour {

	public GameObject home;

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag=="Player")
		{
			col.gameObject.GetComponent<PizzaInventory>().DecreaseHealth();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			home = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			home = null;
		}
	}
}
