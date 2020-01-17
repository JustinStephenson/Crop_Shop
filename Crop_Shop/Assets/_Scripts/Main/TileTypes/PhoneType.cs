using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneType : MonoBehaviour
{
    private static bool madeCall = false;
    private static bool canUsePhone = true;

    private SpriteRenderer mySR;
    [SerializeField]
    private Sprite greenSprite;
    [SerializeField]
    private Sprite redSprite;

    private void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canUsePhone && mySR.sprite != greenSprite)
        {
            mySR.sprite = greenSprite;
        }
    }

    public void Interact(Tool tool, Crop crop, PlayerInteractions player)
    {
        if (madeCall && canUsePhone)
        {
            madeCall = false;
            canUsePhone = false;
            mySR.sprite = redSprite;
        }
        else if (canUsePhone)
        {
            madeCall = true;
            canUsePhone = false;
            mySR.sprite = redSprite;
        }
    }

    public bool MadeCall()
    {
        return madeCall;
    }

    public void SetCanUsePhone(bool state)
    {
        canUsePhone = state;
    }
}
