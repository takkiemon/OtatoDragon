using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("pls work?");
        gameObject.GetComponent<Button>()
            .onClick
            .AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonClicked()
    {
        SceneManager.LoadScene("ClickManager");   
    }
}
