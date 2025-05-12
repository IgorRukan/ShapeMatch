using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureTouch : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Figure>() && !hit.collider.GetComponent<Figure>().isLocked)
                {
                    gameManager.MoveFigureToBar(hit.collider.GetComponent<Figure>());
                }
            }
        }
    }
}
