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
    public List<Recipe> BakeRecipes
    {
        get;
        private set;
    }

    /// <summary>
    /// Recipes for destillation
    /// </summary>
    public List<Recipe> DestillRecipes
    {
        get;
        private set;
    }

    /// <summary>
    /// Recipes for destillation
    /// </summary>
    public List<Recipe> CauldronRecipes
    {
        get;
        private set;
    }

    /// <summary>
    /// Recipes for mixxing
    /// </summary>
    public List<Recipe> MortarRecipes
    {
        get;
        private set;
    }

    /// <summary>
    /// Recipes for Teas, gold source
    /// </summary>
    public List<Recipe> TeaRecipes
    {
        get;
        private set;
    }


    RecipeCreator()
    {
        //myRecipes = createRandomRecipes();

    }

    //Creating RandomRecipes, is calling createRecipe
    private Recipe[] CreateRandomRecipes()
    {
        return null;
    }


    //Creating Recipes
    public Recipe CreateRecipe()
    {
        var inList = new List<IItem> {
            new Ingredient(ItemType.Liquid, IngredientColour.Black)
        };
        

        IItem outItem = (IItem) new MoneyMaker("Chocolate", 100);

        return new Recipe(inList, outItem, 5);

    }
     


}
