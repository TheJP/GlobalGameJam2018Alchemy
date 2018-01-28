using GlobalGameJam2018Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public GameObject herb;
    public GameObject liquid;
    public GameObject gold;
    public GameObject others;

    public void HideAll()
    {
        herb?.SetActive(false);
        liquid?.SetActive(false);
        gold?.SetActive(false);
        others?.SetActive(false);
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

    private void SetColour(GameObject gameObject, IngredientColour colour)
    {
        var newColour = GetColour(colour);
        foreach (var renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = new Material(renderer.material) { color = newColour };
        }
    }

    public void Display(IItem item)
    {
        HideAll();
        if (item == null) { return; }
        if (item is MoneyMaker)
        {
            gold?.SetActive(true);
        }
        else if (item is Ingredient)
        {
            var ingredient = item as Ingredient;
            switch (ingredient.Type)
            {
                case ItemType.Herb:
                    herb?.SetActive(true);
                    SetColour(herb, ingredient.Colour);
                    break;
                case ItemType.Liquid:
                    liquid?.SetActive(true);
                    SetColour(liquid, ingredient.Colour);
                    break;
                default:
                    others?.SetActive(true);
                    SetColour(others, ingredient.Colour);
                    break;
            }
        }
        else
        {
            others?.SetActive(true);
        }
    }
}
