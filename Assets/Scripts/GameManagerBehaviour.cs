using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehaviour : MonoBehaviour
{
    public Text pollutionText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pollutionText.text = "pollution: " + GetPollution();
    }

    int GetPollution()
	{
        int totalPolution = 0;
        CellBehaviour[] cellArray = this.gameObject.GetComponentsInChildren<CellBehaviour>();
        foreach (CellBehaviour cell in cellArray)
        {
            totalPolution += cell.GetPollution();
        };

        return totalPolution;
    }

    bool IsWinConditionMet(int pollution)
    {
        return pollution == 0;
    }
}
