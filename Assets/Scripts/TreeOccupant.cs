using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class TreeOccupant : ICellOccupant
{
    private int stage = 1;
    //Return the amount of polution caused by the occupying object.
    public int Pollution()
    {
        return -2*stage;
    }

    public void IncreaseStage()
    {
        stage++;
    }

    public int GetStage()
    {
        return stage;
    }
}