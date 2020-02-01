using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameManagerBehaviour : MonoBehaviour
{
    public TextMeshProUGUI seedText;
    public Image pollutionBar;
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
        pollutionBar.fillAmount = GetPollution() /100f;
        seedText.text = seeds.ToString();
        PollutionChanged.AddListener(PollutionChangedAction);
    }

    // Update is called once per frame
    void PollutionChangedAction()
    {

        pollutionBar.fillAmount = GetPollution() / 100f;
        if (GetPollution() <= 0)
            Debug.Log("Win");
        if (GetPollution() >= 100)
            Debug.Log("Lost");
        
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
        seedText.text = seeds.ToString();
    }

    public bool SpendSeed()
    {
        if (seeds > 0)
        {
            seeds--;
            seedText.text = seeds.ToString();
            return true;
        }

        return false;
    }
}
