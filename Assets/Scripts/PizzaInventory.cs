using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PizzaInventory : MonoBehaviour {
    Dictionary<string, int> inventory;

	public Text[] inventoryText;
	public string[] itemNames;

    // Use this for initialization
    void Start () {
        inventory = new Dictionary<string, int>();
        foreach (string s in itemNames)
        {
            inventory.Add(s, 0);
        }
           
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int index = 0; index < inventoryText.Length; index++)
			inventoryText[index].text = itemNames[index] + ": " + inventory [itemNames[index]]; 
	}

    public void Increment(string s)
    {
        inventory[s] += 1;
    }

	public void Decrement(string s)
    {
        inventory[s] -= 1;
    }
}
