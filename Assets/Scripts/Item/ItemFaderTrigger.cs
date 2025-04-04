using UnityEngine;
using Utilities.Parms;

namespace Item
{
    public class ItemFaderTrigger : MonoBehaviour
    {
        private ItemFader[] _itemFaders;

        private void Awake()
        {
            _itemFaders = GetComponentsInChildren<ItemFader>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_itemFaders.Length > 0)
            {
                foreach (var item in _itemFaders)
                {
                    item.FadeOut(ItemDefine.FadeDuration);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_itemFaders.Length > 0)
            {
                foreach (var item in _itemFaders)
                {
                    item.FadeIn(ItemDefine.FadeDuration);
                }
            }
        }
    }
}