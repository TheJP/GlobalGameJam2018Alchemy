﻿using Assets.Scripts.ItemSignatures;
using GlobalGameJam2018Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTestController : PrefabLibraryBase
{
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

        var recipeHack = new List<Recipe> {
            new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, IngredientColour.Green) },
            () => new Ingredient(ItemType.Herb, IngredientColour.Green),
            5)};

        for(int i = 0; i < Things.Length; i++)
        {
            Things[i].MyRecipes = recipeHack;
        }
	}
}
