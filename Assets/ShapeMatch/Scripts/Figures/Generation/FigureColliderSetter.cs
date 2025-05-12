using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureColliderSetter : MonoBehaviour
{
    public void SetCollider(string shapeName)
    {
        var polygonColliders = GetComponents<PolygonCollider2D>();
        var circleCollider = GetComponent<CircleCollider2D>();
        var boxColliders = GetComponents<BoxCollider2D>();

        HideAllColliders();
        
        boxColliders[0].enabled = true;
        boxColliders[0].isTrigger = true; // 1 — для обработки тачей

        switch (shapeName.ToLower())
        {
            case "triangle":
                polygonColliders[0].enabled = true; // Первый — под треугольник
                break;

            case "pentagon":
                polygonColliders[1].enabled = true; // Второй — под пятиугольник
                break;

            case "circle":
                circleCollider.enabled = true;
                break;

            case "square":
                boxColliders[1].enabled = true;
                break;

            default:
                Debug.LogWarning($"Неизвестная форма для активации коллайдера: {shapeName}");
                break;
        }
    }

    public void HideAllColliders()
    {
        foreach (var col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
    }
}