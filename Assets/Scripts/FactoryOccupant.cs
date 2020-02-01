using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryOccupant : ICellOccupant
{
    private int stage = 1;
    //Return the amount of polution caused by the occupying object.
    public int Pollution()
    {
        return 5*stage;
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
