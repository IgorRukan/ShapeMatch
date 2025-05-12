using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BarSlot : MonoBehaviour
{
    public Figure currentFigure;
    public int index;
    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }
}
