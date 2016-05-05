using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


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

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private SceneChangeFunction LoadNextSceneDelegate()
    {
        return () =>{ LoadNextScene(); };
    }

    public void ForceFindLoadNextScene()
    {
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex+1;
        LoadNextSceneCalled(SceneManager.GetSceneAt(nextBuildIndex).name);
    }

    public void LoadNextScene()
    {
        LoadNextSceneCalled(nextSceneName);
    }

    public void LoadNextSceneCalled(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SetNextScene(string s)
    {
        nextSceneName = s;
    }
}
