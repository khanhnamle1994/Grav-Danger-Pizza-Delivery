using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Goal : MonoBehaviour {

	public Text winText;
    public Text ingredientText;
	public string whatToSay;
    public string sceneName="";

    public AudioClip[] yays;

    public string[] requiredIngredients;
    public int[] requiredIngredientsAmounts;
    public bool isCheckIngredients = false;

    private PizzaInventory pi;

	// Use this for initialization
	void Start () {
        if (winText==null)
        {
            GameObject go = GameObject.Find("WinText");
            if (go == null)
                throw new UnityException("WinText not found for goal");
            winText = go.GetComponent<Text>();
        }
		winText.text = "";

        if(pi==null)
        {
            pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PizzaInventory>();
        }

        if(ingredientText==null && isCheckIngredients)
        {
            ingredientText = GameObject.Find("IngredientsText").GetComponent<Text>();
        }
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
		if(other.tag == "Player" && IngredientsChecker())
		{
			winText.text = whatToSay;
			PlayYay ();
            StartCoroutine("DelayedLoadNextLevel");
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
        if (!isCheckIngredients)
            return true;

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

    // Check if player has requiredIngredient Amount for ingredient Index
    // return false otherwise
    bool IngredientCheck(int ingredientIndex)
    {
        string ingredientName = requiredIngredients[ingredientIndex];
        return pi.GetItemAmount(ingredientName) >= requiredIngredientsAmounts[ingredientIndex];
    }

    IEnumerator DelayedLoadNextLevel()
    {
        if (sceneName == "")
            throw new UnityException("sceneName not set. will load nothing");
        yield return new WaitForSeconds(5);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PizzaSceneManager>().LoadNextScene();
    }
}
