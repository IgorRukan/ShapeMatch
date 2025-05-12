using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WinTextAnimation : TextAnimation
{
    public float jumpPower;
    protected override void MainAnimation()
    {
        base.MainAnimation();
        
        text.transform.localScale = Vector3.one;

        // Анимация, где текст увеличивается и уменьшается
        currentTween = text.transform
            .DOJump(text.transform.position, jumpPower, 1, duration) //1f
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.OutBounce);
    }
}
