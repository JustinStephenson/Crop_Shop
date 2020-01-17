using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

    private BoxCollider2D myCol;

    [SerializeField]
    private GameObject seedboxPanel;
    [SerializeField]
    private GameObject toolboxPanel;

    [SerializeField]
    private bool cropGrownOrDead = false;
    private bool locustAttack = false;

    [SerializeField]
    private Crop crop;
    [SerializeField]
    private Tool tool;

    void Start()
    {
        myCol = GetComponent<BoxCollider2D>();
        myCol.enabled = false;

        crop.cropAsset = null;
        crop = null;
        tool = null;
    }

    public void SetTool(Tool tool)
    {
        //Debug.Log("Set tool: " + tool.name);
        this.tool = tool;
        crop = null;
    }

    public void SetCrop(Crop crop)
    {
        //Debug.Log("Set crop: " + crop);
        this.crop = crop;
        tool = null;
    }

    public Tool GetTool()
    {
        return tool;
    }

    public Crop GetCrop()
    {
        return crop;
    }

    public void SetGrownOrDead(bool state)
    {
        cropGrownOrDead = state;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!cropGrownOrDead && !locustAttack)
        {
            if (other.CompareTag("dirt"))
            {
                Debug.Log("iteract with dirt");
                other.GetComponent<DirtType>().Interact(tool, crop, this);
            }
            if (other.CompareTag("seedbox"))
            {
                seedboxPanel.SetActive(true);
                ClickTile.pause = true;
                other.GetComponent<SeedType>().Interact(tool, crop, this);
            }
            if (other.CompareTag("toolbox"))
            {
                toolboxPanel.SetActive(true);
                ClickTile.pause = true;
                other.GetComponent<ToolType>().Interact(tool, crop, this);
            }
            if (other.CompareTag("locust"))
            {
                other.GetComponent<LocustController>().Interact(tool, crop, this);
                locustAttack = true;
            }
        }

        if (other.CompareTag("trash"))
        {
            other.GetComponent<TrashType>().Interact(tool, crop, this);
        }
        if (other.CompareTag("truck"))
        {
            other.GetComponent<TruckType>().Interact(tool, crop, this);
        }
        if (other.CompareTag("phone"))
        {
            other.GetComponent<PhoneType>().Interact(tool, crop, this);
        }
    }

    void OnTriggerStay2D()
    {
        //Turn off Interact
        myCol.enabled = false;
        locustAttack = false;
    }

    public void TurnOnInteract()
    {
        //Turn on Interact
        myCol.enabled = true;
    }

    public void UpCol()
    {
        myCol.size = new Vector2(0.1f, 0.5f);
        myCol.offset = new Vector2(0f, 0.5f);
    }

    public void DownCol()
    {
        myCol.size = new Vector2(0.1f, 0.5f);
        myCol.offset = new Vector2(0f, -0.5f);
    }

    public void LeftCol()
    {
        myCol.size = new Vector2(0.5f, 0.1f);
        myCol.offset = new Vector2(0.5f, 0f);
    }

    public void RightCol()
    {
        myCol.size = new Vector2(0.5f, 0.1f);
        myCol.offset = new Vector2(-0.5f, 0f);
    }
}
