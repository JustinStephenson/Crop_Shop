using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator myAnim;

    public static bool canMove = false;

    float lastValueX;
    float lastValueY;

    void Start()
    {
        myAnim = GetComponent<Animator>();

        lastValueX = transform.position.x;
        lastValueY = transform.position.y;
    }

    void Update()
    {
        if (canMove)
        {
            PlayerDir();
        }
        else
        {
            myAnim.SetLayerWeight(1, 0);
        }
    }

    private void PlayerDir()
    {
        myAnim.SetLayerWeight(1, 1);
        if (transform.position.x > lastValueX)
        {
            myAnim.SetInteger("yAxis", 0);
            myAnim.SetInteger("xAxis", 1);
        }
        else if (transform.position.x < lastValueX)
        {
            myAnim.SetInteger("yAxis", 0);
            myAnim.SetInteger("xAxis", -1);
        }
        else if (transform.position.y > lastValueY)
        {
            myAnim.SetInteger("xAxis", 0);
            myAnim.SetInteger("yAxis", 1);
        }
        else if (transform.position.y < lastValueY)
        {
            myAnim.SetInteger("xAxis", 0);
            myAnim.SetInteger("yAxis", -1);
        }

        lastValueX = transform.position.x;
        lastValueY = transform.position.y;
    }

    private void CancelMoveAnim()
    {
        myAnim.SetInteger("xAxis", 0);
        myAnim.SetInteger("yAxis", 0);
    }
}
