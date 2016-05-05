using UnityEngine;
using System.Collections;

public class ScreenRoll : MonoBehaviour {

    public GameObject theCamera;
    public float speed;
    public string level; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        theCamera.transform.Translate(Vector3.down * Time.deltaTime * speed);
        StartCoroutine(waitfor());

	
	}

    IEnumerator waitfor()
    {
        yield return new WaitForSeconds(60);
        Application.LoadLevel(level);
    }
}
