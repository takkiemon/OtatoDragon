using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiverFlow : MonoBehaviour
{
    public Image flowImage;

    public Image coverImage;

    public Image PollutionImage;

    public GameManagerBehaviour gameManager;
    // Update is called once per frame
    void Update()
    {
        coverImage.fillAmount+=0.01f;
        if (coverImage.fillAmount >= 1f)
            coverImage.fillAmount = 0;
        flowImage.fillAmount+=0.01f;
        if (flowImage.fillAmount >= 1f)
            flowImage.fillAmount = 0;
        PollutionImage.fillAmount = gameManager.GetRiverPolution();
    }
}
