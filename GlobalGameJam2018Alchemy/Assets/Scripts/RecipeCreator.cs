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

        new Ingredient();

        //var inList = new List<IItem>
        //{
        //    new IItem(IItem.Type.coldLiquid, IItem.Element.blue)
        //};
        //return new Recipe(inList, new Item(Item.Type.coldLiquid, Item.Element.green), 5);
        throw new NotImplementedException();
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
