using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBehaviour : MonoBehaviour
{
    public Texture2D cursorNormalTexture;
    public Texture2D cursorHoverTexture;
    public Texture2D cursorClickTexture;

    private Color startcolor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Cursor.SetCursor(cursorClickTexture, Vector2.zero, CursorMode.Auto);

        GetComponent<GridCell>().Interact();
    }

    //highlights the object
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);

        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.yellow;
    }
    
    //undoes the highlight to the original
    void OnMouseExit()
    {
        Cursor.SetCursor(cursorNormalTexture, Vector2.zero, CursorMode.Auto);

        GetComponent<Renderer>().material.color = startcolor;
    }

    void OnMouseUp()
    {
        Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    }
}
