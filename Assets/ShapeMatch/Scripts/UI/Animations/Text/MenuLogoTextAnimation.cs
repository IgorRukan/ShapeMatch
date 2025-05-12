using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MenuLogoTextAnimation : TextAnimation
{
    protected override void MainAnimation()
    {
        base.MainAnimation();
        
        text.transform.localScale = Vector3.one;

        // Анимация, где текст увеличивается и уменьшается
        currentTween = text.transform
            .DOScale(Vector3.one * 1.2f, duration) //0.5f
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
