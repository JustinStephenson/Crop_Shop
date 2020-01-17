using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int tileX = 0;
    public int tileY = 0;
    public TileMap map;

    private PlayerInteractions playerAct;
    private Animator myAnim;

    public float moveSpeed = 2f;

    private int count = 1;

    public List<Node> currentPath = null;

    void Start()
    {
        playerAct = GetComponent<PlayerInteractions>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (currentPath != null)
        {
            Draw();
            Move();
        }
    }

    private void Draw()
    {
        int currentNode = 0;

        while (currentNode < currentPath.Count - 1)
        {
            Vector3 start = map.TileCoordToWorldCoord(currentPath[currentNode].x, currentPath[currentNode].y)
                                + new Vector3(0, 0, -1f);
            Vector3 end = map.TileCoordToWorldCoord(currentPath[currentNode + 1].x, currentPath[currentNode + 1].y)
                              + new Vector3(0, 0, -1f);

            Debug.DrawLine(start, end, Color.red);
            currentNode++;
        }
    }

    private void Move()
    {
        if (ClickTile.click && !PauseGame.gamePaused)
        {
            label:
            if (count < currentPath.Count)
            {
                float cost = map.CostToEnterTile(currentPath[count].x, currentPath[count].y,
                                 currentPath[count].x, currentPath[count].y);

                if (cost != 1)
                {
                    Turn();
                    Arrived();
                }
                else
                {
                    tileX = currentPath[count].x;
                    tileY = currentPath[count].y;

                    Vector3 startPos = transform.position;
                    Vector3 endPos = map.TileCoordToWorldCoord(tileX, tileY) + new Vector3(0, 0, -1f);
                    float dist = Vector3.Distance(startPos, endPos);
                    float lerpDistance = moveSpeed;
                    float lerpPercent = lerpDistance / dist;
                    transform.position = Vector3.Lerp(startPos, endPos, lerpPercent);

                    if (dist < 0.05)
                    {
                        transform.position = endPos;
                        count++;
                        goto label;
                    }
                }
            }
            else
            {
                Arrived();
            }
        }
    }

    private void Arrived()
    {
        count = 1;
        ClickTile.click = false;
        PlayerAnimations.canMove = false;
    }

    private void Turn()
    {
        float xPos = currentPath[currentPath.Count - 1].x;
        float yPos = currentPath[currentPath.Count - 1].y;

        PlayerAnimations.canMove = false;

        if (transform.position.x < xPos)
        {
            playerAct.LeftCol();
            myAnim.SetInteger("yAxis", 0);
            myAnim.SetInteger("xAxis", 1);
        }
        else if (transform.position.x > xPos)
        {
            
            playerAct.RightCol();
            myAnim.SetInteger("yAxis", 0);
            myAnim.SetInteger("xAxis", -1);
        }
        else if (transform.position.y < yPos)
        {
            playerAct.UpCol();
            myAnim.SetInteger("yAxis", 1);
            myAnim.SetInteger("xAxis", 0);
        }
        else if (transform.position.y > yPos)
        {
            playerAct.DownCol();
            myAnim.SetInteger("yAxis", -1);
            myAnim.SetInteger("xAxis", 0);
        }

        playerAct.TurnOnInteract();
    }
}
