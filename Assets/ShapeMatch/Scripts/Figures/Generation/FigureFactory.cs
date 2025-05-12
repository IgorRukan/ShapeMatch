using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureFactory : Singleton<FigureFactory>
{
    public FigureData figureData;
    public Pool figurePool;
    private List<Figure> createdFigures;
    private Transform figuresParent;

    private int numberOfAllUniqueFigures;

    public enum FigureTypeEnum
    {
        Default,
        Heavy,
        Explosive,
        Frozen,
        Sticky
    }

    private void Start()
    {
        figuresParent = transform;
        createdFigures = new List<Figure>();

        ValidateUniqueFiguresLimit();
    }

    private void ValidateUniqueFiguresLimit()
    {
        numberOfAllUniqueFigures = figureData.shape.Count * figureData.color.Count * figureData.icon.Count;

        if (GameManager.Instance.numberOfUniqFigures >= numberOfAllUniqueFigures)
        {
            throw new Exception("Количество уникальных фигур превышено");
        }
    }

    public Figure CreateRandomFigure()
    {
        var obj = figurePool.GetPooledObjects();
        obj.GetComponent<Figure>().StateOnAwake(figuresParent);

        var figure = obj.GetComponent<Figure>();

        while (true)
        {
            var shapeId = GetRandomFrom(figureData.shape);
            var shape = figureData.shape[shapeId];
            var mask = figureData.shapeMask[shapeId];
            var color = figureData.color[GetRandomFrom(figureData.color)];
            var icon = figureData.icon[GetRandomFrom(figureData.icon)];

            figure.SetFigureFields(shape, mask, color, icon);

            if (IsUniqFigure(figure))
            {
                AddUniqueFigure(figure);
                break;
            }
        }
        
        return figure;
    }

    public Figure CreateFigure(Figure figure)
    {
        var obj = figurePool.GetPooledObjects();
        obj.GetComponent<Figure>().StateOnAwake(figuresParent);

        var newFigure = obj.GetComponent<Figure>();

        newFigure.SetFigureFields(figure.shape, figure.maskRend.sprite, figure.color, figure.icon);
        return newFigure;
    }


    private void AddUniqueFigure(Figure figure)
    {
        createdFigures.Add(figure);
    }

    public void ClearUniqFigures()
    {
        createdFigures.Clear();
    }

    int GetRandomFrom<T>(List<T> list) => Random.Range(0, list.Count);

    private bool IsUniqFigure(Figure createdFigure) =>
        !createdFigures.Any(f => f.IsEqual(createdFigure));
}