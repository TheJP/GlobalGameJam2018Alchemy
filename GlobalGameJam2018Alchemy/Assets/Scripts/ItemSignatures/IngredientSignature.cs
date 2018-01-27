using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalGameJam2018Networking;

namespace Assets.Scripts.ItemSignatures
{
    class IngredientSignature : ItemSignature
    {
        public IngredientColour Colour { get; }

        public IngredientSignature(ItemType type, IngredientColour colour) : base(type)
        {
        }

        protected override bool CheckItemAcceptance(IItem other)
        {
            Ingredient otherIngredient = other as Ingredient;
            return otherIngredient?.Colour == Colour;
        }
    }
}
