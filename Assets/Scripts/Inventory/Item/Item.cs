using UnityEngine;
using Utilities.Parms;

namespace MyFarm.Inventory
{
    public class Item : MonoBehaviour
    {
        public int itemID;

        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider2D;
        private ItemDetails _itemDetails;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (itemID != 0)
                Init(itemID);
        }

        public void Init(int id)
        {
            itemID = id;
            // 获取物品详情
            _itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
            if (_itemDetails != null)
            {
                _spriteRenderer.sprite = _itemDetails.onWorldIcon;

                // 设置碰撞体大小
                Vector2 newSize = new Vector2(_spriteRenderer.sprite.bounds.size.x, _spriteRenderer.sprite.bounds.size.y);
                _boxCollider2D.size = newSize;
                _boxCollider2D.offset = new Vector2(0, _spriteRenderer.sprite.bounds.center.y);
            }
            
            // if (itemDetails.itemType == ItemType.ReapableScenery)
            // {
            //     gameObject.AddComponent<ReapItem>();
            //     gameObject.GetComponent<ReapItem>().InitCropData(itemDetails.itemID);
            //     gameObject.AddComponent<ItemInteractive>();
            // }
            
        }
        
    }
}