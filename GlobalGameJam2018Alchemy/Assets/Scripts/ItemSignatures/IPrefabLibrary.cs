using GlobalGameJam2018Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ItemSignatures
{
    public abstract class PrefabLibraryBase : MonoBehaviour
    {
        public abstract GameObject GetPrefab(IItem itemType);
    }
}
