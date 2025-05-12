using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public int numberOfUniqFigures;
    private int currentUniqFiguresNum;
    public int requiredPairsToMatch = 3;
    private List<int> tempFiguresId;

    public Action<bool> FinishLvl;
    public Action<int> Match;

    public int numberOfFigures = 0;
    private int maxNumberOfFigures = 0;

    public GameObject figureSpawn;
    private FiguresGenerator figuresGenerator;
    private FiguresFall figuresFall;

    public RowBar bar;

    private void Start()
    {
        figuresGenerator = figureSpawn.GetComponent<FiguresGenerator>();
        figuresFall = figureSpawn.GetComponent<FiguresFall>();
        tempFiguresId = new List<int>();

        currentUniqFiguresNum = numberOfUniqFigures;
        maxNumberOfFigures = numberOfUniqFigures * requiredPairsToMatch;

        SetCurrentNumber();
    }

    public void StartGame()
    {
        //генерируем рандомные фигуры
        figuresGenerator.GenerateRandomFigures(currentUniqFiguresNum,requiredPairsToMatch,numberOfFigures);

        //запускаем анимацию падения
        figuresFall.FigureFallAnimation();
    }

    //получаем текущее количество фигур на поле
    private void SetCurrentNumber()
    {
        numberOfFigures = maxNumberOfFigures;
        currentUniqFiguresNum = numberOfUniqFigures;
    }

    public void MoveFigureToBar(Figure figure)
    {
        if (!(figure.figureType == FigureFactory.FigureTypeEnum.Frozen && figure.isFrozen))
        {
            figure.StatePlaceInBar();
            bar.Place(figure);

            MatchCheck(figure);
        }
    }

    private void MatchCheck(Figure newFigure)
    {
        tempFiguresId.Clear();

        int matchCount = CountMatches(newFigure);

        if (matchCount == requiredPairsToMatch)
        {
            RemoveMatchedFigures();
            bar.ResetId();
            PairMatch();
        }

        if (bar.CheckIsFull())
        {
            MatchBarIsFull();
        }
    }

    private int CountMatches(Figure newFigure)
    {
        int matchCount = 0;
        var barSlots = bar.barSlots;

        for (int i = 0; i < barSlots.Length; i++)
        {
            if (barSlots[i]?.currentFigure != null && newFigure.IsEqual(barSlots[i].currentFigure))
            {
                tempFiguresId.Add(i);
                matchCount++;
            }
        }

        return matchCount;
    }

    private void RemoveMatchedFigures()
    {
        List<int> extraMatches = new List<int>();

        foreach (var index in tempFiguresId)
        {
            var slot = bar.barSlots[index];
            var figure = slot.currentFigure;

            if (figure.figureType == FigureFactory.FigureTypeEnum.Explosive)
            {
                AddIfValid(index - 1, extraMatches);
                AddIfValid(index + 1, extraMatches);
            }

            RemoveMatchedFigure(slot, index);
        }

        // Обрабатываем дополнительно найденные фигуры
        foreach (int index in extraMatches)
        {
            var slot = bar.barSlots[index];
            RemoveMatchedFigure(slot, index);
        }

        numberOfFigures -= extraMatches.Count;
        tempFiguresId.AddRange(extraMatches);
    }

    private void AddIfValid(int index, List<int> list)
    {
        if (index >= 0 && index < bar.barSlots.Length && !tempFiguresId.Contains(index) && !list.Contains(index))
        {
            list.Add(index);
        }
    }

    private void RemoveMatchedFigure(BarSlot slot, int index)
    {
        if (slot?.currentFigure != null)
        {
            slot.currentFigure.ReturnToPool();
            bar.ClearBarSlot(index);
        }
    }

    private void PairMatch()
    {
        numberOfFigures -= requiredPairsToMatch;
        currentUniqFiguresNum--;
        if (numberOfFigures <= 0)
        {
            Finished(true);
        }

        if (numberOfFigures > 0 && numberOfFigures < requiredPairsToMatch)
        {
            Finished(false);
        }

        var matchCount = maxNumberOfFigures - numberOfUniqFigures;
        Match?.Invoke(matchCount);
    }

    private void MatchBarIsFull()
    {
        Finished(false);
    }

    private void ResetField()
    {
        //очищаем поле
        figuresGenerator.ClearFigures();
        //очищаем бар
        bar.ClearAllBoard();
    }

    public void RefillField()
    {
        ResetField();
        PanelManager.Instance.HideRefillButton();
        StartGame();
    }

    private void Finished(bool isWin)
    {
        FinishLvl?.Invoke(isWin);
        ResetField();

        SetCurrentNumber();
    }
}