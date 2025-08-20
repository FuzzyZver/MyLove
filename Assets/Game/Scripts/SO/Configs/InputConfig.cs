using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "Configs/Input Config")]
public class InputConfig : ScriptableObject
{
    [Header("KeyTags")]
    public string MoveKeyTag;
    public string InteractionKeyTag;
    public string DashKeyTag;
    public string JumpKeyTag;

    public string CloseAttackKeyTag;
    public string FarAttackKeyTag;
    public string AbilityKeyTag;

    public string NextFightStyleKeyTag;
    public string LastFightStyleKeyTag;

    public string ConsoleTag;

    [Space]
    [Header("OtherProps")]
    public float MoveInputGravity;
    public float GroundDistanceThreshold;
    public float JumpDuration;
    public float EyeDistanceThreshold;
}
