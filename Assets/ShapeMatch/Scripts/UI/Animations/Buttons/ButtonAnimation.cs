using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    public Button button;
    public float duration = 1f;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.transform.localScale = Vector3.zero;
        button.interactable = false;
        
        MainAnimation();
    }

    private void MainAnimation()
    {
        button.interactable = false;
        
        button.transform.DOScale(Vector3.one, duration)
            .SetEase(Ease.OutBack)
            .OnStart(() =>
            {
                button.interactable = true;
            });
    }
}
