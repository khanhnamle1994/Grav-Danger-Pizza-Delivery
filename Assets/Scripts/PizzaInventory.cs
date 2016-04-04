using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PizzaInventory : MonoBehaviour {
    Dictionary<string, int> inventory;

    // Use this for initialization
    void Start () {
        inventory = new Dictionary<string, int>();
        string[] strings = { "pizza" };
        foreach (string s in strings)
        {
            inventory.Add(s, 0);
        }
           
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Increment(string s)
    {
        inventory[s] += 1;
    }

    void Decrement(string s)
    {
        inventory[s] -= 1;
    }
}
