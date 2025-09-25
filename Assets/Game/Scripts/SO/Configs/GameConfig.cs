using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameC onfig")]
public class GameConfig : ScriptableObject
{
    public CommonConfig CommonConfig;
    public PlayerConfig PlayerConfig;
    public InputConfig InputConfig;
    public ItemsConfig ItemsConfig;
    public FightStylesConfig FightStylesConfig;
}
