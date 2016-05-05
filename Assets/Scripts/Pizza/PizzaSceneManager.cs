using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;


public class PizzaSceneManager : MonoBehaviour {

    public string nextSceneName;
    private delegate void SceneChangeFunction();


    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PizzaInventory pi = player.GetComponent<PizzaInventory>();

        pi.onPlayerDeath += () =>
        {
            StartCoroutine("WaitFunction", ResetLevelDelegate());
        };


        GameObject finish = GameObject.FindGameObjectWithTag("Finish");
        Goal goal = finish.GetComponent<Goal>();

        goal.onPlayerWin += () =>
        {
            StartCoroutine("WaitFunction", LoadNextSceneDelegate());
        };

    }

    IEnumerator WaitFunction(SceneChangeFunction func)
    {
        yield return new WaitForSeconds(5);
        func();
    }
    

    private SceneChangeFunction ResetLevelDelegate()
    {
        return ()=>{ ResetLevel(); };
    }

    private void ResetLevel()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }

    private SceneChangeFunction LoadNextSceneDelegate()
    {
        return () =>{ LoadNextScene(); };
    }

    private void LoadNextScene()
    {
        LoadNextSceneCalled(nextSceneName);
    }

    public void LoadNextSceneCalled(string sceneName)
    {
        EditorSceneManager.LoadScene(sceneName);
    }

    public void LoadSceneIndex(int sceneIndex)
    {
        EditorSceneManager.LoadScene(sceneIndex);
    }

    public void SetNextScene(string s)
    {
        nextSceneName = s;
    }
}
