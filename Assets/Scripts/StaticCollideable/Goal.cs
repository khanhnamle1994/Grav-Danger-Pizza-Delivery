using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Goal : MonoBehaviour {

	public Text winText;
	public string whatToSay;
    public string sceneName="";
<<<<<<< HEAD
	public AudioClip[] yays;
=======
    public string[] requiredIngredients;
    public int[] requiredIngredientsAmounts;

    private PizzaInventory pi;
>>>>>>> origin/master

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
			PlayYay ();
            StartCoroutine("DelayedLoadNextLevel");
		}
	}

<<<<<<< HEAD
	void PlayYay()
	{
		int choice = Random.Range (0, yays.Length);
		gameObject.GetComponent<AudioSource> ().clip = yays [choice];
		gameObject.GetComponent<AudioSource> ().Play ();
	}
=======
    bool IngredientChecker()
    {
        throw new System.Exception("implementing");
        for (int i =0; i <requiredIngredients.Length;i++ )
        {
            
        }
        return true;
    }
>>>>>>> origin/master

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
