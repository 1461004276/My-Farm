using System.Collections.Generic;
using UnityEngine;
using Utilities.Parms;

namespace Inventory.DataSO
{
    [CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "物品/ItemDataList_SO", order = 0)]
    public class ItemDataList_SO : ScriptableObject
    {
        public List<ItemDetails> itemDetailsList; 
    }
}