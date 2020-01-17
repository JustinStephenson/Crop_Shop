using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocustController : MonoBehaviour
{
    private List<Transform> dirtTransforms;
    private Animator myAnim;

    //Lerp stuff
    private Vector3 startPos;
    private Vector3 endPos;
    private bool lerp = false;
    private float lerpSpeed = 0.01f;
    private float lerpPercentage = 0;

    private float lerpToNextCropSpeed = 0.015f;
    private bool lerpToNextCrop = true;

    private int randomCrop;
    private bool attackingCrop = false;
    private bool destroy = false;

    [SerializeField]
    private float attackTimer;
    private float tempAttackTimer;

    private void Start()
    {
        tempAttackTimer = attackTimer;
        dirtTransforms = new List<Transform>();
        myAnim = GetComponent<Animator>();
        CheckForCrops();
        MoveToCrop();
    }

    private void Update()
    {
        if (lerp && !PauseGame.gamePaused)
        {
            lerpPercentage += lerpSpeed;
            transform.position = Vector3.Lerp(startPos, endPos, lerpPercentage);
            if (lerpPercentage >= 1)
            {
                transform.position = endPos;
                lerpPercentage = 0;
                lerp = false;
                DirtType d = null;
                if (dirtTransforms.Count > 0)
                {
                    d = dirtTransforms[randomCrop].GetComponent<DirtType>();
                }

                if (destroy)
                {
                    Destroy(gameObject);
                }
                else if (!d.IsCropPlanted() || d.GetCropPlanted().deadCrop)
                {
                    FlyAway();
                }
                else
                {
                    attackingCrop = true;
                    myAnim.SetBool("Flying", false);
                    GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        if (attackingCrop)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackingCrop = false;
                myAnim.SetBool("Flying", true);
                dirtTransforms[randomCrop].GetComponent<DirtType>().CropPicked();
                MoveToNextCrop();
            }
        }
    }

    public void Interact(Tool tool, Crop crop, PlayerInteractions player)
    {
        if (tool != null)
        {
            if (tool.toolset == Tool.ToolSet.bugspray)
            {
                Death();
            }
        }
    }

    private void CheckForCrops()
    {
        dirtTransforms.Clear();
        foreach (DirtType d in FindObjectsOfType<DirtType>())
        {
            if (d.IsCropPlanted() && !d.GetCropPlanted().deadCrop)
            {
                dirtTransforms.Add(d.transform);
            }
        }
    }

    private void MoveToCrop()
    {
        startPos = transform.position;
        if (dirtTransforms.Count > 0)
        {
            randomCrop = Random.Range(0, dirtTransforms.Count);
            endPos = dirtTransforms[randomCrop].position;
        }
        else
        {
            Destroy(gameObject);
        }
        lerp = true;
    }

    private void MoveToNextCrop()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        attackTimer = tempAttackTimer;
        CheckForCrops();

        if (lerpToNextCrop)
        {
            lerpToNextCrop = false;
            lerpSpeed += lerpToNextCropSpeed;
        }
        startPos = transform.position;
        randomCrop = 0;
        if (dirtTransforms.Count > 0)
        {
            Vector2 distance = new Vector2
                (
                Mathf.Abs(transform.position.x - dirtTransforms[0].position.x),
                Mathf.Abs(transform.position.y - dirtTransforms[0].position.y)
                );
            for (int i = 1; i < dirtTransforms.Count; i++)
            {
                Vector2 myDist = new Vector2
                (
                Mathf.Abs(transform.position.x - dirtTransforms[i].position.x),
                Mathf.Abs(transform.position.y - dirtTransforms[i].position.y)
                );
                if (myDist.magnitude < distance.magnitude)
                {
                    distance = myDist;
                    randomCrop = i;
                }
            }
            endPos = dirtTransforms[randomCrop].position;
            DirtType d = dirtTransforms[randomCrop].GetComponent<DirtType>();
        }
        else
        {
            FlyAway();
        }
        lerp = true;
    }

    private void Death()
    {
        DirtType d = dirtTransforms[randomCrop].GetComponent<DirtType>();
        Destroy(gameObject);
    }

    private void FlyAway()
    {
        if (!lerpToNextCrop)
        {
            lerpSpeed -= lerpToNextCropSpeed;
        }
        Vector3 tempPos = startPos;
        startPos = transform.position;
        //+ 10.0f so the crow clears the whole screen
        tempPos.x = (tempPos.x < 0) ? tempPos.x - 10.0f : tempPos.x + 10.0f;
        endPos = new Vector3(-tempPos.x, tempPos.y, tempPos.z);
        //slows down lerp speed to make up for the eztra distance
        lerpSpeed /= 1.5f;
        destroy = true;
        lerp = true;
    }
}
