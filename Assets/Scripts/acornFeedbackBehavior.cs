using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornFeedbackBehavior : MonoBehaviour
{
    public Vector3 lerpFrom, lerpTo;
    public float lerpSpeed;

    private float lerpFloat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lerpFloat += lerpSpeed / 10000f; // speed is so high that we'll divide by this magic number, to compensate
        transform.position = Vector3.Lerp(lerpFrom, lerpTo, lerpFloat);
        if (lerpFloat >= 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetValues(Vector3 lerpFrom, Vector3 lerpTo, float lerpSpeed)
    {
        this.lerpFrom = new Vector3(lerpFrom.x, lerpFrom.y, -2);
        this.lerpTo = new Vector3(lerpTo.x, lerpTo.y, -2); ;
        this.lerpSpeed = lerpSpeed;
        Debug.Log("acorn behavior: lerp from (" + this.lerpFrom + ") to (" + this.lerpTo + ")");
    }
}
