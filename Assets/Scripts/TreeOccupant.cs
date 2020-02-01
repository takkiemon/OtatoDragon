using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOccupant : ICellOccupant
{
    //Return the amount of polution caused by the occupying object.
    public int Pollution()
    {
        return -2;
    }
}