using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player Config")]
public class PlayerConfig : ScriptableObject
{
    public PlayerActor PlayerActor;
    public float Speed;
    public float JumpForce;
    public float DashDistance;
    public float DashTime;
    public float Health;
    public float Damage;
}
