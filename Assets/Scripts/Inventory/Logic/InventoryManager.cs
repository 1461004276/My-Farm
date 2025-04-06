using Inventory.DataSO;
using UnityEngine;
using Utilities.Parms;

namespace MyFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("Item")]
        public ItemDataList_SO _itemDataList_SO;
        
        /// <summary>
        /// 通过ID返回物品信息
        /// </summary>
        /// <param name="ID">Item ID</param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return _itemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
        }

        
    }
}