using System.IO;
using UnityEngine;

public class PathDataConverter : MonoBehaviour
{
    [Space(20)]
    [Header("Json File Path = '/PathData/ball_path.json'")]
    [Header("If path is 'Assets/PathData/ball_path.json' then")]
    [Header("Example:")]
    
    [SerializeField] 
    private string jsonFilePath;
    
    private PathData _pathData;
    
    private void Awake()
    {
        string ballPathJson = File.ReadAllText(Application.dataPath + jsonFilePath);
        _pathData = JsonUtility.FromJson<PathData>(ballPathJson);
    }

    public Vector3[] GetPathPositions()
    {
        int length = _pathData.x.Length;
        Vector3[] pathPositions = new Vector3[length];

        for (int i = 0; i < length; i++)
        {
            pathPositions[i] = new Vector3(_pathData.x[i], _pathData.y[i], _pathData.z[i]);
        }
        
        return pathPositions;
    }
}
