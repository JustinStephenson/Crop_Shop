using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    [SerializeField]
    private Sprite[] characterList;
    private int index;
    private Image myImage;

    private void Start()
    {
        index = 0;
        myImage = GetComponent<Image>();
        myImage.sprite = characterList[index];
    }

    public void ClickLeft()
    {
        index--;
        if (index < 0)
        {
            index = characterList.Length - 1;
        }
        SelectCharacter();
    }

    public void CLickRight()
    {
        index++;
        if (index > characterList.Length - 1)
        {
            index = 0;
        }
        SelectCharacter();
    }

    private void SelectCharacter()
    {
        myImage.sprite = characterList[index];
    }
}
