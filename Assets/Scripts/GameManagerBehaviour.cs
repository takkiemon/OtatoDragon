using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerBehaviour : MonoBehaviour
{
    public GameObject seedObject;
    public TextMeshProUGUI seedText;
    public Image pollutionBar;
    public UnityEvent PollutionChanged;
    System.Random random = new System.Random();

    public float minimumTimeToSpawnFactory; // in seconds
    public float maximumTimeToSpawnFactory; // in seconds
    public int inverseSpawnChance; // value of 20 means 1 in 20;
    public float factorySpawnTimer; // it's public so we can check this in the editor
    public int startingPollution;
    int chance;

    public GameObject dropletPlane;

    private int seeds;

    private GridCell[,] grid;
    public int gridSize = 5;
    public GameObject gridCell;
    private List<KeyValuePair<GridCell,float>> river;

    // Start is called before the first frame update
    void Start()
    {
        river = new List<KeyValuePair<GridCell, float>>();
        grid = new GridCell[gridSize,gridSize];

        for (int y= 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                grid[x,y] = Instantiate(gridCell, new Vector3(50 * x - 25 * (gridSize-1), -25 * y + 12.5f * (gridSize - 1) - 10
                        , 0), new Quaternion(), transform)
                    .GetComponent<GridCell>();
                grid[x,y].pos = new Vector2Int(x,y);
            }
        }
       river.Add(new KeyValuePair<GridCell,float>(grid[5, 5], 0.88f));
        river.Add(new KeyValuePair<GridCell, float>(grid[4, 5],0.85f));
        river.Add(new KeyValuePair<GridCell, float>(grid[4, 4], 0.75f));
        river.Add(new KeyValuePair<GridCell, float>(grid[4, 3],0.68f));
        river.Add(new KeyValuePair<GridCell, float>(grid[5, 3],0.58f));
        river.Add(new KeyValuePair<GridCell, float>(grid[5, 2],0.52f));
        river.Add(new KeyValuePair<GridCell, float>(grid[4, 2],0.45f));
        river.Add(new KeyValuePair<GridCell, float>(grid[3,1],0.4f));
        river.Add(new KeyValuePair<GridCell, float>(grid[2, 1],0.36f));
        river.Add(new KeyValuePair<GridCell, float>(grid[1, 1],0.32f));
        river.Add(new KeyValuePair<GridCell, float>(grid[1, 0],0.26f));
        river.Add(new KeyValuePair<GridCell, float>(grid[0, 0],0.22f));

        for (int i = 0; i < 3; i++)
            SpawnFactoryOnRandomTile();

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

        SetGridCellValues();
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


   public float GetPollution()



	{
        float totalPollution = startingPollution;
        GridCell[] cellArray = this.gameObject.GetComponentsInChildren<GridCell>();
        foreach (GridCell cell in cellArray)
        {
            totalPollution += cell.GetPollutionCount();
        };
	    int pollutedRiver = river.Count;

	    for (int i = 0; i < river.Count; i++)
	    {
	        if (river[i].Key.GetOccupantType() == GridCell.Occupant.factory)
	        {
	            pollutedRiver = i;
	            break;
	        }
	    }

	    totalPollution += 1.5f*(river.Count - pollutedRiver);
        return totalPollution;
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
        } else
        {
            seedObject.GetComponent<BlinkBehaviour>().BlinkRed();
            return false;
        }
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
            //sound that a factory spawns
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().PlaySoundEffects("factorySpawn");

        } else if (GetComponentsInChildren<GridCell>()[chance].GetOccupantType() == GridCell.Occupant.factory)
        {
            GetComponentsInChildren<GridCell>()[chance].UpgradeOccupant();
            //sound of upgrading factory
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().PlaySoundEffects("factoryTierUp");
        }
    }

    public void SetGridCellValues()
    {
        foreach (GridCell cell in GetComponentsInChildren<GridCell>())
        {
            cell.dropletPlane = this.dropletPlane;
        }
    }

    public bool NeighbourPolluted(Vector2Int pos)
    {
        if (pos.x > 0)
            if (grid[pos.x - 1, pos.y].GetOccupantType() == GridCell.Occupant.factory)
                return true;
        if (pos.x< gridSize-1)
            if (grid[pos.x + 1, pos.y].GetOccupantType() == GridCell.Occupant.factory)
                return true;
        if (pos.y > 0)
            if (grid[pos.x, pos.y - 1].GetOccupantType() == GridCell.Occupant.factory)
                return true;
        if (pos.y < gridSize -1)
            if (grid[pos.x, pos.y + 1].GetOccupantType() == GridCell.Occupant.factory)
                return true;
        return false;
    }

    public float GetRiverPolution()
    {
        float polStart = 0;
        
        foreach (KeyValuePair<GridCell,float> cell in river)
        {
            if (cell.Key.GetOccupantType() == GridCell.Occupant.factory && cell.Value > polStart)
                polStart = cell.Value;
        }

        return polStart;
    }
}
