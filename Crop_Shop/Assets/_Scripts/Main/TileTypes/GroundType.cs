using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundType : MonoBehaviour
{
    [SerializeField]
    private Sprite[] mySprites;

    private void Start()
    {
        int randomNum = Random.Range(0, mySprites.Length);
        GetComponent<SpriteRenderer>().sprite = mySprites[randomNum];
    }
}
