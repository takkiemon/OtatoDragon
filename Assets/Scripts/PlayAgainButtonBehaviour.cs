using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainButtonBehaviour : MonoBehaviour
{
    public Texture2D cursorNormalTexture;
    public Texture2D cursorHoverTexture;
    public Texture2D cursorClickTexture;

    // Start is called before the first frame update
    void Start()
    {
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

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorNormalTexture, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseUp()
    {
        Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    }
}
