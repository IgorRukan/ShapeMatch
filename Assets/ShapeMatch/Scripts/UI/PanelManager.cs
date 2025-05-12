using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PanelManager : Singleton<PanelManager>
{
    public GameObject menuPanel;
    public GameObject inGamePanel;
    public GameObject finishPanel;

    public Button startButton;
    public Button restartButton;
    public Button refillButton;
    
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    
    private GameManager gameManager;
    private FiguresFall figuresFall;

    private void Start()
    {
        gameManager = GameManager.Instance;
        figuresFall = FiguresFall.Instance;
        gameManager.FinishLvl += ShowFinishPanel;
        figuresFall.endFall += EnableRefillButton;
        
        HideAllPanels();
        menuPanel.SetActive(true);
        
        ButtonsEvents();
    }

    private void ButtonsEvents()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        refillButton.onClick.AddListener(OnResetButtonClick);
    }

    private void OnStartButtonClick()
    {
        HideAllPanels();
        HideRefillButton();
        
        inGamePanel.SetActive(true);
        
        gameManager.StartGame();
    }
    
    private void EnableRefillButton()
    {
        refillButton.gameObject.SetActive(true);
    }

    public void HideRefillButton()
    {
        refillButton.gameObject.SetActive(false);
    }

    private void OnResetButtonClick()
    {
        gameManager.RefillField();
    }

    private void OnRestartButtonClick()
    {
        HideAllPanels();
        menuPanel.SetActive(true);
    }

    private void ShowFinishPanel(bool isWin)
    {
        HideAllPanels();
        finishPanel.SetActive(true);

        winText.gameObject.SetActive(isWin);
        loseText.gameObject.SetActive(!isWin);
    }

    private void HideAllPanels()
    {
        menuPanel.SetActive(false);
        finishPanel.SetActive(false);
        inGamePanel.SetActive(false);
        
    }
}
