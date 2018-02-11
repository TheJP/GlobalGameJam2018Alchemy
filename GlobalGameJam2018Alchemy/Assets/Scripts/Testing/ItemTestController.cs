using System;
using System.Linq;
using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTestController : MonoBehaviour
{
    private const int START_Y = 7;
    private const int START_X = -5;
    private readonly Quaternion rotationTop = Quaternion.AngleAxis(90, Vector3.right);

    public GameObject ItemDisplay;
    public Camera mainCamera;
    public float speed = 0.7f;

    public Color green;

    private ItemType[] items = new[] { ItemType.Herb, ItemType.Liquid, ItemType.Powder, ItemType.Steam };
    private IngredientColour[] coloursIng = new[]
    {
        IngredientColour.Yellow,
        IngredientColour.Orange,
        IngredientColour.Green,

        IngredientColour.Red,
        IngredientColour.Violet,
        IngredientColour.Blue,

        IngredientColour.Black
    };

    private ProcessedItem.ProcessedItemType[] itemsProcessed = (ProcessedItem.ProcessedItemType[])Enum.GetValues(typeof(ProcessedItem.ProcessedItemType));
    private ProcessedItem.ProcessedItemColor[] coloursProcessed = new[]
    {
        ProcessedItem.ProcessedItemColor.Yellow,
        ProcessedItem.ProcessedItemColor.Orange,
        ProcessedItem.ProcessedItemColor.Green,

        ProcessedItem.ProcessedItemColor.Red,
        ProcessedItem.ProcessedItemColor.Violet,
        ProcessedItem.ProcessedItemColor.Blue,

        ProcessedItem.ProcessedItemColor.Black
    };

    // Use this for initialization
    void Start()
    {
        int curY = START_Y;
        for (int i = 0; i < items.Length; i++)
        {
            for (int j = 0; j < coloursIng.Length; j++)
            {
                ItemDisplay display = Instantiate(ItemDisplay, new Vector3(j + START_X + 2, 0, curY), rotationTop).GetComponent<ItemDisplay>();
                display.Display(new Ingredient(items[i], coloursIng[j]));
            }
            curY--;
        }
        for (int i = 0; i < itemsProcessed.Length; i++)
        {
            for (int j = 0; j < coloursProcessed.Length; j++)
            {
                ItemDisplay display = Instantiate(ItemDisplay, new Vector3(j + START_X + 2, 0, curY), rotationTop).GetComponent<ItemDisplay>();
                display.Display(new ProcessedItem(itemsProcessed[i], coloursProcessed[j]));
            }
            curY--;
        }

        {
            ItemDisplay display = Instantiate(ItemDisplay, new Vector3(START_X + 2, 0, curY), rotationTop).GetComponent<ItemDisplay>();
            display.Display(new MoneyMaker("Cat Wizard", int.MaxValue, ItemType.Processed));
            curY--;
        }

        StartCoroutine(ColourChanging());
        StartCoroutine(ItemChanging());
        StartCoroutine(BothChanging());
        StartCoroutine(Blinking());
    }

    // Update is called once per frame
    void Update()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveDirection *= speed;
        mainCamera.transform.Translate(moveDirection);
    }

    IEnumerator<YieldInstruction> ColourChanging()
    {
        var displayIng = items
            .Select((item, i) => new { item, display = Instantiate(ItemDisplay, new Vector3(START_X, 0, START_Y - i), rotationTop).GetComponent<ItemDisplay>() })
            .ToArray();
        var displayProc = itemsProcessed
            .Select((item, i) => new { item, display = Instantiate(ItemDisplay, new Vector3(START_X, 0, START_Y - i - items.Length), rotationTop).GetComponent<ItemDisplay>() })
            .ToArray();
        int colourIndex = 0;

        while (true)
        {
            foreach (var elem in displayIng)
            {
                elem.display.Display(new Ingredient(elem.item, coloursIng[colourIndex]));
            }
            foreach (var elem in displayProc)
            {
                elem.display.Display(new ProcessedItem(elem.item, coloursProcessed[colourIndex]));
            }
            colourIndex++;
            if(colourIndex >= coloursIng.Length)
            {
                colourIndex = 0;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator<YieldInstruction> ItemChanging()
    {

        ItemDisplay[] displays = new ItemDisplay[coloursIng.Length];
        for (int j = 0; j < coloursIng.Length; j++)
        {
            displays[j] = Instantiate(ItemDisplay, new Vector3(j + START_X + 2, 0, START_Y + 2), rotationTop).GetComponent<ItemDisplay>();
        }
        int maxItems = items.Length + itemsProcessed.Length;
        int itemIndex = 0;

        while (true)
        {
            for (int j = 0; j < coloursIng.Length; j++)
            {
                IItem item;
                if(itemIndex < items.Length)
                {
                    item = new Ingredient(items[itemIndex], coloursIng[j]);
                }
                else
                {
                    item = new ProcessedItem(itemsProcessed[itemIndex-items.Length], coloursProcessed[j]);
                }
                displays[j].Display(item);
            }

            itemIndex++;
            if (itemIndex >= maxItems)
            {
                itemIndex = 0;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator<YieldInstruction> BothChanging()
    {
        ItemDisplay display = Instantiate(ItemDisplay, new Vector3(START_X, 0, START_Y + 2), rotationTop).GetComponent<ItemDisplay>();
        int maxItems = items.Length + itemsProcessed.Length;
        int itemIndex = 0;
        int colourIndex = 0;

        while (true)
        {
            IItem item;
            if (itemIndex < items.Length)
            {
                item = new Ingredient(items[itemIndex], coloursIng[colourIndex]);
            }
            else if(itemIndex < maxItems)
            {
                item = new ProcessedItem(itemsProcessed[itemIndex - items.Length], coloursProcessed[colourIndex]);
            }
            else
            {
                item = new MoneyMaker("Cat Wizard", int.MaxValue, ItemType.Processed);
                colourIndex--;
            }
            display.Display(item);

            colourIndex++;
            if (colourIndex >= coloursIng.Length)
            {
                colourIndex = 0;
            }
            itemIndex++;
            if (itemIndex >= maxItems)
            {
                itemIndex = 0;
            }
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator<YieldInstruction> Blinking()
    {
        ItemDisplay display = Instantiate(ItemDisplay, new Vector3(START_X + 1, 0, START_Y + 1), rotationTop).GetComponent<ItemDisplay>();
        var item = new MoneyMaker("Cat Wizard", int.MaxValue, ItemType.Processed);

        while (true)
        {
            display.Display(item);
            yield return new WaitForSeconds(0.5f);
            display.HideAll();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
