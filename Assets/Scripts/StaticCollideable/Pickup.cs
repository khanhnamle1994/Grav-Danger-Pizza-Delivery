using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

    [SerializeField]
	private string itemName;
    public string ItemName { get { return itemName; } set {itemName=value; } }
	public int amount;

    public delegate void OnPickUpDeath();
    public event OnPickUpDeath onPickUpDeath = delegate { };

    void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<PizzaInventory> ().IncrementAmount (itemName, amount);
			Destroy (gameObject);
		}
	}

    void OnDestroy()
    {
        onPickUpDeath();
    }
}
