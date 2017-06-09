using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

	public int scene;

	public void LoadScene()
	{
		SceneManager.LoadScene(scene);
	}
}
