using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float camSpeed = 25.0f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float zoomAmount = 2f; 
    private float zoomSpeed = 5f; 
    private float orthographicSize;
    private float targetOrthographicSize;


    private void Start() {
        orthographicSize = virtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void Update()
    {
        handleMovement();
        handleZoom();
    }

    private void handleMovement() {
        // Handles camera movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(x,y).normalized;
        transform.position += moveDir * camSpeed * Time.deltaTime;
    }

    private void handleZoom() {
        // Handles Zoom of movement.
        targetOrthographicSize += Input.mouseScrollDelta.y * -zoomAmount;

        float minOrthographicSize = 10; // minimumZoomOut
        float maxOrthographicSize = 30; // maximumZoomOut
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize,minOrthographicSize,maxOrthographicSize); // Locks zoom between two options

        orthographicSize = Mathf.Lerp(orthographicSize,targetOrthographicSize,Time.deltaTime * zoomSpeed); // Making the zoom go smooth
        virtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
