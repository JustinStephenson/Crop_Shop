using UnityEngine;

public class OrderList : MonoBehaviour
{
    [SerializeField]
    private GameObject orderListPanel;

    public void ClickedOn()
    {
        if (ClickTile.pause)
        {
            ClickTile.pause = false;
            orderListPanel.SetActive(false);
        }
        else
        {
            orderListPanel.SetActive(true);
            ClickTile.pause = true;
        }
    }
}
