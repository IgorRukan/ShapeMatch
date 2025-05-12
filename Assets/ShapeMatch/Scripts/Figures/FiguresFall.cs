using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresFall : Singleton<FiguresFall>
{
    public float spawnDelay;
    private FiguresGenerator figuresGenerator;
    public List<Figure> figures;

    private GameManager gameManager;
    private bool isFinished = false;

    public Action endFall;

    private void Start()
    {
        figuresGenerator = GetComponent<FiguresGenerator>();
        figures = new List<Figure>();
        gameManager = GameManager.Instance;
        gameManager.FinishLvl += LevelFinished;
    }

    private void SetFigures()
    {
        figures = figuresGenerator.GetFigures();
    }

    public void FigureFallAnimation()
    {
        SetFigures();
        StartCoroutine(Fall());
    }

    private void LevelFinished(bool obj)
    {
        isFinished = true;
    }

    private IEnumerator Fall()
    {
        isFinished = false;
        var i = 0;
        do
        {
            if (isFinished)
            {
                isFinished = false;
                yield break;
            }

            if (figures[i] != null)
            {
                figures[i].StateOnField();
            }

            i++;
            yield return new WaitForSeconds(spawnDelay);
        } while (i!=figures.Count);

        endFall?.Invoke();
    }
}