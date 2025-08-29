using UnityEngine;
using Leopotam.Ecs;

public class RemoveItemSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<RemoveItemEvent> _removeItemEventFilter;
    private PlayerActor _player;

    public void Init()
    {
        _player = SceneData.PlayerOnScene;
    }

    public void Run()
    {
        var playerEntity = _player.GetEntity();

        foreach(int i in _removeItemEventFilter)
        {
            int itemType = _removeItemEventFilter.Get1(i).ItemType;
            int slot = _removeItemEventFilter.Get1(i).Slot;

            if(itemType == 0)
            {//weapon
                Weapon dropWeapon;
                if(slot == 0)
                {
                    dropWeapon = playerEntity.Get<InventoryComponent>().CriticWeapon;
                    playerEntity.Get<InventoryComponent>().CriticWeapon = null;
                    UI.Console.SetConsoleText("БЛЯЯ УДАЛИЛИ");
                }
                if (slot == 1)
                {
                    dropWeapon = playerEntity.Get<InventoryComponent>().AlarmingWeapon;
                    playerEntity.Get<InventoryComponent>().AlarmingWeapon = null;
                    UI.Console.SetConsoleText("ЫХЫХыхыхых а тут и так пусто)");
                }
                if (slot == 2)
                {
                    dropWeapon = playerEntity.Get<InventoryComponent>().ChildWeapon;
                    playerEntity.Get<InventoryComponent>().ChildWeapon = null;
                }
                if (slot == 3)
                {
                    dropWeapon = playerEntity.Get<InventoryComponent>().RebelWeapon;
                    playerEntity.Get<InventoryComponent>().RebelWeapon = null;
                }
                if (slot == 4)
                {
                    dropWeapon = playerEntity.Get<InventoryComponent>().SaverWeapon;
                    playerEntity.Get<InventoryComponent>().SaverWeapon = null;
                }
                //EcsWorld.NewEntity().Get<DropItemEvent>... короче система дропа предметов которую надо реализовать в будущем

            }
            else if(itemType == 1)
            {//runes
                playerEntity.Get<InventoryComponent>().RuneSlots.RemoveAt(slot);
            }
            else
            {
                Debug.LogError("This item type does not exist");
            }
        }
    }
}
