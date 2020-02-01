using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalClickBehaviourDestroy : MonoBehaviour
{
    public bool canIDestroy;

    public float waitTimeDestroy;

    private Color startcolor;

    // Start is called before the first frame update
    void Start()
    {
        //set so you can do an action at the start of the game
        //kinda does this double
        canIDestroy = true;
    }

    public void CounterToDoActionAgainDestroy()
    {
        startcolor = GetComponent<Image>().color;

        //set so you can't do action again
        canIDestroy = false;
        //sets the image red so you know you cant do an action
        GetComponent<Image>().color = Color.red;

        StartCoroutine(TheCounterDestroy(waitTimeDestroy));
    }

    private IEnumerator TheCounterDestroy(float waitTime)
    {
        //
        yield return new WaitForSeconds(waitTime);
        //after x seconds you can do action again
        canIDestroy = true;
        GetComponent<Image>().color = Color.green;
    }
}
