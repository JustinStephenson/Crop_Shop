using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    [SerializeField]
    private OrderManager orderManager;
    [SerializeField]
    private TruckType truckType;
    [SerializeField]
    private PhoneType phoneType;

    [SerializeField]
    private GameObject truck;
    private TruckSprite[] truckSprites = new TruckSprite[4];
    private bool clearTruckSprites = true;

    [SerializeField]
    private float timeTruckComes;

    //Lerp stuff
    private Vector3 startPos;
    private Vector3 endPos;
    private bool LerpTruck = false;
    private const float LERP_SPEED = 0.005f;
    private float LerpPercentage = 0;

    private void Start()
    {
        int i = 0;
        foreach(TruckSprite sp in truck.GetComponentsInChildren<TruckSprite>())
        {
            truckSprites[i] = sp;
            i++;
        }

        //tempTime = timeTruckComes;
        startPos = new Vector3(truck.transform.position.x, -5.0f, truck.transform.position.z);
        endPos = new Vector3(truck.transform.position.x, 1.0f, truck.transform.position.z);
    }

    private void Update()
    {
        if (LerpTruck && !PauseGame.gamePaused)
        {
            LerpPercentage += LERP_SPEED;
            truck.transform.position = Vector3.Lerp(startPos, endPos, LerpPercentage);
            if (LerpPercentage >= 1)
            {
                truck.transform.position = endPos;
                if (endPos.y >= 1.0f)
                {
                    TruckComes();
                }
                Vector3 temp = endPos;
                endPos = startPos;
                startPos = temp;
                LerpPercentage = 0;
                LerpTruck = false;
            }
        }
        if (truck.transform.position.y <= -4.0f && !clearTruckSprites)
        {
            foreach (TruckSprite sp in truckSprites)
            {
                sp.SetSpriteToNull();
            }
            clearTruckSprites = true;
            phoneType.SetCanUsePhone(true);
        }

        if (phoneType.MadeCall() && !truckType.GetTruckHere())
        {
            //Truck Comes
            LerpTruck = true;
        }
        if (!phoneType.MadeCall() && truckType.GetTruckHere())
        {
            //Truck Leaves
            TruckLeaves();
            UnloadTruck();
        }
    }

    private void TruckComes()
    {
        truckType.SetTruckHere(true);
        phoneType.SetCanUsePhone(true);
        Debug.Log("Truck is here");
    }

    private void TruckLeaves()
    {
        LerpTruck = true;
        truckType.SetTruckHere(false);
        clearTruckSprites = false;
        Debug.Log("Truck left");
    }

    private void UnloadTruck()
    {
        List<Crop> crops = truckType.GetCropsFromTruck();
        if (crops.Count > 0)
        {
            foreach (Crop c in crops)
            {
                orderManager.OrderUpdate(c);
            }
            crops.Clear();
        }
    }
}
