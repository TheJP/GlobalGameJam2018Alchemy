using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalGameJam2018Networking;

namespace Assets.Scripts.Recipe
{
    class ProcessedItemSignature : ItemSignature
    {
        ProcessedItem.ProcessedItemType ProcessedType { get; }
        ProcessedItem.ProcessedItemColor Colour { get; }

        public ProcessedItemSignature(ProcessedItem.ProcessedItemType processedType, ProcessedItem.ProcessedItemColor colour) : base(ItemType.Processed)
        {
            ProcessedType = processedType;
            Colour = colour;
        }

        protected override bool CheckItemAcceptance(IItem other)
        {
            ProcessedItem processedItem = other as ProcessedItem;

            return processedItem?.ProcessedColor == Colour
                && processedItem?.ProcessedType == ProcessedType;
        }
    }
}
