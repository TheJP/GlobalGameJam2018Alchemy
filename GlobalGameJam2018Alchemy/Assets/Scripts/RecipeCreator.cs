using Assets.Scripts.ItemSignatures;
using GlobalGameJam2018Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RecipeCreator
{
    /// <summary>
    /// ALL THE RECIPES
    /// </summary>
    private List<Recipe> MyRecipes;

    /// <summary>
    /// Recipes for Baking
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
    /// Recipes for Cauldron
    /// </summary>
    public List<Recipe> CauldronRecipes
    {
        get;
    } = new List<Recipe>();

    /// <summary>
    /// Recipes for Mortar
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


    //zuerst loggen
    public String MyToString(List<Recipe> myList)
    {
        StringBuilder sb = new StringBuilder();

        foreach (Recipe recipe in myList)
        {
            bool addPlus = false;
            foreach (ItemSignature itemSignature in recipe.InItems)
            {
                if (addPlus) {
                    sb.Append("+ ");
                }

                if (itemSignature is ProcessedItemSignature)
                {
                    ProcessedItemSignature pIS = (ProcessedItemSignature)itemSignature;
                    sb.Append(pIS.ProcessedType + " " + pIS.Colour + " ");
                    //Debug.Log(pIS.ProcessedType);
                    //Debug.Log(pIS.Colour);
                }
                else if (itemSignature is IngredientSignature)
                {
                    IngredientSignature iS = (IngredientSignature)itemSignature;
                    sb.Append(iS.Type + " " + iS.Colour + " ");
                    //Debug.Log(iS.Type);
                    //Debug.Log(iS.Colour);
                }
                else if (itemSignature is MoneyMakerSignature)
                {
                    MoneyMakerSignature mMS = (MoneyMakerSignature)itemSignature;
                    sb.Append(mMS.Type + " no Color ");
                    //Debug.Log(mMS.Type);
                }
                addPlus = true;
            }


            if (recipe.CreateItem() is ProcessedItem)
            {
                ProcessedItem item = (ProcessedItem)recipe.CreateItem();
                sb.Append("= " + item.ProcessedType + " " + item.ProcessedColor);
                //Debug.Log(item.ProcessedType);
                //Debug.Log(item.ProcessedColor);
            }
            else if (recipe.CreateItem() is MoneyMaker)
            {
                MoneyMaker item = (MoneyMaker)recipe.CreateItem();
                sb.Append("= " + "Tea");
            }

            //sb.Append(" " + recipe.Complexity);
            //Debug.Log(recipe.Complexity);
            sb.AppendLine();
        }

        return sb.ToString();
    }


    public RecipeCreator()
    {
        //myRecipes = createRandomRecipes();
        CreateStartRecipes();
    }

    //Creating RandomRecipes, is calling createRecipe
    private Recipe[] CreateRandomRecipes()
    {
        return null;
    }

    private void CreateStartRecipes()
    {
        List<IngredientColour> myLstOne = Enum.GetValues(typeof(IngredientColour)).OfType<IngredientColour>().ToList();

        foreach(IngredientColour colour in myLstOne) {


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

        //2nd Stage:

        Array myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        List<ProcessedItem.ProcessedItemColor> lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        List<ProcessedItem.ProcessedItemColor> lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        List<ProcessedItem.ProcessedItemColor> lstThree = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();

        System.Random rnd = new System.Random();

        for (int i = 0; i < myColorArrayOne.Length; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            BakeRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Powderessence, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Slimeessence, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Fairiedust, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);
        }

        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstThree = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();

        for (int i = 0; i < myColorArrayOne.Length; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            MortarRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Powderessence, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Lightifier, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);
        }


        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();

        for (int i = 0; i < myColorArrayOne.Length; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);


            TeaRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Herbessence, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Steamessence, lstTwo[intTwo])
                                          }, () => new MoneyMaker("Teapot", 418, ItemType.Processed), 15));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
        }

        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstThree = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();

        for (int i = 0; i < myColorArrayOne.Length; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            CauldronRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Slimeessence, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Steamessence, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Glowstone, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);
        }

        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstThree = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();

        for (int i = 0; i < myColorArrayOne.Length; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            CauldronRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Slimeessence, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Steamessence, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Glowstone, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);
        }



        //3rd Stage: Heres the Explanation: 
        // We need 7 different energy amunition
        // For this there are 4 different cooking variants, 2 of which are using baking! As we need 7 different energy amunition (colors), we distribute 1 color
        // to each of the 4 cooking variants, so there are 3 colors left, which are distributed randomly, towards Baking, Mortar and Cauldron.

        int baking = 0;
        int bakingOne = 1;
        int bakingTwo = 1;
        int mortar = 1;
        int cauldron = 1;

        for (int i = 0; i < 3; i++)
        {
            switch (rnd.Next(0, 2))
            {
                case 0:
                    baking++;
                    break;
                case 1:
                    mortar++;
                    break;
                case 2:
                    cauldron++;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        for (int i = 0; i < baking; i++)
        {
            if (rnd.Next(0, 1) == 0)
            {
                bakingOne++;
            }
            else
            {
                bakingTwo++;
            }
        }


        //FAIRIE DUST + FIREFLIES

        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();

        //as we need all the seven colors for the energy we won't initialize lstThree again.
        lstThree = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();


        for (int i = 0; i < bakingOne; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            BakeRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Fairiedust, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Fireflies, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);
        }

        // LIGHTIFIER + GLOWSTone

        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();


        for (int i = 0; i < bakingTwo; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            BakeRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Lightifier, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Glowstone, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);
        }

        // LIGHTIFIER + FIREFLIES

        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();

        for (int i = 0; i < mortar; i++)
        {

            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            MortarRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Lightifier, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Fireflies, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);
        }


        // FAIRIEDUST + GLOWSTone

        myColorArrayOne = Enum.GetValues(typeof(ProcessedItem.ProcessedItemColor));
        lstOne = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();
        lstTwo = myColorArrayOne.OfType<ProcessedItem.ProcessedItemColor>().ToList();


        for (int i = 0; i < cauldron; i++)
        {
            int intOne = rnd.Next(0, lstOne.Count);
            int intTwo = rnd.Next(0, lstTwo.Count);
            int intThree = rnd.Next(0, lstThree.Count);

            var capture = lstThree[intThree];
            CauldronRecipes.Add(new Recipe(new List<ItemSignature> {
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Fairiedust, lstOne[intOne]),
                                                new ProcessedItemSignature(ProcessedItem.ProcessedItemType.Glowstone, lstTwo[intTwo])
                                          }, () => new ProcessedItem(ProcessedItem.ProcessedItemType.Energy, capture), 10));

            lstOne.RemoveAt(intOne);
            lstTwo.RemoveAt(intTwo);
            lstThree.RemoveAt(intThree);

        }


    }


}


