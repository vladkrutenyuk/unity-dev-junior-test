using UnityEngine;

public class PathMovement : MonoBehaviour
{
    [SerializeField] 
    private PathDataConverter pathDataConverter;
    [SerializeField] 
    private LineRenderer lineRenderer;
    [SerializeField] 
    private ObjectPathFollower objectPathFollower;

    [Range(0, 1)] 
    [SerializeField] 
    private float maximumSpeed = 0.5f;

    private Vector3[] _pathPositions;
    private Transform _objectPathFollowerTransform;
    private float _currentPathTime = 0;
    private bool _isPlaying;
    
    [Range(0, 1)] 
    public float speedMultiplier = 0.5f;
    
    private void Awake()
    {
        objectPathFollower.pathMovement = this;
    }

    private void Start()
    {
        _pathPositions = pathDataConverter.GetPathPositions();
        
        lineRenderer.positionCount = _pathPositions.Length;
        lineRenderer.SetPositions(_pathPositions);

        _objectPathFollowerTransform = objectPathFollower.transform;
        
        SetCurrentPosition(0);
    }

    private void Update()
    {
        if (speedMultiplier > 0 && _isPlaying)
        {
            _currentPathTime += maximumSpeed * speedMultiplier * Time.deltaTime;
            SetCurrentPosition(_currentPathTime);

            if (_currentPathTime >= 1)
            {
                Pause();
            }
        }
    }
    
    public void Play()
    {
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false;
        _currentPathTime = 0;
        SetCurrentPosition(_currentPathTime);
    }

    public void Pause()
    {
        _isPlaying = false;
    }
    
    public void SetCurrentFollowerObjectColor(bool isSelected)
    {
        objectPathFollower.effects.SetCurrentColor(isSelected);
    }

    public Vector3 GetObjectPathFollowerPosition()
    {
        return _objectPathFollowerTransform.position;
    }

    private void SetCurrentPosition(float pathTime)
    {
        pathTime = Mathf.Clamp01(pathTime);

        float currentPathPoint = Mathf.Lerp(0, _pathPositions.Length - 1, pathTime);
        int backPathPoint = (int)currentPathPoint;
        int nextPathPoint = backPathPoint + 1;
        float interpolationBetweenPoints = currentPathPoint - backPathPoint;

        _objectPathFollowerTransform.position = Vector3.Lerp(
            _pathPositions[backPathPoint], 
            _pathPositions[nextPathPoint < _pathPositions.Length ? nextPathPoint : backPathPoint], 
            interpolationBetweenPoints
        );

        SetCurrentPathLine(backPathPoint);
    }

    private void SetCurrentPathLine(int backPathPoint)
    {
        int length = backPathPoint + 2;
        if (length > _pathPositions.Length)
        {
            length = _pathPositions.Length;
        }
        
        lineRenderer.positionCount = length;
        
        for (int i = 0; i < length; i++)
        {
            lineRenderer.SetPosition(i, _pathPositions[i]);
        }
        
        lineRenderer.SetPosition(length - 1, _objectPathFollowerTransform.position);
    }
}
