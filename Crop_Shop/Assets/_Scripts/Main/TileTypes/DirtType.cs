using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtType : MonoBehaviour {

    private SpriteRenderer mySprite;
    private SpriteRenderer cropSprite;
    private SpriteRenderer waterSprite;
    private SpriteRenderer weedSprite;

    public Sprite dirtExtra;
    public Sprite dirtRegular;

    public Sprite[] weeds;

    private bool needsPlowing = true;
    private bool cropPlanted = false;

    public Crop crop;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        cropSprite = GetComponentsInChildren<SpriteRenderer>()[1];  //get child 1 Sprite Rend
        waterSprite = GetComponentsInChildren<SpriteRenderer>()[2]; //get child 2 Sprite Rend
        weedSprite = GetComponentsInChildren<SpriteRenderer>()[3]; //get child 3 Sprite Rend
        AddDirt();
    }

    void Update()
    {
        if (cropPlanted)
        {
            if (crop.deadCrop || crop.fullyGrown)
            {
                waterSprite.enabled = false;
            }
            else if (crop.NeedsWater())
            {
                waterSprite.enabled = true;
            }
            else
            {
                waterSprite.enabled = false;
            }
       }
    }

    public void Interact(Tool tool, Crop crop, PlayerInteractions player)
    {
        if (needsPlowing)
        {
            if (tool != null)
            {
                if (tool.toolset == Tool.ToolSet.plow)
                {
                    needsPlowing = false;
                    mySprite.sprite = dirtRegular;
                }
            }
        }
        else
        {
            if (crop != null && !cropPlanted)
            {
                //player has planted the crop on plowed dirt
                this.crop = new Crop(crop);
                CropPlanted();
            }
            else if (cropPlanted)
            {
                Debug.Log("pick");
                if (this.crop.fullyGrown || this.crop.deadCrop)
                {
                    //player picks the crop
                    player.SetCrop(this.crop);
                    player.SetGrownOrDead(true);
                    CropPicked();
                }

                else if (tool != null)
                {
                    if (tool.toolset == Tool.ToolSet.watercan)
                    {
                        this.crop.SetWaterLevel(tool.GetWaterAmount());
                    }
                    else if (tool.toolset == Tool.ToolSet.weedcutter)
                    {
                        weedSprite.sprite = weeds[this.crop.CutWeeds(tool.GetCutAmount())];
                    }
                }

            }
        }
    }

    public bool IsCropPlanted()
    {
        return cropPlanted;
    }

    public Crop GetCropPlanted()
    {
        return crop;
    }

    private void AddDirt()
    {
        mySprite.sprite = dirtExtra;
    }

    private void CropPlanted()
    {
        cropPlanted = true;
        crop.cropState = Crop.CropState.Planted;
        cropSprite.sprite = crop.GetCropSprite();
        InvokeRepeating("GrowPlant", 1.0f, 1.0f);
        InvokeRepeating("DehydratePlant", 1.0f, 1.0f);
    }

    public void CropPicked()
    {
        cropSprite.sprite = null;
        crop = null;
        cropPlanted = false;
        needsPlowing = true;
        waterSprite.enabled = false;
        CancelInvoke();
        AddDirt();
    }

    private void GrowPlant()
    {
        crop.ChangeGrowthLevel();
        cropSprite.sprite = crop.GetCropSprite();
        weedSprite.sprite = weeds[crop.GainWeeds()];
        if (crop.fullyGrown)
        {
            CancelInvoke();
        }
    }

    private void DehydratePlant()
    {
        crop.Dehydrate();
        cropSprite.sprite = crop.GetCropSprite();
        if (crop.deadCrop)
        {
            CancelInvoke();
        }
    }
}
