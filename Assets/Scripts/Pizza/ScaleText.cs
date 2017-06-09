using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleText : MonoBehaviour {

	public int textSetSize;
	int textSize, boxWidth, boxHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		textSize = (int) (Screen.height / 400f * textSetSize);
		boxWidth = (int) (Screen.height * .95);
		boxHeight = (int) (Screen.width * .95);
		gameObject.GetComponent<Text> ().fontSize = textSize;
		gameObject.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, boxHeight);
		gameObject.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, boxWidth);
	}
}
