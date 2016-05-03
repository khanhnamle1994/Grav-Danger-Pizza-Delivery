using UnityEngine;
using System.Collections;

public class AudioTrack : MonoBehaviour {

	public AudioClip[] clips;
    AudioSource audio;
    

    public bool willPlayOnStart=true;
    public bool autoPlayNext = false;

    private int currentClipIndex;

    private bool shouldPlayNext = false;
    
    private AudioClip nextClip;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
        if (clips.Length == 0)
            throw new UnityException("Zero audio clips found");
        audio.clip = clips[0];
        currentClipIndex = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
        // if audio isn't playing
		if(!audio.isPlaying)
        {
            // if on first clip and will play on start
            if(currentClipIndex==0 && willPlayOnStart)
            {
                audio.Play();
                willPlayOnStart = false; // ensure if only clip, won't play repeatedly
                TryLoadNextClip();
                shouldPlayNext = false;
            }
               
            // if shouldPlayNext true..
            else if(shouldPlayNext || autoPlayNext)
            {
                // if reached end, stop auto play
                if (currentClipIndex == (clips.Length - 1))
                    autoPlayNext = false;


                // play audio and load the next clip
                audio.clip = nextClip;
                audio.Play();
                TryLoadNextClip();
                shouldPlayNext = false;
            }

        }
	}
    
    public void PlayNextWhenReady()
    {
        shouldPlayNext = true;
    }

    public void ForcePlay(int clipIndex)
    {
        ForceSetClip(clipIndex);
        audio.Play();
    }

    public void ForceSetClip(int clipIndex)
    {
        CheckClipIndex(clipIndex);

        audio.Stop();
        audio.clip = clips[clipIndex];
        currentClipIndex = clipIndex;
    }

    private void TryLoadNextClip()
    {
        int nextClipIndex = currentClipIndex + 1;
        if (nextClipIndex < clips.Length)
            LoadNextClip();
        else
            nextClip = null;
    }

    private void LoadNextClip()
    {
        currentClipIndex++;
        nextClip = clips[currentClipIndex];
    }

    private void CheckClipIndex(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= clips.Length)
            throw new UnityException("ClipIndex passed in out of range");
    }
}
