using UnityEngine;
using Leopotam.Ecs;

public class AddItemSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<AddWeaponEvent> _addWeaponEventFilter;
    private EcsFilter<AddRuneEvent> _addRuneEventFilter;
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();

        foreach(int i in _addWeaponEventFilter)
        {
            int slotId = _addWeaponEventFilter.Get1(i).Slot;
            //Критик
            if(slotId == 0)
            {
                if (playerEntity.Get<InventoryComponent>().CriticWeapon == null)
                {
                    playerEntity.Get<InventoryComponent>().CriticWeapon = _addWeaponEventFilter.Get1(i).Weapon;
                    UI.Console.SetConsoleText($"" +
                        $"Id: {playerEntity.Get<InventoryComponent>().CriticWeapon.Id}" +
                        $"Name: {playerEntity.Get<InventoryComponent>().CriticWeapon.Name}");
                }
                else
                {
                    UI.Console.SetConsoleText("ЗАНЯТОООО ААА");
                }
            }
            //Тревожник
            if (slotId == 1)
            {
                if (playerEntity.Get<InventoryComponent>().AlarmingWeapon == null)
                {
                    playerEntity.Get<InventoryComponent>().AlarmingWeapon = _addWeaponEventFilter.Get1(i).Weapon;
                }
            }
            //Ребенок
            if (slotId == 2)
            {
                if (playerEntity.Get<InventoryComponent>().ChildWeapon == null)
                {
                    playerEntity.Get<InventoryComponent>().ChildWeapon = _addWeaponEventFilter.Get1(i).Weapon;
                }
            }
            //Бунтарь
            if (slotId == 3)
            {
                if (playerEntity.Get<InventoryComponent>().RebelWeapon == null)
                {
                    playerEntity.Get<InventoryComponent>().RebelWeapon = _addWeaponEventFilter.Get1(i).Weapon;
                }
            }
            //Спасатель
            if (slotId == 4)
            {
                if (playerEntity.Get<InventoryComponent>().SaverWeapon == null)
                {
                    playerEntity.Get<InventoryComponent>().SaverWeapon = _addWeaponEventFilter.Get1(i).Weapon;
                }
            }
        }
        foreach(int i in _addRuneEventFilter)
        {
            if (playerEntity.Get<InventoryComponent>().MaxRunes == playerEntity.Get<InventoryComponent>().RuneSlots.Count) return;
            playerEntity.Get<InventoryComponent>().RuneSlots.Add(_addRuneEventFilter.Get1(i).Rune);
        }
    }
}
