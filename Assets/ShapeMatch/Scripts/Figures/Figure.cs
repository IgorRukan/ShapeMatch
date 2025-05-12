using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class Figure : MonoBehaviour
{
    public Sprite shape;
    public Color color;
    public Sprite icon;
    public SpriteRenderer shapeRend;
    public SpriteRenderer iconRend;
    public SpriteMask maskRend;
    public SpriteRenderer backgroundRend;
    private FigureColliderSetter setter;
    private SortingGroup sortingGroup;

    public bool isLocked = false;
    private Rigidbody2D rb;
    
    public SpriteRenderer typeIcon;
    public FigureFactory.FigureTypeEnum figureType;
    public bool isFrozen = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sortingGroup = GetComponentInChildren<SortingGroup>();
        setter = GetComponent<FigureColliderSetter>();
    }

    public void SetFigureFields(Sprite newShape, Sprite mask, Color newColor, Sprite newIcon)
    {
        shape = newShape;
        color = newColor;
        icon = newIcon;

        shapeRend.sprite = shape;
        maskRend.sprite = mask;
        backgroundRend.color = color;
        iconRend.sprite = icon;
    }

    public void StateOnAwake(Transform figuresParent)
    {
        gameObject.SetActive(true);
        transform.localPosition = figuresParent.position;

        isLocked = false;
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = false;
        transform.eulerAngles = Vector3.zero;
        shapeRend.sortingOrder = 1;
        iconRend.sortingOrder = 2;
        sortingGroup.sortingOrder = 1;
    }

    public void StateOnField()
    {
        rb.isKinematic = false;
        setter.SetCollider(shape.name);
    }
    
    public void StatePlaceInBar()
    {
        isLocked = true;
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        rb.freezeRotation = true;
        rb.velocity = Vector2.zero;
        transform.eulerAngles = Vector3.zero;
        shapeRend.sortingOrder = 6;
        iconRend.sortingOrder = 7;
        sortingGroup.sortingOrder = 6;

        setter.HideAllColliders();
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
        setter.HideAllColliders();
    }

    public bool IsEqual(Figure figure)
    {
        return shape.name == figure.shape.name && color.Equals(figure.color) && icon.name == figure.icon.name;
    }
}