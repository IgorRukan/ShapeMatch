using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RowBar : MonoBehaviour
{
    public int emptySpaces;
    public BarSlot[] barSlots;
    private int currentId = 0;

    public void Place(Figure figure)
    {
        //узнаем текущую свободную позицию сначала
        for (int i = 0; i < barSlots.Length; i++)
        {
            if (barSlots[i].currentFigure == null)
            {
                currentId = i;
                break;
            }
        }
        
        SetBarSlot(figure);

        currentId++;
    }

    private void SetBarSlot(Figure figure)
    {
        barSlots[currentId].index = currentId;
        barSlots[currentId].currentFigure = figure;
        barSlots[currentId].image.enabled = false;
        figure.transform.position = barSlots[currentId].transform.position;
    }

    public bool CheckIsFull()
    {
        if (currentId >= emptySpaces)
        {
            return true;
        }
        return false;
    }

    public void ClearAllBoard()
    {
        for (int i = 0; i < barSlots.Length; i++)
        {
            ClearBarSlot(i);
        }
    }

    public void ClearBarSlot(int index)
    {
        barSlots[index].currentFigure = null;
        barSlots[index].index = 0;
        barSlots[index].image.enabled = true;
    }

    public void ResetId()
    {
        currentId = 0;
    }
}