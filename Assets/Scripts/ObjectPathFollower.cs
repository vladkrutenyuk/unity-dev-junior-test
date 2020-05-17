using UnityEngine;

public class ObjectPathFollower : MonoBehaviour
{
    private float _doubleClickDelayTime = 0.2f;
    private double _lastMouseClickTime;
    
    [HideInInspector] 
    public PathMovement pathMovement;
    public ObjectPathFollowerEffects effects;

    private void OnMouseDown()
    {

        if (IsDoubleClick())
        {
            DoubleClick();
        }
        else
        {
            SingleClick();
        }
        
        _lastMouseClickTime = Time.time;
    }

    private void SingleClick()
    {
        pathMovement.Play();
    }
    
    private void DoubleClick()
    {
        pathMovement.Stop();
    }

    private bool IsDoubleClick()
    {
        return Time.time - _lastMouseClickTime <= _doubleClickDelayTime;
    }
}
