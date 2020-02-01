using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalClickBehaviourPlant : MonoBehaviour
{
    public bool canIPlant;

    public float waitTimePlant;

    private Color startcolor;

    // Start is called before the first frame update
    void Start()
    {
        //set so you can do an action at the start of the game
        //kinda does this double
        canIPlant = true;
    }

    public void CounterToDoActionAgainPlant()
    {
        startcolor = GetComponent<Image>().color;

        //set so you can't do action again
        canIPlant = false;
        //sets the image red so you know you cant do an action
        GetComponent<Image>().color = Color.red;

        StartCoroutine(TheCounterPlant(waitTimePlant));
    }

    private IEnumerator TheCounterPlant(float waitTime)
    {
        //
        yield return new WaitForSeconds(waitTime);
        //after x seconds you can do action again
        canIPlant = true;
        GetComponent<Image>().color = Color.green;
    }
}
