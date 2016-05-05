using UnityEngine;
using System.Collections;

public class TutorialAudioQueues : MonoBehaviour {

	AudioTrack audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioTrack> ();
		audio.PlayNextWhenReady ();
		StartCoroutine (playNext());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator playNext() {
		yield return new WaitUntil(()=> { return Input.GetMouseButtonDown(0); }); //wait until left click
		audio.PlayNextWhenReady();
		audio.PlayNextWhenReady ();
		yield return new WaitUntil(()=> {return Input.GetMouseButtonDown(1); }); //wait until right click
		audio.PlayNextWhenReady();
		audio.PlayNextWhenReady ();
		yield return new WaitUntil (()=> { return Input.GetMouseButtonDown(0) && Input.GetButton("Shift"); }); //wait until green
		audio.PlayNextWhenReady();
		audio.PlayNextWhenReady();
		yield return new WaitUntil (()=> { return Input.GetMouseButtonDown(1) && Input.GetButton("Shift"); }); //wait until yellow
		audio.autoPlayNext = true;
		yield return new WaitForSeconds (15f);
		GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PointToGoal> ().ToggleArrow (true);

	}
}
