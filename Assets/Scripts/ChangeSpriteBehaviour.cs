using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteBehaviour : MonoBehaviour
{
    public SpriteRenderer render;

    public Sprite[] treeTiers;
    public Sprite[] factoryTiers;

    private int tierNr;

    //private bool isCoroutineExecuting = false;

    public float startTime;
    public float repeatTime;

    public bool isTree;

    private void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();

        //sets the stage to 0, the first
        tierNr = 0;

        //sets the first stages
        if (isTree) render.sprite = treeTiers[tierNr];
        //more functions?

        if(!isTree) render.sprite = factoryTiers[tierNr];
        //more functions?

        //start tiers after x seconds
        InvokeRepeating("NextTier", startTime, repeatTime);
    }

    public void NextTier()
    {
        //sets the stage to the next one
        tierNr++;

        //sets to the next tier
        if (isTree) render.sprite = treeTiers[tierNr];
        //more functions?

        if (!isTree) render.sprite = factoryTiers[tierNr];

        //cancels the next stages when it's on the last stage
        if (tierNr == 2)
        {
            CancelInvoke();
        }
    }

    /*
    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
            yield break;

        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        NextStage();

        isCoroutineExecuting = false;
    }
    */
}
