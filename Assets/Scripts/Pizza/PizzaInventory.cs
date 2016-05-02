using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PizzaInventory : MonoBehaviour {
    Dictionary<string, int> inventory;

	public Text inventoryText;
	public string[] itemNames;
	public int [] itemAmounts;

    // Use this for initialization
    void Start () {
        inventory = new Dictionary<string, int>();
        foreach (string s in itemNames)
        {
            inventory.Add(s, 0);
        }
		for (int i = 0; i < itemAmounts.Length; i++) {
			inventory [itemNames[i]] = itemAmounts [i];
		}
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		inventoryText.text = "INVENTORY";
		for (int index = 0; index < itemNames.Length; index++)
			inventoryText.text += "\n" + itemNames[index] + ": " + inventory [itemNames[index]]; 
	}

    public void Increment(string s)
    {
		IncrementAmount (s, 1);
    }

	public void IncrementAmount(string s, int amount)
	{
		try {
			inventory[s] += amount;
		}
		catch (KeyNotFoundException e)
		{
			System.Array.Resize(ref itemNames, itemNames.Length + 1);
			itemNames [itemNames.Length - 1] = s;
			inventory.Add(s, amount);
		}
	}

	public void Decrement(string s)
    {
        inventory[s] -= 1;
    }

	public void DecrementAmount(string s, int amount)
	{
		IncrementAmount (s, -amount);
	}

	public void SetAmount(string s, int amount)
	{
		inventory [s] = amount;
	}
}
