using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ZoomOutCamera : MonoBehaviour {

	public GameObject background;
	public int sceneOne;
	public float duration;
	float time;

	// Use this for initialization
	void Start () {
		time = 0;
		StartCoroutine (ZoomOut ());
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKeyDown('%'))
			SceneManager.LoadScene(sceneOne);*/
	}

	/*void OnMouseDown(){
		SceneManager.LoadScene(sceneOne);
	}*/

	IEnumerator ZoomOut()
	{
		Camera.main.orthographicSize += .015f;
		background.transform.localScale += new Vector3 (.005f, .005f, 0);
		time += Time.deltaTime;
		yield return null;
		if (time < duration)
			StartCoroutine(ZoomOut ());
		else
			SceneManager.LoadScene(sceneOne);
	}
}
