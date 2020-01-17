using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public OrderManager orderManager;
    public SpawnManager spawnManager;
    private int currentLevel;

    [SerializeField]
    private List<Crop> cropsList = new List<Crop>();
    //pointer to somewhere in cropsList that indcates up to what crops I can use.
    private int cropsAvaliablePointer;

    private class Difficulty
    {
        public int cropsListnumber;
        public int totalAmountOfCrops;
        public int crowSpawnTime;
        public int locustSpawnTime;
        public Difficulty (int cropsListnumber, int totalAmountOfCrops, int crowSpawnTime, int locustSpawnTime)
        {
            this.cropsListnumber = cropsListnumber;
            this.totalAmountOfCrops = totalAmountOfCrops;
            this.crowSpawnTime = crowSpawnTime;
            this.locustSpawnTime = locustSpawnTime;
        }
    }

    readonly Difficulty[] difficultyArray =
    {
        //lvl1
        new Difficulty(1, 3, 0, 0),
        //lvl2
        new Difficulty(1, 5, 0, 0),
        //lvl3
        new Difficulty(2, 6, 0, 0),
        //lvl4
        new Difficulty(3, 10, 0, 0),
        //lvl5
        new Difficulty(3, 12, 0, 0)
        //lvl6
        //lvl7
        //lvl8
        //lvl9
        //lvl10
        //lvl11
        //lvl12
        //lvl13
        //lvl14
        //lvl15
        //lvl16
        //lvl17
        //lvl18
        //lvl19
        //lvl20
        //lvl21
        //lvl22
        //lvl23
        //lvl24
        //lvl25
        //lvl26
        //lvl27
        //lvl28
        //lvl29
        //lvl30
        //lvl31
        //lvl32
    };

    private void Awake()
    {

    }

    private void Start()
    {
        currentLevel = GameControl.control.currentLevel;
        //array start at 0, but lvl starts at 1.
        LevelUpdate(difficultyArray[currentLevel - 1]);
    }

    private void LevelUpdate(Difficulty d)
    {
        cropsAvaliablePointer = d.cropsListnumber;
        UpdateOrderList(d.totalAmountOfCrops);
        UpdateEnemy(d.crowSpawnTime, d.locustSpawnTime);
    }

    //Note to self: Consider randomizing the crop placement
    //totalAmountOfCrops upper bound is 108, from 9 x 12 due to amount of space in order list.
    private void UpdateOrderList(int totalAmountOfCrops)
    {
        List<Crop> cropsToOrder = new List<Crop>();
        List<int> numberOfCrop = new List<int>();

        for (int i = 0; i < cropsAvaliablePointer; i++)
        {
            if (totalAmountOfCrops == 0)
            {
                break;
            }
            if (i == cropsAvaliablePointer - 1)
            {
                cropsToOrder.Add(cropsList[i]);
                numberOfCrop.Add(totalAmountOfCrops);
                break;
            }
            //get random int
            int randomInt;
            if (totalAmountOfCrops >= 10)
            {
                randomInt = Random.Range(1, 10);
            }
            else
            {
                randomInt = Random.Range(1, totalAmountOfCrops + 1);
            }
            totalAmountOfCrops -= randomInt;
            cropsToOrder.Add(cropsList[i]);
            numberOfCrop.Add(randomInt);
        }
        orderManager.SetCropsToOrder(cropsToOrder, numberOfCrop);
    }

    private void UpdateEnemy(int crowSpawnTime, int locustSpawnTime)
    {
        spawnManager.SetSpawnCrowTime(crowSpawnTime);
        spawnManager.SetSpawnLocustTime(locustSpawnTime);
    }
}

