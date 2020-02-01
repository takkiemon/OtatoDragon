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

    GlobalClickBehaviourDestroy actionControllerDestroy;
    GlobalClickBehaviourPlant actionControllerPlant;

    private void Start()
    {
        actionControllerDestroy = GameObject.FindGameObjectWithTag("ActionTimerDestroy").GetComponent<GlobalClickBehaviourDestroy>();
        actionControllerPlant = GameObject.FindGameObjectWithTag("ActionTimerPlant").GetComponent<GlobalClickBehaviourPlant>();
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
        if (actionControllerDestroy.canIDestroy || actionControllerPlant.canIPlant)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if(!actionControllerDestroy.canIDestroy || !actionControllerPlant.canIPlant)
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
