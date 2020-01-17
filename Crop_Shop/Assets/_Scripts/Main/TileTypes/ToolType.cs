using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolType : MonoBehaviour {

    public static Tool tool;

    private static PlayerInteractions player;

    public Sprite toolboxSouth;
    public Sprite toolboxNorth;

    void Start()
    {
        if (transform.position.y > 5)
        {
            GetComponent<SpriteRenderer>().sprite = toolboxNorth;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = toolboxSouth;
        }
    }

    public void Interact(Tool tool, Crop crop, PlayerInteractions player)
    {
        //PlayerIneraction Script opens the tool panel
        if (ToolType.player == null)
        {
            ToolType.player = player;
        }
    }

    public static void SetPlayerTool()
    {
        player.SetTool(tool);
    }
}
