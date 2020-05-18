using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MinimumVerticalRotationAngle = -90;
    private const float MaximumVerticalRotationAngle = 0;
    private const float NearestZoomDistance = 5;
    private const float FurthestZoomDistance = 40;

    [SerializeField] 
    private Camera controlledCamera;
    
    [Range(1, 5)]
    [SerializeField] 
    private float mouseSensitivity = 2.5f;

    private Transform _controlledCameraTransform;
    private float _currentVerticalRotationAngle;
    private float _currentHorizontalRotationAngle;
    
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
        _currentHorizontalRotationAngle += rotationAngleY;

        float rotationAngleX = Input.GetAxis("Mouse Y") * mouseSensitivity;
        _currentVerticalRotationAngle += rotationAngleX;
        _currentVerticalRotationAngle = Mathf.Clamp(
            _currentVerticalRotationAngle, 
            MinimumVerticalRotationAngle, 
            MaximumVerticalRotationAngle
        );
            
        gameObject.transform.localEulerAngles = new Vector3(
            0, 
            _currentHorizontalRotationAngle, 
            _currentVerticalRotationAngle
        );
    }

    private void ApplyCameraZoom(int zoomValue)
    {
        float zoomDistance = _controlledCameraTransform.localPosition.x + zoomValue * mouseSensitivity / 2;
        zoomDistance = Mathf.Clamp(zoomDistance, -FurthestZoomDistance, -NearestZoomDistance);
        _controlledCameraTransform.localPosition = new Vector3(zoomDistance, 0, 0);

    }
}
