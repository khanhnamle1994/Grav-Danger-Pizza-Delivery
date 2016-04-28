using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Goal : MonoBehaviour {

	public Text winText;
	public string whatToSay;


	// Use this for initialization
	void Start () {
		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player")
		{
			winText.text = whatToSay;
		}
	}
}
