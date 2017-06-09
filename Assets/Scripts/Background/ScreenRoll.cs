using UnityEngine;
using System.Collections;

public class ScreenRoll : MonoBehaviour {

    public GameObject background;
    public float speed;
    public string level; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Camera.main.transform.Translate(Vector3.down * Time.deltaTime * speed);
		background.transform.Translate(Vector3.down * Time.deltaTime * speed * .75f);
        StartCoroutine(waitfor());

	
	}

    IEnumerator waitfor()
    {
        yield return new WaitForSeconds(64);
        Application.LoadLevel(level);
    }
}
