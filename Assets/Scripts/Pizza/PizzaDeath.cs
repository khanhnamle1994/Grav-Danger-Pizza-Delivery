using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;


public class PizzaDeath : MonoBehaviour {

	public void ResetLevel()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().name);
    }
}
