using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {
    Text inventoryText;
    Text ingredientText;
    Text centerText;


    public delegate string OnCenterText();
    public event OnCenterText onCenterText;

    public string deathString = "You died";


    // Use this for initialization
    void Start () {
        inventoryText = GameObject.Find("InventoryText").GetComponent<Text>();
        ingredientText = GameObject.Find("IngredientsText").GetComponent<Text>();
        centerText = GameObject.Find("CenterText").GetComponent<Text>();


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PizzaInventory pi = player.GetComponent<PizzaInventory>();
        Goal goal = GameObject.FindGameObjectWithTag("Finish").GetComponent<Goal>();

        PizzaInventory.OnInventoryChange InventorySet = () =>
        {
            SetTextFor(inventoryText, pi.BuildText());
        };

        PizzaInventory.OnInventoryChange IngredientSet = () =>
        {
            SetTextFor(ingredientText, goal.BuildText());
        };

        pi.onInventoryChange += InventorySet;
        if(goal.IsCheckIngredients)
            pi.onInventoryChange += IngredientSet;

        Goal.OnPlayerWin WinSet = () =>
        {
            SetTextFor(centerText, goal.whatToSay);
        };

        goal.onPlayerWin += WinSet;

        PizzaInventory.OnPlayerDeath DeathSet = () =>
        {
            SetTextFor(centerText, deathString);
        };
        pi.onPlayerDeath += DeathSet;

    }



    public void SetTextFor(Text text, string textString)
    {
        text.text = textString;
    }

    void OnDestroy()
    {
        
    }

}
