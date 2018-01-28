using System.Collections;
using System.Collections.Generic;
using System.Text;
using GlobalGameJam2018Networking;
using UnityEngine;

public class RecipeBook : MonoBehaviour, IInteractable
{

    RecipeCreator RecipeCreator;

    /// <summary>
    /// The RecipeBook should be always interactable
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CanInteract(IItem item)
    {
        return true;
    }

    /// <summary>
    /// Has to do the same as PutItem
    /// </summary>
    public IItem GetItem()
    {
        PutItem(null);
        return null;
    }

    /// <summary>
    /// Returns true, when operation is successfull
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool PutItem(IItem item)
    {
        DisplayRecipes();
        return false;
    }

    public void createRecipes() {
        this.RecipeCreator = new RecipeCreator();

        StringBuilder sbDestill = new StringBuilder();
        sbDestill.Append("Destill Recipes : ");
        sbDestill.AppendLine();
        sbDestill.Append(this.RecipeCreator.MyToString(this.RecipeCreator.DestillRecipes));
        sbDestill.AppendLine();

        StringBuilder sbMortar = new StringBuilder();
        sbMortar.Append("Mortar Recipes : ");
        sbMortar.AppendLine();
        sbMortar.Append(this.RecipeCreator.MyToString(this.RecipeCreator.MortarRecipes));
        sbMortar.AppendLine();

        StringBuilder sbTea = new StringBuilder();
        sbTea.Append("Tea Recipes : ");
        sbTea.AppendLine();
        sbTea.Append(this.RecipeCreator.MyToString(this.RecipeCreator.TeaRecipes));
        sbTea.AppendLine();

        StringBuilder sbBaking = new StringBuilder();
        sbBaking.Append("Baking Oven Recipes :");
        sbBaking.AppendLine();
        sbBaking.Append(this.RecipeCreator.MyToString(this.RecipeCreator.BakeRecipes));
        sbBaking.AppendLine();

        StringBuilder sbCauldron = new StringBuilder();
        sbCauldron.Append("Cauldron Recipes : ");
        sbCauldron.AppendLine();
        sbCauldron.Append(this.RecipeCreator.MyToString(this.RecipeCreator.CauldronRecipes));
        sbCauldron.AppendLine();
    }

    /// <summary>
    /// Called by Interaction with RecipeBook, user can escape with space or it will go away after a time.
    /// </summary>
    public void DisplayRecipes()
    {

    
    }


    void Start()
    {
        createRecipes();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
