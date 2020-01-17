using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckType : MonoBehaviour
{
    public SpriteRenderer cropSprite;

    private static List<Crop> crops;

    private bool haveCrop = false;
    private static bool truckHere = false;

    //sprites
    public Sprite topLeft;
    public Sprite bottomLeft;
    public Sprite topRight;
    public Sprite bottomRight;

    private void Start()
    {
        crops = new List<Crop>();
        cropSprite.sprite = null;

        //put sprites on the right tile
        if (transform.position.x == 2 && transform.position.y == 1)
        {
            GetComponent<SpriteRenderer>().sprite = topLeft;
        }
        else if (transform.position.x == 2 && transform.position.y == 0)
        {
            GetComponent<SpriteRenderer>().sprite = bottomLeft;
        }
        else if (transform.position.x == 3 && transform.position.y == 1)
        {
            GetComponent<SpriteRenderer>().sprite = topRight;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = bottomRight;
        }
    }

    private void Update()
    {
        if (!truckHere && cropSprite.sprite != null)
        {
            cropSprite.sprite = null;
            haveCrop = false;
        }
    }

    public void Interact(Tool tool, Crop crop, PlayerInteractions player)
    {
        if (truckHere)
        {
            if (crop != null && crop.fullyGrown && !haveCrop)
            {
                haveCrop = true;
                cropSprite.sprite = crop.GetCropSprite();
                crops.Add(crop);
                player.SetCrop(null);
                player.SetGrownOrDead(false);
            }
            else
            {
                return;
            }
        }
        return;
    }

    public List<Crop> GetCropsFromTruck()
    {
        return crops;
    }

    public void SetTruckHere(bool state)
    {
        truckHere = state;
    }

    public bool GetTruckHere()
    {
        return truckHere;
    }

    public bool GetHaveCrop()
    {
        return haveCrop;
    }

    public Sprite GetSprite()
    {
        return cropSprite.sprite;
    }
}
