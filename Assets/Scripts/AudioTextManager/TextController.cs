using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {
    Text inventoryText;
    Text ingredientText;
    Text centerText;
    Text helpText;


    //public delegate string OnCenterText();
    //public event OnCenterText onCenterText;

    string deathString = "Out of Pizza!";

    public delegate void OnHelp(string helpString);
    public event OnHelp onHelp;

    internal PizzaInventory pi;

    // Use this for initialization
    void Start () {
        inventoryText = GameObject.Find("InventoryText").GetComponent<Text>();
        ingredientText = GameObject.Find("IngredientsText").GetComponent<Text>();
        centerText = GameObject.Find("CenterText").GetComponent<Text>();
        helpText = GameObject.Find("HelpText").GetComponent<Text>();
        
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

        onHelp += (string helpString) =>
        {
            SetTextFor(helpText, helpString);
        };

    }

    internal void SetHelpText(string helpString)
    {
        onHelp(helpString);
    }

    public void SetTextFor(Text text, string textString)
    {
        text.text = textString;
    }

    void OnDestroy()
    {
        
    }

}
