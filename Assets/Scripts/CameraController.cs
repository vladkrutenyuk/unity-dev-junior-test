using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MinimumRotationAngleX = -90;
    private const float MaximumRotationAngleX = 0;
    private const float NearestZoomDistance = 5;
    private const float FurthestZoomDistance = 40;

    [SerializeField] 
    private Camera controlledCamera;
    
    [Range(1, 5)]
    [SerializeField] 
    private float mouseSensitivity = 2.5f;

    private Transform _controlledCameraTransform;
    private float _currentRotationAngleX;
    private float _currentRotationAngleY;
    
    private void Start()
    {
        _controlledCameraTransform = controlledCamera.transform;
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ApplyRotation();
        }

        if (Math.Abs(Input.mouseScrollDelta.y) > 0)
        {
            ApplyCameraZoom((int)Input.mouseScrollDelta.y);
        }
    }
    
    public void FocusObject(Vector3 focusedObjectPosition)
    {
        gameObject.transform.position = focusedObjectPosition;
    }

    private void ApplyRotation()
    {
        float rotationAngleY = Input.GetAxis("Mouse X") * mouseSensitivity;
        _currentRotationAngleY += rotationAngleY;

        float rotationAngleX = Input.GetAxis("Mouse Y") * mouseSensitivity;
        _currentRotationAngleX += rotationAngleX;
        _currentRotationAngleX = Mathf.Clamp(
            _currentRotationAngleX, 
            MinimumRotationAngleX, 
            MaximumRotationAngleX
        );
            
        gameObject.transform.localEulerAngles = new Vector3(
            0, 
            _currentRotationAngleY, 
            _currentRotationAngleX
        );
    }

    private void ApplyCameraZoom(int zoomValue)
    {
        float zoomDistance = _controlledCameraTransform.localPosition.x + zoomValue * mouseSensitivity / 2;
        zoomDistance = Mathf.Clamp(zoomDistance, -FurthestZoomDistance, -NearestZoomDistance);
        _controlledCameraTransform.localPosition = new Vector3(zoomDistance, 0, 0);

    }
}
