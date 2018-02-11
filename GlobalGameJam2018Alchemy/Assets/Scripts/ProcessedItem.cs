using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessedItem : IItem
{


    public enum ProcessedItemType
    {
        Herbessence,
        Slimeessence,
        Powderessence,
        Steamessence,

        Fairiedust,
        Lightifier,
        Fireflies,
        Glowstone,
        Energy,
        HerbPowder
    }

    public enum ProcessedItemColor
    {
        Black,
        Blue,
        Green,
        Orange,
        Yellow,
        Violet,
        Red
    }

    public ProcessedItemType ProcessedType
    {
        private set;
        get;
    }
    public ProcessedItemColor ProcessedColor
    {
        private set;
        get;
    }

    public ProcessedItem(ProcessedItemType type, ProcessedItemColor color)
    {
        this.ProcessedType = type;
        this.ProcessedColor = color;
    }

    public ItemType Type => ItemType.Processed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
