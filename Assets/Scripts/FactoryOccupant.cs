using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryOccupant : ICellOccupant
{
    //Return the amount of polution caused by the occupying object.
    public int Polution()
    {
        return 10;
    }
}
