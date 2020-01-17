using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSprite : MonoBehaviour
{
    private GameObject truckTypeObject;
    private TruckType truckType;
    private ClickTile clickTile;
    private SpriteRenderer mySpriteRen;
    private Color32 orginalColor;
    public int truckNum;
    private bool hasCrop = false;

    public void Start()
    {
        truckTypeObject = GameObject.FindGameObjectsWithTag("truck")[truckNum];
        truckType = truckTypeObject.GetComponent<TruckType>();
        clickTile = truckTypeObject.GetComponent<ClickTile>();
        mySpriteRen = GetComponent<SpriteRenderer>();
        orginalColor = mySpriteRen.color;
    }

    void Update()
    {
        if (truckType.GetTruckHere() || mySpriteRen.color != orginalColor)
        {
            mySpriteRen.color = clickTile.GetColor();
        }
        if (truckType.GetHaveCrop() && !hasCrop)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].sprite = truckType.GetSprite();
            hasCrop = true;
        }
    }

    public void SetSpriteToNull()
    {
        GetComponentsInChildren<SpriteRenderer>()[1].sprite = null;
        hasCrop = false;
    }

    public Sprite GetSprite()
    {
        return GetComponentsInChildren<SpriteRenderer>()[1].sprite;
    }
}
