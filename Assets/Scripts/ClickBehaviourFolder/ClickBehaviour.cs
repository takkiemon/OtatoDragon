using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickBehaviour : MonoBehaviour
{
    public Texture2D cursorNormalTexture;
    public Texture2D cursorHoverTexture;
    public Texture2D cursorClickTexture;

    private Color startcolor;
    public GameObject acornPlane;

    GlobalActionBehaviour actionController;

    private void Start()
    {
        actionController = GetComponentInParent<GlobalActionBehaviour>();
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

        //changes colour depending if you can do an action
        if (actionController.canDoAction)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if(!actionController.canDoAction)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
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
