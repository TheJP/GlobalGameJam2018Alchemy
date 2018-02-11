using Assets.Scripts.ItemSignatures;
using GlobalGameJam2018Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTestController : PrefabLibraryBase
{
    public Chest chest;
    public GameObject HerbPrefab;
    public InteractivePipe Input;
    public Workbench[] Things;

    public override GameObject GetPrefab(IItem itemType)
    {
        return HerbPrefab;
    }

    public void Start ()
    {
        var builder = LevelConfig.Builder("blablabla");
        builder.AddPipe(PipeDirection.ToAlchemist, 0);
        var config = builder.Create();

        Input.Pipe = config.Pipes.First();
        Input.AddItem(new Ingredient(ItemType.Herb, IngredientColour.Green));

        var recipeHack = new List<Recipe>
        {
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Black) }, () =>  new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Blue), 5),
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Blue) }, () =>   new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Green), 5),
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Green) }, () =>  new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Orange), 5),
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Orange) }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Red), 5),
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Red) }, () =>    new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Violet), 5),
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Violet) }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Yellow), 5),
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Yellow) }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Black), 5),

            new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Black) }, () =>  new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Blue), 5),
            new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Blue) }, () =>   new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Green), 5),
            new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Green) }, () =>  new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Orange), 5),
            new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Orange) }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Red), 5),
            new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Red) }, () =>    new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Violet), 5),
            new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Violet) }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Yellow), 5),
            new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Yellow) }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, ProcessedItem.ProcessedItemColor.Black), 5),
        };

        for(int i = 0; i < Things.Length; i++)
        {
            Things[i].MyRecipes = recipeHack;
        }

        chest?.PutItem(new Ingredient(ItemType.Herb, IngredientColour.Blue));
        chest?.PutItem(new Ingredient(ItemType.Herb, IngredientColour.Violet));
    }
}
