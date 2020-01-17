using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Each character will have there own gimick

    private BoxCollider2D crowHitbox;   //Scarecrow character has a bigger crow hitbox

    private void Start()
    {
        crowHitbox = GetComponentsInChildren<BoxCollider2D>()[1];
    }
}
