using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;


public class PizzaSceneManager : MonoBehaviour {

    public string nextSceneName;

	public void ResetLevel()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }

    public void LoadNextScene()
    {
        LoadNextSceneCalled(nextSceneName);
    }

    public void LoadNextSceneCalled(string sceneName)
    {
        EditorSceneManager.LoadScene(sceneName);
    }
}
