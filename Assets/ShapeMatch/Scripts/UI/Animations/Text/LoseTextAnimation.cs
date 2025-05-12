using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LoseTextAnimation : TextAnimation
{
    public float pauseTime = 1f;
    protected override void MainAnimation()
    {
        base.MainAnimation();
        
        text.transform.localScale = Vector3.one;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(text.transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360));

        sequence.AppendInterval(pauseTime);

        sequence.SetLoops(-1, LoopType.Restart);

        sequence.SetEase(Ease.InOutSine);
    }
}
