using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayAgainButtonBehaviour : MonoBehaviour {
    public Texture2D cursorNormalTexture;
    public Texture2D cursorHoverTexture;
    public Texture2D cursorClickTexture;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorNormalTexture, Vector2.zero, CursorMode.Auto);
        gameObject.GetComponent<Button>()
            .onClick
            .AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked()
    {
        SceneManager.LoadScene("ClickManager");   
    }

    public void OnPointerDown()
    {
        Cursor.SetCursor(cursorClickTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter()
    {
        Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit()
    {
        Cursor.SetCursor(cursorNormalTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerUp()
    {
        Cursor.SetCursor(cursorNormalTexture, Vector2.zero, CursorMode.Auto);
    }
}
