using UnityEngine;
using UnityEngine.UI;

public class ToolBoxSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject box;
    public Tool tool;

    private void Start()
    {
        GetComponent<Image>().sprite = tool.GetToolSprite();
    }

    public void ClickedOn()
    {
        ToolType.tool = this.tool;
        ToolType.SetPlayerTool();
        ClickTile.pause = false;
        box.SetActive(false);
    }
}
