using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchemist : MonoBehaviour
{
    [Tooltip("Noises that the alchemists makes while doing his alchemystical things.")]
    public SoundPlayer oldManSounds;

    public ItemDisplay itemDisplay;
    private Quaternion itemRotation;

    private IItem currentItem = null;
    private IItem CurrentItem
    {
        get { return currentItem; }
        set { currentItem = value; itemDisplay?.Display(value); }
    }

    private void Start() => itemRotation = itemDisplay.transform.rotation;

    private void Update()
    {
        itemDisplay.transform.rotation = itemRotation;
        if (Input.GetButtonUp("Jump")) { oldManSounds?.PlaySound(); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonUp("Jump"))
        {
            IInteractable bench = other.GetComponentInParent<IInteractable>();

            if (CurrentItem == null)
            {
                CurrentItem = bench.GetItem();
            }
            else if (bench.CanInteract(CurrentItem))
            {
                if (bench.PutItem(CurrentItem))
                {
                    CurrentItem = null;
                }
            }
        }
    }
}
