using GlobalGameJam2018Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RecipeCreator
{
    Recipe[] myRecipes;

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
    Recipe CreateRecipe()
    {
        var inList = new List<Ingredient> {
            new Ingredient(ItemType.Liquid, IngredientColour.Black)
        };
        return null;     
    }
     

    public List<Recipe> getBurnRecipes() {
        return null;
    }

    public List<Recipe> getCutRecipes() {
        return null;
    }

    public List<Recipe> getMixRecipes() {
        return null;
    }


}
