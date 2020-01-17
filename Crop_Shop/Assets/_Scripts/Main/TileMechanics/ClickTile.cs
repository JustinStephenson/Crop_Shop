using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    public static bool click = false;
    public static bool pause = false;

    SpriteRenderer mySpriteRen;
    private Color32 orginalColor;
    private Color32 clickColor = new Color32(0x96, 0x96, 0x96, 0xFF);
    private bool LerpColor = false;
    private const float LERP_SPEED = 0.01f;
    private float LerpPercentage = 0;

    void Start()
    {
        mySpriteRen = GetComponent<SpriteRenderer>();
        orginalColor = mySpriteRen.color;
    }

    void OnMouseUp()
    {
        if (!click && !pause)
        {
            click = true;
            PlayerAnimations.canMove = true;
            map.MovePlayer(tileX, tileY);
            HighlightTile();
        }
    }

    void Update()
    {
        if (LerpColor)
        {
            LerpPercentage += LERP_SPEED;
            mySpriteRen.color = Color.Lerp(clickColor, orginalColor, LerpPercentage);
            if (LerpPercentage >= 1)
            {
                mySpriteRen.color = orginalColor;
                LerpColor = false;
            }
        }
    }

    void HighlightTile()
    {
        mySpriteRen.color = clickColor;
        LerpPercentage = 0;
        LerpColor = true;
    }

    public Color32 GetColor()
    {
        return mySpriteRen.color;
    }
}
