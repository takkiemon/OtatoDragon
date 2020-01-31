using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    //ICellOccupant cellOccupant; // the type of thing that is occupying the gridcell (tree or factory)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPollutionCount()
    {
        if (true/*cellOccupant*/)
        {
            return 0;// cellOccupant.Pollution();
        }
        else
        {
            return 0;
        }
    }

    public void SetOccupant(/*ICellOccupant type*/)
    {
        /*if (cellOccupant != null)
        {
        cellOccupant = type;
        }
        else
        {
        Destroy the current occupant? deny the request to set occupant?
        }*/
    }
}
