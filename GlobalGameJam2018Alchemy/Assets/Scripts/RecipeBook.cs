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

        StringBuilder sb = new StringBuilder();
        sb.Append("Destill Recipes : ");
        sb.AppendLine();
        sb.Append(this.RecipeCreator.MyToString(this.RecipeCreator.DestillRecipes));
        sb.AppendLine();

        sb.Append("Mortar Recipes : ");
        sb.AppendLine();
        sb.Append(this.RecipeCreator.MyToString(this.RecipeCreator.MortarRecipes));
        sb.AppendLine();

        sb.Append("Tea Recipes : ");
        sb.AppendLine();
        sb.Append(this.RecipeCreator.MyToString(this.RecipeCreator.TeaRecipes));
        sb.AppendLine();

        sb.Append("Baking Oven Recipes :");
        sb.AppendLine();
        sb.Append(this.RecipeCreator.MyToString(this.RecipeCreator.BakeRecipes));
        sb.AppendLine();

        sb.Append("Cauldron Recipes : ");
        sb.AppendLine();
        sb.Append(this.RecipeCreator.MyToString(this.RecipeCreator.CauldronRecipes));
        sb.AppendLine();
        Debug.Log(sb.ToString());
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
