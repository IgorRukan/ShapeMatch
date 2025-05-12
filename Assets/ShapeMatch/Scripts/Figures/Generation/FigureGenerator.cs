using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FiguresGenerator : MonoBehaviour
{
    [SerializeField]
    private List<Figure> figures;

    private FigureFactory figureFactory;
    private static FigureFactory.FigureTypeEnum[] cachedTypes;

    private void Start()
    {
        figureFactory = FigureFactory.Instance;
        figures = new List<Figure>();
        cachedTypes = (FigureFactory.FigureTypeEnum[])Enum.GetValues(typeof(FigureFactory.FigureTypeEnum));
    }

    public void GenerateRandomFigures(int numberOfUniqFigures, int requiredPairsToMatch, int numOfFigures)
    {
        var unpaired = numberOfUniqFigures * requiredPairsToMatch - numOfFigures;

        if (unpaired > 0)
        {
            numberOfUniqFigures--;
            var randomFigure = figureFactory.CreateRandomFigure();
            
            AddFigure(randomFigure, unpaired);
        }
        for (int i = 0; i < numberOfUniqFigures; i++)
        {
            var randomFigure = figureFactory.CreateRandomFigure();
            AddFigure(randomFigure, requiredPairsToMatch - 1);
        }

        Shuffle(figures);
    }

    private void AddFigure(Figure figure, int requiredPairsToMatch)
    {
        figures.Add(figure);
        for (int i = 0; i < requiredPairsToMatch; i++)
        {
            var figureClone = figureFactory.CreateFigure(figure);
            
            var type = GetRandomFigureType();
            AssignFeatureToFigure(figureClone.gameObject,type);
            figureClone.figureType = type;
            
            figures.Add(figureClone);
        }
    }
    
    private FigureFactory.FigureTypeEnum GetRandomFigureType()
    {
        return cachedTypes[Random.Range(0, cachedTypes.Length)];
    }
    
    private void AssignFeatureToFigure(GameObject figureGO, FigureFactory.FigureTypeEnum assignedType)
    {
        var allFeatures = figureGO.GetComponents<FigureType>();

        foreach (var feature in allFeatures)
        {
            feature.enabled = feature.FeatureType == assignedType;
        }
    }

    public void ClearFigures()
    {
        if (figures == null) return;

        foreach (var figure in figures)
        {
            figure.ReturnToPool();
        }

        figures.Clear();
        figureFactory.ClearUniqFigures();
    }

    public List<Figure> GetFigures()
    {
        return figures;
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}