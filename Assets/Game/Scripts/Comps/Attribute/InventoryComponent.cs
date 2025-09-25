using System.Collections.Generic;
using UnityEngine;

public struct InventoryComponent
{
    public Weapon CriticWeapon;
    public Weapon AlarmistWeapon;
    public Weapon ChildWeapon;
    public Weapon RebelWeapon;
    public Weapon SaverWeapon;

    public List<Rune> RuneSlots;
    public int MaxRunes;
}
