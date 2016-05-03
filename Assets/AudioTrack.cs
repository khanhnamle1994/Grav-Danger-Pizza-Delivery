using UnityEngine;
using System.Collections;

public class AudioTrack : MonoBehaviour {

	public AudioClip[] audioClip;

	// Use this for initialization
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		for (int i = 0; i < audioClip.Length; i++) {
			audio.clip = audioClip [i];
			audio.Play();

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
