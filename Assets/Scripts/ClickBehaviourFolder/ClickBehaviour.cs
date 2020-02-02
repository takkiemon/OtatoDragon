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
    public GameObject dropletPlane;

    GlobalActionBehaviour actionController;

    GridCell gridCell;

    SpriteRenderer spriteRenderer;
    Color onEnterColour;
    Color normalColour;

    private void Start()
    {
        actionController = GetComponentInParent<GlobalActionBehaviour>();
        gridCell = GetComponent<GridCell>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        onEnterColour = new Color(255f, 255f, 0f, 1f); //is about 50 % transparent and vurry much yellow
        normalColour = Color.black;

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

        //sets the colour temp to something flashy
        //startcolor = GetComponent<Renderer>().material.color;
        spriteRenderer.color = onEnterColour;

        //changes colour depending if you can do an action
        if (actionController.canDoAction)
        {
            //GetComponent<Renderer>().material.color = Color.green; use spriteRenderer for this and not material color
        }
        else if(!actionController.canDoAction)
        {
            //GetComponent<Renderer>().material.color = Color.red;
        }

        if (gridCell.GetOccupantType() == GridCell.Occupant.empty)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    
    //undoes the highlight to the original
    void OnMouseExit()
    {
        Cursor.SetCursor(cursorNormalTexture, Vector2.zero, CursorMode.Auto);

        //sets the flashy colour off so you know you are not targeting that grid cell
        //GetComponent<Renderer>().material.color = startcolor;
        spriteRenderer.color = normalColour;

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnMouseUp()
    {
        Cursor.SetCursor(cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    }
}
