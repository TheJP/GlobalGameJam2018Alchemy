using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public Animator animator;

    private void OnMouseEnter() => animator.SetBool("Hovering", true);
    private void OnMouseExit() => animator.SetBool("Hovering", false);
    private void OnMouseUpAsButton() => AudioListener.pause = !AudioListener.pause;
}
