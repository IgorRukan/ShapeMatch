using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    protected TextMeshProUGUI text;
    protected Tween currentTween; 
    public float duration;

    protected void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    protected void OnEnable()
    {
        MainAnimation();
    }

    protected void OnDisable()
    {
        StopAnimation();
    }

    protected virtual void MainAnimation()
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
    }
    
    protected void StopAnimation()
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
    }
}
