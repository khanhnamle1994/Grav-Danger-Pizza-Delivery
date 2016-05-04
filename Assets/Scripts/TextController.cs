using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {
    Text inventoryText;
    Text ingredientText;

    public delegate string FetchText();

    // Use this for initialization
    void Start () {
        inventoryText = GameObject.Find("InventoryText").GetComponent<Text>();
        ingredientText = GameObject.Find("IngredientsText").GetComponent<Text>();
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
        if(goal.isCheckIngredients)
            pi.onInventoryChange += IngredientSet;

    }



    public void SetTextFor(Text text, string textString)
    {
        text.text = textString;
    }

    void OnDestroy()
    {
        
    }

}
