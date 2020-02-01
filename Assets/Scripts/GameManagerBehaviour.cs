using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerBehaviour : MonoBehaviour
{
    public Text pollutionText;
    public UnityEvent PollutionChanged;
    private static int seeds;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentsInChildren<GridCell>()[2].SetOccupant(GridCell.Occupant.factory);
        GetComponentsInChildren<GridCell>()[5].SetOccupant(GridCell.Occupant.factory);
        GetComponentsInChildren<GridCell>()[8].SetOccupant(GridCell.Occupant.factory);
        GetComponentsInChildren<GridCell>()[0].SetOccupant(GridCell.Occupant.factory);
        GetComponentsInChildren<GridCell>()[1].SetOccupant(GridCell.Occupant.factory);
        GetComponentsInChildren<GridCell>()[20].SetOccupant(GridCell.Occupant.factory);


        if (PollutionChanged == null)
            PollutionChanged = new UnityEvent();
        pollutionText.text = "pollution: " + GetPollution();
        PollutionChanged.AddListener(PollutionChangedAction);
    }

    // Update is called once per frame
    void PollutionChangedAction()
    {
        pollutionText.text = "pollution: " + GetPollution();
        if (GetPollution() <= 0) {
            SceneManager.LoadScene("WinScene");
        } else if (GetPollution() >= 100) {
            SceneManager.LoadScene("LoseScene");
        }
    }

    int GetPollution()
	{
        int totalPolution = 0;
        GridCell[] cellArray = this.gameObject.GetComponentsInChildren<GridCell>();
        foreach (GridCell cell in cellArray)
        {
            totalPolution += cell.GetPollutionCount();
        };

        return totalPolution;
    }

    bool IsWinConditionMet(int pollution)
    {
        return pollution == 0;
    }

    public void GainSeed()
    {
        seeds++;
    }

    public bool SpendSeed()
    {
        if (seeds > 0)
        {
            seeds--;
            return true;
        }

        return false;
    }
}
