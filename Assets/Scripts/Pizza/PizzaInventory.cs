using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PizzaInventory : MonoBehaviour {
    Dictionary<string, int> inventory;

	public Text[] inventoryText;
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
		for (int index = 0; index < inventoryText.Length; index++)
			inventoryText[index].text = itemNames[index] + ": " + inventory [itemNames[index]]; 
	}

    public void Increment(string s)
    {
        inventory[s] += 1;
    }

	public void IncrementAmount(string s, int amount)
	{
		inventory [s] += amount;
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
