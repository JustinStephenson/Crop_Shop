using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Crop {

    public CropAsset cropAsset;

    //Growing Stuff
    public enum CropState
    {
        Seed,
        Planted,
        Growing1,
        Growing2,
        Growing3,
        Grown,
        Dead
    }
    public CropState cropState = CropState.Seed;
    private float growthLevel = 0f;
    public bool fullyGrown = false;
    public bool deadCrop = false;

    //Watering Stuff
    public enum WaterState
    {
        Dry,
        Wet
    }
    public WaterState waterState = WaterState.Wet;
    private float waterLevel = 50f;
    private readonly float dehydrateThreshold = 30f;
    private readonly float waterLevelMax = 50f;

    //Weed Stuff
    public enum WeedState
    {
        Weed0,
        Weed1,
        Weed2
    }
    public WeedState weedState = WeedState.Weed0;
    private float weedLevel = 0f;
    private readonly float growWeedThreshold = 2f;

    public Crop(Crop crop)
    {
        cropAsset = crop.cropAsset;
    }

    //Growth Methods
    public Sprite GetCropSprite()
    {
        if (cropAsset == null)
        {
            return null;
        }

        switch(cropState)
        {
            case CropState.Seed:
                return cropAsset.seedSprite;
            case CropState.Planted: 
                return cropAsset.seedSprite;
            case CropState.Growing1:
                return cropAsset.growing1Sprite;
            case CropState.Growing2:
                return cropAsset.growing2Sprite;
            case CropState.Growing3:
                return cropAsset.growing3Sprite;
            case CropState.Grown:
                return cropAsset.grownSprite;
            case CropState.Dead:
                return cropAsset.deadSprite;
        }

        Debug.Log("Error");
        return cropAsset.seedSprite;
    }

    public Sprite GetCropSpriteFullyGrown()
    {
        return cropAsset.grownSprite;
    }

    public float GetGrowthLevel()
    {
        return growthLevel;
    }

    public void ChangeGrowthLevel()
    {
        growthLevel += cropAsset.growthSpeed;
        if (growthLevel >= 100)
        {
            cropState = CropState.Grown;
            fullyGrown = true;
        }
        else if (growthLevel >= 75)
        {
            cropState = CropState.Growing3;
        }
        else if (growthLevel >= 50)
        {
            cropState = CropState.Growing2;
        }
        else if (growthLevel >= 25)
        {
            cropState = CropState.Growing1;
        }
        //Debug.Log(this.cropState);
    }

    //Water Methods
    public bool NeedsWater()
    {
        if (waterState == WaterState.Dry)
        {
            return true;
        }
        return false;
    }

    public void SetWaterLevel(float waterAmount)
    {
        waterLevel += waterAmount;
        if (waterLevel > dehydrateThreshold)
        {
            waterState = WaterState.Wet;
        }
        if (waterLevel > waterLevelMax)
        {
            waterLevel = waterLevelMax;
        }
        Debug.Log(waterLevel);
    }

    public void Dehydrate()
    {
        waterLevel -= cropAsset.dehydrateSpeed;
        if (waterLevel <= dehydrateThreshold)
        {
            if (waterLevel <= 0)
            {
                cropState = CropState.Dead;
                deadCrop = true;
            }
            waterState = WaterState.Dry;
        }
    }

    //Weed Methods
    public int GainWeeds()
    {
        weedLevel += cropAsset.weedGrowthSpeed;
        if (weedLevel >= growWeedThreshold)
        {
            if (weedState != WeedState.Weed2)
            {
                weedState++;
            }
        }
        return (int)weedState;
    }

    public int CutWeeds(int cutAmount)
    {
        weedLevel = 0;
        weedState -= cutAmount;
        if (weedState < 0)
        {
            weedState = 0;
        }
        return (int)weedState;
    }
    
    //Other Methods
    public override bool Equals(System.Object obj)
    {
        if (this == obj)
        {
            return true;
        }
        if (obj == null)
        {
            return false;
        }
        Crop other = (Crop)obj;
        if (other.cropAsset == null)
        {
            return false;
        }
        if (cropAsset.ID == other.cropAsset.ID)
        {
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}


