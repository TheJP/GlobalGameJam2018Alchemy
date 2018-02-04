using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource source;

    private void Start() => source = GetComponent<AudioSource>();

    public void PlaySound() => source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
}
