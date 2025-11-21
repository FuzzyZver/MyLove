using UnityEngine;

[CreateAssetMenu(fileName = "CommonConfig", menuName = "Configs/Common Config")]
public class CommonConfig : ScriptableObject
{
    [Header("Background")]
    public GameObject ForegroundPrefab;
    public GameObject MiddlegroundPrefab;
    public GameObject BackgroundPrefab;
}
