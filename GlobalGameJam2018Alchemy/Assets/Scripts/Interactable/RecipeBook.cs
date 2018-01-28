using System.Collections;
using System.Collections.Generic;
using System.Text;
using GlobalGameJam2018Networking;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour, IInteractable
{

    public Text TextLeft;
    public Text TextMid;
    public Text TextRight;
    public Canvas MyCanvas;

    public RecipeCreator RecipeCreator;


    bool ManualDisable;

    private bool workaround;

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
        /*
        sbDestill.Append(this.RecipeCreator.MyToString(this.RecipeCreator.DestillRecipes));
        */
        sbDestill.Append("Destill Recipes : ");
        sbDestill.AppendLine();
        sbDestill.Append("Liquid COLORX = Slimeessence COLORX\nPowder COLORX = Powderessence COLORX \nSteam COLORX = Steamessence COLORX \nHerbPowder COLORX = Herbessence COLORX");
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

        this.TextLeft.text = sbDestill.ToString() + "\n" + sbMortar.ToString();

        this.TextMid.text = sbTea.ToString() + ""+ sbCauldron.ToString();

        this.TextRight.text = sbBaking.ToString();
    }

    /// <summary>
    /// Called by Interaction with RecipeBook, user can escape with space or it will go away after a time.
    /// </summary>
    public void DisplayRecipes()
    {
        workaround = !MyCanvas.enabled;
        MyCanvas.enabled = true;
        ManualDisable = false;
        CancelInvoke();
        Invoke("DisableDisplay", 10.0f);
        
    }

    public void DisableDisplay() {
        if (!ManualDisable) {
        MyCanvas.enabled= false;
        }
    }

    void Awake() {
        createRecipes();
        MyCanvas.enabled = false;
    }

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (MyCanvas.isActiveAndEnabled && !workaround) {
            if (Input.GetButtonUp("Jump") || Input.GetKeyDown(KeyCode.Escape)){ 
                ManualDisable = true;
                MyCanvas.enabled = false;
            }
        }

        workaround = false;
    }
}
