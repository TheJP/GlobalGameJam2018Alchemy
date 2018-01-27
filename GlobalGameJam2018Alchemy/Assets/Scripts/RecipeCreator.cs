using Assets.Scripts.ItemSignatures;
using GlobalGameJam2018Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RecipeCreator
{
    /// <summary>
    /// ALL THE RECIPES
    /// </summary>
    private List<Recipe> MyRecipes;

    /// <summary>
    /// Recipes for baking
    /// </summary>
    public List<Recipe> BakeRecipes { get; } = new List<Recipe>();

    /// <summary>
    /// Recipes for destillation
    /// </summary>
    public List<Recipe> DestillRecipes
    {
        get;
    } = new List<Recipe>();

    /// <summary>
    /// Recipes for destillation
    /// </summary>
    public List<Recipe> CauldronRecipes
    {
        get;
    } = new List<Recipe>();

    /// <summary>
    /// Recipes for mixxing
    /// </summary>
    public List<Recipe> MortarRecipes
    {
        get;
    } = new List<Recipe>();

    /// <summary>
    /// Recipes for Teas, gold source
    /// </summary>
    public List<Recipe> TeaRecipes
    {
        get;
    } = new List<Recipe>();


    public RecipeCreator()
    {
        //myRecipes = createRandomRecipes();

    }

    //Creating RandomRecipes, is calling createRecipe
    private Recipe[] CreateRandomRecipes()
    {
        return null;
    }

    private void CreateStartRecipes()
    {
        foreach(IngredientColour colour in Enum.GetValues(typeof(IngredientColour)))
        {
            #region hush_nothing_here
            ProcessedItem.ProcessedItemColor otherColour = (ProcessedItem.ProcessedItemColor)Enum.Parse(typeof(ProcessedItem.ProcessedItemColor), colour.ToString());
            #endregion

            MortarRecipes.Add(
                new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Herb, colour) }, 
                () => new ProcessedItem(ProcessedItem.ProcessedItemType.HerbPowder, otherColour),
                5));
            DestillRecipes.Add(
                new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Liquid, colour) },
                () => new ProcessedItem(ProcessedItem.ProcessedItemType.Slimeessence, otherColour),
                5));
            DestillRecipes.Add(
                new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Powder, colour) },
                () => new ProcessedItem(ProcessedItem.ProcessedItemType.Powderessence, otherColour),
                5));
            DestillRecipes.Add(
                new Recipe(new List<ItemSignature> { new IngredientSignature(ItemType.Steam, colour) },
                () => new ProcessedItem(ProcessedItem.ProcessedItemType.Steamessence, otherColour),
                5));
            DestillRecipes.Add(
                new Recipe(new List<ItemSignature> { new ProcessedItemSignature(ProcessedItem.ProcessedItemType.HerbPowder, otherColour) },
                () => new ProcessedItem(ProcessedItem.ProcessedItemType.Herbessence, otherColour),
                5));


        }
    }


    //Creating Recipes
    public Recipe CreateRecipe()
    {
        var inList = new List<ItemSignature>
        {
            new IngredientSignature(ItemType.Liquid, IngredientColour.Black)
        };

        // Code for processed Items:
        return new Recipe(inList, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Fairiedust, ProcessedItem.ProcessedItemColor.Black), 100);

        // Code for MoneMaker Items:
        return new Recipe(inList, () => new MoneyMaker("Chocolate", 100, ItemType.Processed), 5);
    }
     


}
