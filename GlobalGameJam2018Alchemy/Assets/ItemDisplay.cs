using GlobalGameJam2018Networking;
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

    public void Display(IItem item)
    {
        HideAll();
        if(item == null) { return; }
        if(item is MoneyMaker)
        {
            gold?.SetActive(true);
        }
        else if (item is Ingredient)
        {
            var ingredient = item as Ingredient;
            switch (ingredient.Type)
            {
                case ItemType.Herb:
                    herb?.SetActive(true); // TODO: set color
                    break;
                case ItemType.Liquid:
                    liquid?.SetActive(true); // TODO: set color
                    break;
                default:
                    others?.SetActive(true); // TODO: set color
                    break;
            }
        }
        else
        {
            others?.SetActive(true);
        }
    }
}
