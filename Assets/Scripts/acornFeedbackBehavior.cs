using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornFeedbackBehavior : MonoBehaviour
{
    public Vector2 lerpFrom, lerpTo;
    public float lerpSpeed;

    private float lerpFloat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lerpFloat += lerpSpeed;
        transform.position = Vector2.Lerp(lerpFrom, lerpTo, lerpFloat);
        if (lerpFloat >= 1)
        {
            Destroy(gameObject);
        }
    }

    public void SetValues(Vector2 lerpFrom, Vector2 lerpTo, float lerpSpeed)
    {
        this.lerpFrom = lerpFrom;
        this.lerpTo = lerpTo;
        this.lerpSpeed = lerpSpeed;
    }
}
