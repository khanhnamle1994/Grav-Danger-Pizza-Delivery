using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class GoToScene : MonoBehaviour {

	public int scene;

	public void LoadScene()
	{
		EditorSceneManager.LoadScene(scene);
	}
}
