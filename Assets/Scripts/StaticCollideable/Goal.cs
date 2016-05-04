using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Goal : MonoBehaviour {

	public Text winText;
	public string whatToSay;
    public string sceneName="";
    public string[] requiredIngredients;
    public int[] requiredIngredientsAmounts;

    private PizzaInventory pi;

	// Use this for initialization
	void Start () {
        if (winText==null)
        {
            GameObject go = GameObject.Find("WinText");
            winText = go.GetComponent<Text>();
        }
		winText.text = "";

        if(pi==null)
        {
            pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PizzaInventory>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player" && IngredientChecker())
		{
			winText.text = whatToSay;
            StartCoroutine("DelayedLoadNextLevel");
		}
	}

    bool IngredientChecker()
    {
        throw new System.Exception("implementing");
        for (int i =0; i <requiredIngredients.Length;i++ )
        {
            
        }
        return true;
    }

    IEnumerator DelayedLoadNextLevel()
    {
        if (sceneName == "")
            throw new UnityException("sceneName not set. will load nothing");
        yield return new WaitForSeconds(5);
        LoadNextLevel();
    }

    void LoadNextLevel()
    {
        EditorSceneManager.LoadScene(sceneName);
    }

}
