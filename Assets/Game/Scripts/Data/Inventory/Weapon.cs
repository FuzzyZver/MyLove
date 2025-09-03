using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Weapon
{
    public int Id;
    public string Name;
    public Sprite Sprite;
    [Header("Properties")]
    public float Damage;
    public float KickSpeed;
    public List<Rune> Runes;
}
