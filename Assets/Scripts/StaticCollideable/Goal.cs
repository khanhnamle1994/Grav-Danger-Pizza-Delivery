using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	public Text winText;
	public string whatToSay;
    public string sceneName="";

    public AudioClip[] yays;

    public string[] requiredIngredients;
    public int[] requiredIngredientsAmounts;
    [SerializeField]
    private bool isCheckIngredients;
    public bool IsCheckIngredients
    {
        get
        {
            return isCheckIngredients;
        }
        set
        {
            isCheckIngredients = value;
        }
    }

    private PizzaInventory pi;

    public delegate void OnPlayerWin();
    public event OnPlayerWin onPlayerWin = delegate { };

    void Awake()
    {
        if (pi == null)
        {
            pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PizzaInventory>();
        }
    }

    // Use this for initialization
    void Start () {
        if (winText==null)
        {
            GameObject go = GameObject.Find("CenterText");
            if (go == null)
                throw new UnityException("WinText not found for goal");
            winText = go.GetComponent<Text>();
        }
		winText.text = "";
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    

    public string BuildText()
    {
        string text = "REQ. Ingredients";
        for (int index = 0; index < requiredIngredients.Length; index++)
        {
            string currentItem = requiredIngredients[index];
            text += "\n" + currentItem + ": " + pi.GetItemAmount(currentItem) + "/" + requiredIngredientsAmounts[index];
        }
        return text;
    }

    void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player")
		{
            if (IsCheckIngredients)
            {
                // if ingrediendts not fulfilled
                // then not win
                if (!IngredientsChecker())
                    return;
            }

			PlayYay ();
            onPlayerWin();
        }
	}

	void PlayYay()
	{
		int choice = Random.Range (0, yays.Length);
		gameObject.GetComponent<AudioSource> ().clip = yays [choice];
		gameObject.GetComponent<AudioSource> ().Play ();
	}

    bool IngredientsChecker()
    {

        for (int i =0; i <requiredIngredients.Length;i++ )
        {
            // if player doesn't have ingredient amount for item
            // then return false
            if (!IngredientCheck(i))
                return false;
        }
        // return true if passed all checks
        return true;
    }

    public List<string> RemainingIngredients()
    {
        List<string> requirements = new List<string>();
        for (int i = 0; i < requiredIngredients.Length; i++)
        {
            // if player doesn't have ingredient amount for item
            // then return false
            if (!IngredientCheck(i))
                requirements.Add(requiredIngredients[i]);
        }
        return requirements;
    }

    // Check if player has requiredIngredient Amount for ingredient Index
    // return false otherwise
    bool IngredientCheck(int ingredientIndex)
    {
        string ingredientName = requiredIngredients[ingredientIndex];
        return pi.GetItemAmount(ingredientName) >= requiredIngredientsAmounts[ingredientIndex];
    }
}
