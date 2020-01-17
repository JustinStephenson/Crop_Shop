using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedBoxSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject box;
    public Crop crop;

    public void ClickedOn()
    {
        SeedType.crop = crop;
        SeedType.SetPlayerCrop();
        ClickTile.pause = false;
        box.SetActive(false);
    }
}
