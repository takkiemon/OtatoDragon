using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlinkRed()
    {
        StopAllCoroutines();
        StartCoroutine("BlinkCoroutine");
    }

    IEnumerator BlinkCoroutine()
    {
        GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        yield return new WaitForSecondsRealtime(1);
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }
}
