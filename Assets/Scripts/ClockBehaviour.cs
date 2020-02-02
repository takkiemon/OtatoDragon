using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockBehaviour : MonoBehaviour
{
    private float startTime;
    public float totalTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = 1f - (Time.time - startTime) / totalTime;
    }
}
