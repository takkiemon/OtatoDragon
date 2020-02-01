using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerBehaviour : MonoBehaviour
{
    public TextMeshProUGUI seedText;
    public Image pollutionBar;
    public UnityEvent PollutionChanged;
    System.Random random = new System.Random();

    public float minimumTimeToSpawnFactory; // in seconds
    public float maximumTimeToSpawnFactory; // in seconds
    public int inverseSpawnChance; // value of 20 means 1 in 20;
    public float factorySpawnTimer; // it's public so we can check this in the editor
    int chance;

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

        System.Random random = new System.Random();
        if (minimumTimeToSpawnFactory > maximumTimeToSpawnFactory)
        {
            maximumTimeToSpawnFactory = minimumTimeToSpawnFactory;
        }
    }

    private void FixedUpdate() // the default time between calls is 0.02 seconds (50 calls per second) 
    {
        factorySpawnTimer++;
        if (factorySpawnTimer >= minimumTimeToSpawnFactory * 50)
        {
            RollDieForFactorySpawn();
        }
    }

    // Update is called once per frame
    void PollutionChangedAction()
    {
        pollutionBar.fillAmount = GetPollution() / 100f;
        if (GetPollution() <= 0) { 
            SceneManager.LoadScene("WinScene");
        } else if (GetPollution() >= 100) {
            SceneManager.LoadScene("LoseScene");
        }
    }

    int GetPollution()
	{
        int totalPollution = 0;
        GridCell[] cellArray = this.gameObject.GetComponentsInChildren<GridCell>();
        foreach (GridCell cell in cellArray)
        {
            totalPollution += cell.GetPollutionCount();
        };

        return totalPollution;
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

    public void RollDieForFactorySpawn() // this function checks first if it spawns or not, so I want to rename it, but I couldn't think of a better name. // CheckToSpawnFactory() // SpawnFactoryOrNot()
    {
        chance = random.Next(0, inverseSpawnChance);
        if (chance == 0 || factorySpawnTimer >= maximumTimeToSpawnFactory * 50)
        {
            SpawnFactoryOnRandomTile();
            factorySpawnTimer = 0;
        }
    }

    public void SpawnFactoryOnRandomTile()
    {
        chance = random.Next(GetComponentsInChildren<GridCell>().Length);
        if (GetComponentsInChildren<GridCell>()[chance].GetOccupantType() == GridCell.Occupant.empty)
        {
            GetComponentsInChildren<GridCell>()[chance].SetOccupant(GridCell.Occupant.factory);
        }
    }
}
