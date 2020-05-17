using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private Camera controlledCamera;
    
    [Range(1, 5)]
    [SerializeField] 
    private float mouseSensitivity = 2.5f;

    private Transform _controlledCameraTransform;
    
    private void Start()
    {
        _controlledCameraTransform = controlledCamera.transform;
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 point = gameObject.transform.position;
            
            _controlledCameraTransform.RotateAround(
                point,
                Vector3.up,
                Input.GetAxis("Mouse X") * mouseSensitivity
            );
            
            _controlledCameraTransform.RotateAround(
                point,
                _controlledCameraTransform.right,
                - Input.GetAxis("Mouse Y") * mouseSensitivity
            );
        }

        if (Math.Abs(Input.mouseScrollDelta.y) > 0)
        {
            ChangeCameraDistance((int)Input.mouseScrollDelta.y);
        }
    }
    
    public void FocusObject(Vector3 focusedObjectPosition)
    {
        gameObject.transform.position = focusedObjectPosition;
    }

    private void ChangeCameraDistance(int scroll)
    {
        _controlledCameraTransform.position += _controlledCameraTransform.forward * (scroll * mouseSensitivity / 2);
    }
}
