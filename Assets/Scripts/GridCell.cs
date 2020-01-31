using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    ICellOccupant cellOccupant; // the type of thing that is occupying the gridcell (tree or factory)
    public enum Occupant
    {
        factory = 0,
        tree = 1
    }

    public int GetPollutionCount()
    {
        if (cellOccupant != null)
        {
            return cellOccupant.Pollution();
        }
        else
        {
            return 0;
        }
    }

    public void SetOccupant(Occupant type)
    {
        if (cellOccupant == null)
        {
            switch (type)
            {
                case Occupant.factory:
                    cellOccupant = new FactoryOccupant();
                    Instantiate((GameObject)Resources.Load("Prefabs/Factory", typeof(GameObject)),transform);
                    break;
                case Occupant.tree:
                    cellOccupant = new TreeOccupant();
                    Instantiate((GameObject)Resources.Load("Prefabs/Tree Variant", typeof(GameObject)), transform);
                    break;
                default:
                    break;
            }
        }
    }

    public void Interact()
    {
        if (cellOccupant == null)
        {
            SetOccupant(Occupant.tree);
        }
        else if (cellOccupant is FactoryOccupant)
        {
            cellOccupant = null;
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}