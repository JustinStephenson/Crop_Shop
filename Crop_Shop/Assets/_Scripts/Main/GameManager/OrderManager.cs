using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    private List<Crop> cropsToOrder = new List<Crop>();
    private List<int> numberOfCrop = new List<int>();

    [SerializeField]
    private GameObject[] itemList;

    [SerializeField]
    private GameObject[] doneLine;

    IEnumerator Start()
    {
        //need to wait for LevelManager to set values
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < itemList.Length; i++)
        {
            doneLine[i].SetActive(false);
            itemList[i].SetActive(false);
        }

        for (int i = 0; i < cropsToOrder.Count; i++)
        {
            itemList[i].GetComponent<Image>().sprite = cropsToOrder[i].GetCropSpriteFullyGrown();
            itemList[i].GetComponentInChildren<Text>().text = numberOfCrop[i].ToString();
            itemList[i].SetActive(true);
        }
    }

    public void SetCropsToOrder(List<Crop> cropsToOrder, List<int> numberOfCrop) 
    {
        //Garbage collect old list
        this.cropsToOrder.Clear();
        this.numberOfCrop.Clear();
        //Set to new list
        this.cropsToOrder = cropsToOrder;
        this.numberOfCrop = numberOfCrop;
    }

    public void OrderUpdate(Crop crop)
    {
        for (int i = 0; i < cropsToOrder.Count; i++)
        {
            if (cropsToOrder[i].Equals(crop))
            {
                if (numberOfCrop[i] != 0)
                {
                    numberOfCrop[i]--;
                    itemList[i].GetComponentInChildren<Text>().text = numberOfCrop[i].ToString();
                }
                if (numberOfCrop[i] == 0 && !doneLine[i].activeSelf)
                {
                    doneLine[i].SetActive(true);
                    CheckIfOrderComplete();
                }
            }
        }
    }

    private void CheckIfOrderComplete()
    {
        int totalDone = 0;
        for (int i = 0; i < cropsToOrder.Count; i++)
        {
            if (doneLine[i].activeSelf)
            {
                totalDone++;
            }
        }

        if (cropsToOrder.Count == totalDone)
        {
            GameOverManager.GameOver(true);
        }
    }
}
