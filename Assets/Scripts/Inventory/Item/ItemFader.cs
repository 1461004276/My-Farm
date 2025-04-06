using DG.Tweening;
using UnityEngine;

namespace MyFarm.Inventory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemFader : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        /// <summary>
        /// 逐渐恢复颜色
        /// </summary>
        /// <param name="duration"></param>
        public void FadeIn(float duration)
        {
            Color targetColor = new Color(1, 1, 1, 1);
            _spriteRenderer.DOColor(targetColor, duration);
        }
        /// <summary>
        /// 逐渐变淡颜色
        /// </summary>
        /// <param name="duration"></param>
        public void FadeOut(float duration)
        {
            Color targetColor = new Color(1, 1, 1, 0.5f);
            _spriteRenderer.DOColor(targetColor, duration);
        }
    
    }
}
