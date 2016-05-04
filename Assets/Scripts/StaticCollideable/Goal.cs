using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Goal : MonoBehaviour {

	public Text winText;
	public string whatToSay;
    public string sceneName="";

	// Use this for initialization
	void Start () {
        if (winText==null)
        {
            GameObject go = GameObject.Find("WinText");
            winText = go.GetComponent<Text>();
        }
		winText.text = "";
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
        return false;
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
