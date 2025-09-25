using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FightStylesConfig", menuName = "Configs/FightStyles Config")]
public class FightStylesConfig : ScriptableObject
{
    public List<FightStyleBuffs> FightStyleBuffs;
}

[System.Serializable]
public class FightStyleBuffs
{
    public float DamageBuff;
    public float SpeedBuff;
    public float ShieldResistBuff;
    public float ResistanceBuff;
}