using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField]
    private Transform bar;

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, bar.localScale.y, bar.localScale.z);
    }
}
