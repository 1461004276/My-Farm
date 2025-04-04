using UnityEngine;

namespace Item.Bushes
{
    public enum BushType
    {
        None = -1,
        Bushes1 = 0,
        Bushes2 = 1,
        Bushes3 = 2,
        Bushes4 = 3,
        Bushes5 = 4,
        Bushes6 = 5,
        Bushes7 = 6,
        Bushes8 = 7,
    }


    public class BushesSetting : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;
        public BushType bushType;

        private void Awake()
        {
            if (bushType == BushType.None) bushType = (BushType)Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[(int)bushType];
        }

        [ContextMenu("Set Bushes Sprite")]
        public void SetBushesSprite()
        {
            spriteRenderer.sprite = sprites[(int)bushType];
        }
    }
}