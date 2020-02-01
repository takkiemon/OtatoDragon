using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellOccupant
{
    //Return the amount of polution caused by the occupying object.
    int Pollution();
    void IncreaseStage();
    int GetStage();
}
