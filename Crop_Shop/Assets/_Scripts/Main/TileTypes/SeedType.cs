using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedType : MonoBehaviour {

    public static Crop crop;

    private static PlayerInteractions player;

    //sprites
    public Sprite seedboxSouth;
    public Sprite seedboxNorth;

    void Awake()
    {
        crop = null;
    }

    void Start()
    {
        if (transform.position.y > 5)
        {
            GetComponent<SpriteRenderer>().sprite = seedboxNorth;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = seedboxSouth;
        }
    }

    public void Interact(Tool tool, Crop crop, PlayerInteractions player)
    {
        //PlayerIneraction Script opens the seed panel
        if (SeedType.player == null)
        {
            SeedType.player = player;
        }
    }

    public static void SetPlayerCrop()
    {
        player.SetCrop(crop);
    }
}
