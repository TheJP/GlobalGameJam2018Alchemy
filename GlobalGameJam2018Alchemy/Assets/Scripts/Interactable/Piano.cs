using System.Collections;
using System.Collections.Generic;
using GlobalGameJam2018Networking;
using UnityEngine;

public class Piano : MonoBehaviour, IInteractable {

    public AudioClip[] myAudioClips;
    private AudioSource audioSource;

    public bool CanInteract(IItem item)
    {
        return true;
    }

    //Playing Music
    public IItem GetItem()
    {
        PlayMusic();
        return null;
    }

    //Playing Music
    public bool PutItem(IItem item)
    {
        PlayMusic();
        return false;
    }

    public void PlayMusic() {
        int test = (int)Mathf.Floor(Random.Range(0.0f, (float)myAudioClips.Length + 0.99f));
        Debug.Log(test);
        audioSource.PlayOneShot(myAudioClips[test]);
    }

    // Use this for initialization
    void Start () {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        PlayMusic();
    }
	

	// Update is called once per frame
	void Update () {
	}
}
