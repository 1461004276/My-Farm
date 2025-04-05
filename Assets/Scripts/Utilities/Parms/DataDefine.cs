using UnityEngine;

namespace Utilities.Parms
{
    [System.Serializable] //加了序列化之后，实例化该类后里面的公有变量可以直接在UnityInspector面板显示出来，可以十分方便的可视化设置变量
    public class ItemDetails //物品详细信息类
    {
        public int itemID;//ID
        public string itemName;//名字
        public ItemType itemType;//类型
        public Sprite icon;//图片
        public Sprite onWorldIcon;//在世界中的图片
        public string description;//详情信息
        public int useRadius;//使用范围
        public bool canPickedUp;//能否被拾取
        public bool canDropped;//能否被丢掉
        public bool canCarried;//能否举起
        public int price;//价格
        [Range(0, 1)]
        public float sellPercentage;//被销售时的出售比例
    }
}