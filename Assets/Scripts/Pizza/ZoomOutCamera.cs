using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ZoomOutCamera : MonoBehaviour {

	public GameObject background;
	public int sceneOne;

	// Use this for initialization
	void Start () {
		StartCoroutine (ZoomOut ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		SceneManager.LoadScene(sceneOne);
	}

	IEnumerator ZoomOut()
	{
		Camera.main.orthographicSize += .015f;
		background.transform.localScale += new Vector3 (.005f, .005f, 0);
		yield return null;
		if (Camera.main.orthographicSize < 35f)
			StartCoroutine(ZoomOut ());
		else
			SceneManager.LoadScene(sceneOne);
	}
}
