using UnityEngine;
using System.Collections;

public class AudioTrack : MonoBehaviour {

	public AudioClip[] clip;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		//StartCoroutine (playAudio());
	}
	
	// Update is called once per frame
	void Update () {
		audio.clip = clip[0];
		audio.Play();
		yield return WaitUntil (!audio.isPlaying);
		audio.clip = clip[1];
	}

	//IEnumerator playAudio() {
	//	audio.clip = clip[0];
	//	audio.Play();
	//	yield return WaitUntil (!audio.isPlaying);
	//	audio.clip = clip[1];
	//}
}
