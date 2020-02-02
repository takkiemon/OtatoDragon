using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalActionBehaviour : MonoBehaviour
{
    public bool canDoAction;

    // Start is called before the first frame update
    void Start()
    {
        //set so you can do an action at the start of the game
        canDoAction = true;
    }

    public void CounterToDoActionAgain(int waitTime)
    {
        //set so you can't do action again
        canDoAction = false;

        StartCoroutine(TheCounterDestroy(waitTime));
    }

    private IEnumerator TheCounterDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //after x seconds you can do action again
        canDoAction = true;
    }
}
