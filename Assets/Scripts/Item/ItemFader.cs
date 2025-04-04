using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utilities.Parms;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// 逐渐恢复颜色
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        Color targetColor = new Color(1, 1, 1, 1);
        spriteRenderer.DOColor(targetColor, duration);
    }
    /// <summary>
    /// 逐渐变淡颜色
    /// </summary>
    /// <param name="duration"></param>
    public void FadeOut(float duration)
    {
        Color targetColor = new Color(1, 1, 1, 0.5f);
        spriteRenderer.DOColor(targetColor, duration);
    }
    
}
