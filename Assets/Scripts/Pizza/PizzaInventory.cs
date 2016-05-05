using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PizzaInventory : MonoBehaviour {
    Dictionary<string, int> inventory;

	public Text inventoryText;
	public string[] itemNames;
	public int [] itemAmounts;

    public string healthItem = "Pizza";
    public int startingHealthAmount = 15;

    //public delegate void OnInventoryChangeOld(string textString);
    //public event OnInventoryChangeOld onInventoryChangeOld = delegate {};

    public delegate void OnInventoryChange();
    public event OnInventoryChange onInventoryChange = delegate { };


    public delegate void OnPlayerDeath();
    public event OnPlayerDeath onPlayerDeath = delegate { };

    void Awake()
    {
        inventory = new Dictionary<string, int>();
        inventory.Add(healthItem, startingHealthAmount);
        foreach (string s in itemNames)
        {
            inventory.Add(s, 0);
        }
        for (int i = 0; i < itemAmounts.Length; i++)
        {
            inventory[itemNames[i]] = itemAmounts[i];
        }

        OnInventoryChange DeathCheck = () =>
        {
            if (IsDeathCheck())
            {
                onPlayerDeath();
            }
        };

        onInventoryChange += DeathCheck;
    }


    // Use this for initialization
    void Start () {
        if (inventoryText == null)
            inventoryText = GameObject.Find("InventoryText").GetComponent<Text>();
        
        ForcedInventoryTextEventCall();
    }

    private void ForcedInventoryTextEventCall()
    {
        onInventoryChange();
    }
    

    public string BuildText()
    {
        string text = "INVENTORY";
        text += "\n" + healthItem + ": " + inventory[healthItem];
        for (int index = 0; index < itemNames.Length; index++)
            text += "\n" + itemNames[index] + ": " + inventory[itemNames[index]];
        return text;
    }

    private bool IsDeathCheck()
    {
        return inventory[healthItem] == 0;
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
            Debug.Log("key not found");
			System.Array.Resize(ref itemNames, itemNames.Length + 1);
			itemNames [itemNames.Length - 1] = s;
			inventory.Add(s, amount);
		}
        onInventoryChange();
	}

	public void Decrement(string s)
    {
        DecrementAmount(s, 1);
    }

	public void DecrementAmount(string s, int amount)
	{
		IncrementAmount (s, -amount);
	}

    public void DecreaseHealth()
    {
        Debug.Log("decrease health called");
        Decrement(healthItem);
        Debug.Log(GetItemAmount(healthItem));
    }

    public void DecreaseHealthAmount(int amount)
    {
        DecrementAmount(healthItem, amount);
    }

	public void SetAmount(string s, int amount)
	{
		inventory [s] = amount;
	}

    public int GetItemAmount(string s)
    {
        try
        {
            return inventory[s];
        }
        catch (KeyNotFoundException e)
        {
            Debug.Log("key not found");
            return 0;
        }
    }
}
