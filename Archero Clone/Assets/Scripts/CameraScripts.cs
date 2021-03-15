using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    private Camera cam;
    private Transform playerTransform;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Player.Instance.transform;
        offset = transform.position - playerTransform.position;
        cam = Camera.main;
        float width = Screen.width;
        float height = Screen.height;
        float designRatio = 16.0f / 9;
        float targetRatio = height / width;
        float size = cam.orthographicSize;
        float result = targetRatio * size / designRatio;
        cam.orthographicSize = result;
    }

    private void LateUpdate()
    {
        //transform.position = playerTransform.position + offset;
        Vector3 cameraPos = Vector3.zero;
        cameraPos.y = playerTransform.position.y + offset.y;
        cameraPos.z = playerTransform.position.z + offset.z;
        transform.position = cameraPos;
    }
}
