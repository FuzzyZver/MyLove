using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsConfig", menuName = "Configs/Items Config")]
public class ItemsConfig : ScriptableObject
{
    public ItemActor ItemActor;
    public List<Weapon> CloseWeapons;
    public List<Weapon> FarWeapons;

    public List<Rune> runes;
}
