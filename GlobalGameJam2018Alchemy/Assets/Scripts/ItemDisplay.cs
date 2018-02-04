using GlobalGameJam2018Networking;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    [Header("Ingredients")]
    public GameObject powder;
    public GameObject herb;
    public GameObject liquid;
    public GameObject steam;

    [Header("Processed Items")]
    public GameObject herbPowder;
    public ParticleSystem essenceEffect;

    public GameObject fairiedust;
    public GameObject lightifier;
    public GameObject fireflies;
    public GameObject glowstone;
    public GameObject energy;

    [Header("Other")]
    public GameObject gold;
    public GameObject others;

    public Color CurrentItemColor { get; private set; }

    private GameObject currentDisplay;
    private ParticleSystem currentEffect;

    public void HideAll()
    {
        currentDisplay?.SetActive(false);
        currentEffect?.Stop();
        currentDisplay = null;
        currentEffect = null;
        CurrentItemColor = Color.white;

        //herb?.SetActive(false);
        //liquid?.SetActive(false);
        //gold?.SetActive(false);
        //others?.SetActive(false);
    }

    private Color GetColour(IngredientColour colour)
    {
        switch (colour)
        {
            case IngredientColour.Black: return Color.black;
            case IngredientColour.Blue: return Color.blue;
            case IngredientColour.Green: return Color.green;
            case IngredientColour.Orange: return new Color(1f, 0.5f, 0f);
            case IngredientColour.Red: return Color.red;
            case IngredientColour.Violet: return new Color(0.5f, 0f, 1f);
            case IngredientColour.Yellow: return Color.yellow;
            default: throw new ArgumentException("Unknown colour");
        }
    }

    private IngredientColour GetOtherColour(ProcessedItem.ProcessedItemColor colour)
    {
        switch (colour)
        {
            case ProcessedItem.ProcessedItemColor.Black: return IngredientColour.Black;
            case ProcessedItem.ProcessedItemColor.Blue: return IngredientColour.Blue;
            case ProcessedItem.ProcessedItemColor.Green: return IngredientColour.Green;
            case ProcessedItem.ProcessedItemColor.Orange: return IngredientColour.Orange;
            case ProcessedItem.ProcessedItemColor.Red: return IngredientColour.Red;
            case ProcessedItem.ProcessedItemColor.Violet: return IngredientColour.Violet;
            case ProcessedItem.ProcessedItemColor.Yellow: return IngredientColour.Yellow;
            default: throw new ArgumentException("Unknown colour");
        }
    }

    public void Display(IItem item)
    {
        HideAll();
        if (item == null) { return; }
        if (item is MoneyMaker)
        {
            currentDisplay = gold;
            ShowItem();
        }
        else if (item is Ingredient)
        {
            var ingredient = item as Ingredient;
            switch (ingredient.Type)
            {
                case ItemType.Herb:
                    currentDisplay = herb;
                    break;
                case ItemType.Liquid:
                    currentDisplay = liquid;
                    break;
                case ItemType.Powder:
                    currentDisplay = powder;
                    break;
                case ItemType.Steam:
                    currentDisplay = steam;
                    break;
                default:
                    currentDisplay = others;
                    break;
            }
            ShowItem(ingredient.Colour);
        }
        else if (item is ProcessedItem)
        {
            var processedItem = item as ProcessedItem;
            switch (processedItem.ProcessedType)
            {
                case ProcessedItem.ProcessedItemType.HerbPowder:
                    currentDisplay = herbPowder;
                    break;

                case ProcessedItem.ProcessedItemType.Herbessence:
                    currentDisplay = herb;
                    currentEffect = essenceEffect;
                    break;
                case ProcessedItem.ProcessedItemType.Slimeessence:
                    currentDisplay = liquid;
                    currentEffect = essenceEffect;
                    break;
                case ProcessedItem.ProcessedItemType.Powderessence:
                    currentDisplay = powder;
                    currentEffect = essenceEffect;
                    break;
                case ProcessedItem.ProcessedItemType.Steamessence:
                    currentDisplay = steam;
                    currentEffect = essenceEffect;
                    break;

                case ProcessedItem.ProcessedItemType.Fairiedust:
                    currentDisplay = fairiedust;
                    break;
                case ProcessedItem.ProcessedItemType.Lightifier:
                    currentDisplay = lightifier;
                    break;
                case ProcessedItem.ProcessedItemType.Fireflies:
                    currentDisplay = fireflies;
                    break;
                case ProcessedItem.ProcessedItemType.Glowstone:
                    currentDisplay = glowstone;
                    break;
                case ProcessedItem.ProcessedItemType.Energy:
                    currentDisplay = energy;
                    break;
                default:
                    currentDisplay = others;
                    break;
            }

            currentDisplay?.SetActive(true);
            ShowItem(GetOtherColour(processedItem.ProcessedColor));
        }
        else
        {
            currentDisplay = others;
            ShowItem();
        }
    }

    private void ShowItem()
    {
        CurrentItemColor = Color.white;
        currentDisplay?.SetActive(true);
        //currentEffect?.Play();
    }

    private void ShowItem(IngredientColour ingredientColour)
    {
        ShowItem();
        CurrentItemColor = GetColour(ingredientColour);
        SetColour(currentDisplay);
        var itemEffect = currentDisplay?.GetComponent<ParticleSystem>();
        if(itemEffect != null)
        {
            itemEffect.startColor = CurrentItemColor;
        }
        if(currentEffect != null)
        {
            currentEffect.startColor = CurrentItemColor;
            currentEffect.Play();
        }
    }

    private void SetColour(GameObject gameObject)
    {
        foreach (var renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = new Material(renderer.material) { color = CurrentItemColor };
        }
    }
}
