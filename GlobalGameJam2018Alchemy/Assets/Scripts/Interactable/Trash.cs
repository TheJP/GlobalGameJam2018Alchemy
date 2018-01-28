using System.Collections;
using System.Collections.Generic;
using GlobalGameJam2018Networking;
using UnityEngine;

public class Trash : MonoBehaviour, IInteractable
{
    public GameObject ActiveAnimationObject;

    private IItem currentThrash = null;

    public bool CanInteract(IItem item)
    {
        return item != null && currentThrash == null;
    }

    public IItem GetItem()
    {
        return null;
    }

    public bool PutItem(IItem item)
    {
        if (CanInteract(item))
        {
            currentThrash = item;
            Invoke("OnFinish", 5);
            StartAnimation(Color.black);
            //remove item
            return true;
        }
        return false;
    }

    private void OnFinish()
    {
        currentThrash = null;
        StopAnimation();
    }

    private void SetColour(GameObject gameObject, Color colour)
    {
        foreach (var particleSystem in gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            Renderer renderer = particleSystem.GetComponent<Renderer>();
            renderer.material = new Material(renderer.material) { color = colour };
        }
    }

    private void StartAnimation(Color colour)
    {
        ActiveAnimationObject?.SetActive(true);
        ActiveAnimationObject?.GetComponentInChildren<ParticleSystem>()?.Play();
        SetColour(ActiveAnimationObject, colour);
    }

    private void StopAnimation()
    {
        ActiveAnimationObject?.GetComponentInChildren<ParticleSystem>()?.Stop();
        //ActiveAnimationObject?.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
