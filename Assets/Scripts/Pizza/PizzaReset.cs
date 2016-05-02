using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;


public class PizzaReset : MonoBehaviour {

	public void ResetLevel()
    {
        EditorSceneManager.LoadScene(EditorSceneManager.GetActiveScene().ToString());
    }
}
