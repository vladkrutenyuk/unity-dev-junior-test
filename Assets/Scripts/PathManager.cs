using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour
{
    [SerializeField] 
    private PathMovement[] paths;
    [SerializeField] 
    private Slider speedMltSlider;
    [SerializeField] 
    private Text currentPathNumberText;
    [SerializeField] 
    private CameraController cameraController;

    private int _currentPathIndex = 0;

    private void Start()
    {
        speedMltSlider.value = paths[_currentPathIndex].speedMultiplier;

        SetVisualForCurrentSelected();
    }

    private void Update()
    {
        cameraController.FocusObject(paths[_currentPathIndex].GetObjectPathFollowerPosition());
    }

    public void OnValueChangedSpeedMltSlider()
    {
        paths[_currentPathIndex].speedMultiplier = speedMltSlider.value;
    }

    public void OnClickNextPathButton()
    {
        SwitchPath(1);
    }
    
    public void OnClickPreviousPathButton()
    {
        SwitchPath(-1);
    }

    private void SwitchPath(int shift)
    {
        if (paths.Length < 2)
        {
            return;
        }
        
        paths[_currentPathIndex].Pause();

        int nextPathIndex = _currentPathIndex + shift;

        if (nextPathIndex > paths.Length - 1)
        {
            nextPathIndex -= paths.Length;
        }
        else if (nextPathIndex < 0)
        {
            nextPathIndex += paths.Length;
        }
        
        paths[_currentPathIndex].SetCurrentFollowerObjectColor(false);
        _currentPathIndex = nextPathIndex;
        SetVisualForCurrentSelected();
    }

    private void SetVisualForCurrentSelected()
    {
        currentPathNumberText.text = (_currentPathIndex + 1).ToString();
        
        paths[_currentPathIndex].SetCurrentFollowerObjectColor(true);
    }
}
