using UnityEngine;

[CreateAssetMenu(fileName = "CommonConfig", menuName = "Configs/Common Config")]
public class CommonConfig : ScriptableObject
{
    [Header("Environment")]
    public Sprite OpenDoorSprite;
    public Sprite CloseDoorSprite;
    public float DoorCloseTime;

    [Header("Background")]
    public GameObject ForegroundPrefab;
    public GameObject MiddlegroundPrefab;
    public GameObject BackgroundPrefab;
}
