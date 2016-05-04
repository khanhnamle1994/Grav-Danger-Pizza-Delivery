using UnityEngine;
using System.Collections;

public class EnemyBody : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag=="Player")
		{
			col.gameObject.GetComponent<PizzaInventory>().DecreaseHealth();
		}
	}
}
