using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    //gotta reference to the globalclickcontroller
    GlobalClickBehaviourDestroy actionControllerDestroy;
    GlobalClickBehaviourPlant actionControllerPlant;

    ICellOccupant cellOccupant; // the type of thing that is occupying the gridcell (tree or factory)
    public GameObject acornPlane;
    public GameObject acornUIElement;
    public Vector2Int pos;

    private Vector3 ResourcePosition = new Vector3(173, 90, -2);

    private GameObject pollutionIndicator;
    private GameObject gameOccupantObject;

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
        //das referencesssss
        actionControllerDestroy = GameObject.FindGameObjectWithTag("ActionTimerDestroy").GetComponent<GlobalClickBehaviourDestroy>();
        actionControllerPlant = GameObject.FindGameObjectWithTag("ActionTimerPlant").GetComponent<GlobalClickBehaviourPlant>();

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
            switch (type)
            {
                case Occupant.factory:
                    Destroy(pollutionIndicator);
                    pollutionIndicator = Instantiate((GameObject)Resources.Load("Prefabs/CO2", typeof(GameObject)), transform);
                    Invoke("DestroyIndicator", 1);

                    cellOccupant = new FactoryOccupant();
                    Destroy(gameOccupantObject);
                    gameOccupantObject = Instantiate((GameObject)Resources.Load("Prefabs/Factory", typeof(GameObject)),transform);

                break;
                case Occupant.tree:
                    Destroy(pollutionIndicator);
                    pollutionIndicator = Instantiate((GameObject)Resources.Load("Prefabs/O2", typeof(GameObject)), transform);
                    Invoke("DestroyIndicator", 1);

                    cellOccupant = new TreeOccupant();
                    Destroy(gameOccupantObject);
                    Instantiate((GameObject)Resources.Load("Prefabs/Tree Variant", typeof(GameObject)), transform);
                break;
                case Occupant.empty:
                    Destroy(gameOccupantObject);
                    cellOccupant = null;
                    break;
                default:
                    break;
            }
        //}
        GetComponentInParent<GameManagerBehaviour>().PollutionChanged.Invoke();

    }

    public void DestroyIndicator()
    {
        Destroy(pollutionIndicator);
    }

    public void Interact()
    {
        if (cellOccupant == null && actionControllerPlant.canIPlant)
        {
            if (!GetComponentInParent<GameManagerBehaviour>().NeighbourPolluted(pos))
            {
                if (GetComponentInParent<GameManagerBehaviour>().SpendSeed())
                {
                    SetOccupant(Occupant.tree);
                    //so you cannot do this all again right away
                    actionControllerPlant.CounterToDoActionAgainPlant();
                    var acornObject = Instantiate(acornPlane);
                    acornObject.GetComponent<acornFeedbackBehavior>().SetValues(ResourcePosition, this.transform.position, 60.0f);
                }
            }
        }
        else if (cellOccupant is FactoryOccupant && actionControllerDestroy.canIDestroy)
        {
            //so you cannot do this all again right away
            actionControllerDestroy.CounterToDoActionAgainDestroy();

            SetOccupant(Occupant.empty);
            GetComponentInParent<GameManagerBehaviour>().GainSeed();
            var acornObject = Instantiate(acornPlane);
            acornObject.GetComponent<acornFeedbackBehavior>().SetValues(this.transform.position, ResourcePosition, 60.0f);
        }
    }

    public void UpgradeOccupant()
    {
        if (cellOccupant == null)
        {
            SetOccupant(Occupant.factory);
        } else
        {
            cellOccupant.IncreaseStage();
        }
        GetComponentInParent<GameManagerBehaviour>().PollutionChanged.Invoke();

        Invoke("UpgradeOccupant", 20);
    }
}