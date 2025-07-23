using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player Config")]
public class PlayerConfig : ScriptableObject
{
    public PlayerActor PlayerActor;
    public float Speed;
}
