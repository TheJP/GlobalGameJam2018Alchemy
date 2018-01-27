
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalGameJam2018Networking;

namespace Assets.Scripts.ItemSignatures
{
    class MoneyMakerSignature : ItemSignature
    {
        public string Name { get; }
        public MoneyMakerSignature(string Name) : base(ItemType.Processed)
        {
        }

        protected override bool CheckItemAcceptance(IItem other)
        {
            MoneyMaker moneyMaker = other as MoneyMaker;
            return string.Equals(moneyMaker?.Name, Name);
        }
    }
}
