using GlobalGameJam2018Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ItemSignatures
{
    public abstract class ItemSignature
    {
        public ItemType Type { get; private set; }

        public ItemSignature(ItemType type)
        {
            Type = type;
        }

        public bool IsItemAcceptable(IItem other)
        {
            if(Type == other.Type)
            {
                return CheckItemAcceptance(other);
            }

            return false;
        }

        protected abstract bool CheckItemAcceptance(IItem other);
    }
}
