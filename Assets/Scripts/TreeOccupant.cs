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
        return -4*stage;
    }

    public void IncreaseStage()
    {
        stage++;
        //sound that a tree upgrades
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().PlaySoundEffects("treeTierUp");
    }

    public int GetStage()
    {
        return stage;
    }
}