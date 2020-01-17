using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour {

    public float orthographicSize;
    public float aspect;

    void Awake()
    {
        aspect = ((float)Screen.width / (float)Screen.height);
    }

    void Start()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(
            -orthographicSize * aspect, orthographicSize * aspect,
            -orthographicSize, orthographicSize,
            Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }
}
