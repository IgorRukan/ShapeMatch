using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FigureType : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected GameManager gameManager;
    protected Figure figure;
    public Sprite TypeIconSprite;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        figure = GetComponent<Figure>();
    }

    protected virtual void OnEnable()
    {
        TypeFeatures();
    }

    protected virtual void OnDisable()
    {
    }

    public abstract FigureFactory.FigureTypeEnum FeatureType { get; }

    protected virtual void TypeFeatures()
    {
        SetCurrentSprite();

        rb.gravityScale = 1f;
    }

    private void SetCurrentSprite()
    {
        if (TypeIconSprite == null)
        {
            figure.typeIcon.sprite = null;
        }
        else
        {
            figure.typeIcon.sprite = TypeIconSprite;
        }
    }
}