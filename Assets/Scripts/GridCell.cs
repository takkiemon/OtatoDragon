using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    ICellOccupant cellOccupant; // the type of thing that is occupying the gridcell (tree or factory)
    public enum Occupant
    {
        empty = 0,
        factory = 1,
        tree = 2
    }

    public Occupant GetOccupantType()
    {
        if (cellOccupant is TreeOccupant)
            return Occupant.tree;
        else if (cellOccupant is FactoryOccupant)
            return Occupant.factory;
        return Occupant.empty;
    }

    void Start()
    {
        //Invoke("UpgradeOccupant", Random.Range(0, 30f));
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
        //if (cellOccupant == null)
        //{
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
                case Occupant.empty:
                    if (transform.childCount > 0)
                    {
                        Destroy(transform.GetChild(0).gameObject);
                    }
                    cellOccupant = null;
                    break;
                default:
                    break;
            }
        //}
        GetComponentInParent<GameManagerBehaviour>().PollutionChanged.Invoke();

    }

    public void Interact()
    {
        if (cellOccupant == null)
        {
            if (GetComponentInParent<GameManagerBehaviour>().SpendSeed())
            {
                SetOccupant(Occupant.tree);
            }
        }
        else if (cellOccupant is FactoryOccupant)
        {
            SetOccupant(Occupant.empty);
            GetComponentInParent<GameManagerBehaviour>().GainSeed();
        }
    }

    void UpgradeOccupant()
    {
        if (cellOccupant == null)
            SetOccupant(Occupant.factory);
        else
            cellOccupant.IncreaseStage();
        GetComponentInParent<GameManagerBehaviour>().PollutionChanged.Invoke();

        Invoke("UpgradeOccupant", Random.Range(0,30f));
    }
}