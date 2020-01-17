using UnityEngine;
using UnityEngine.UI;

public class PlayerHoldingInfo : MonoBehaviour
{
    private Image myImage;

    [SerializeField]
    private PlayerInteractions player;

    private void Start()
    {
        myImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (player.GetTool() != null)
        {
            myImage.sprite = player.GetTool().GetToolSprite();
            //Debug.Log("tool");
        }
        else if (player.GetCrop() != null)
        {
            myImage.sprite = player.GetCrop().GetCropSprite();
            //Debug.Log("crop");
        }
        else
        {
            myImage.sprite = null;
            //Debug.Log("null");
        }
    }
}
