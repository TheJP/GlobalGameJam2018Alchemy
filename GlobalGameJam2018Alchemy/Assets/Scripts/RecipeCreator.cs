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
    private Recipe[] createRandomRecipes()
    {
        return null;
    }


    //Creating Recipes
    Recipe createRecipe()
    {

        List<Item> inList = new List<Item>
        {
            new Item(Item.Type.coldLiquid, Item.Element.blue)
        };
        return new Recipe(inList, new Item(Item.Type.coldLiquid, Item.Element.green), 5);

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
