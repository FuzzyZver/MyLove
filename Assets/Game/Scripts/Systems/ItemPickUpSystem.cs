using UnityEngine;
using Leopotam.Ecs;
using System;

public class ItemPickUpSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<InteractInputEvent> _interactInputEventFilter;
    private EcsFilter<ItemEnterEvent> _itemEnterEventFilter;
    private EcsFilter<ItemExitEvent> _itemExitEventFilter;
    private EcsEntity _item;

    public void Init()
    {
        _item = EcsEntity.Null;
    }

    public void Run()
    {
        foreach (int i in _itemEnterEventFilter)
        {
            _item = _itemEnterEventFilter.Get1(i).Entity;
            Debug.Log("ItemPickUp");
        }
        foreach (int i in _itemExitEventFilter)
        {
            if(_item == _itemExitEventFilter.Get1(i).Entity)
            {
                _item = EcsEntity.Null;
                Debug.Log("ItemDroped");
            }
        }
        foreach(int i in _interactInputEventFilter)
        {
            if (_item == EcsEntity.Null) return;
            if (_item.Has<WeaponItemComponent>())
            {
                //Должно вызываться окно в котором игрок выберет куда положить предмет, а пока что:
                EcsWorld.NewEntity().Get<AddWeaponEvent>() = new AddWeaponEvent()
                {
                    Slot = 0,
                    Weapon = _item.Get<WeaponItemComponent>().Weapon
                };
                //костыли ыы
                GameObject.Destroy(_item.Get<GameObjectRef>().GameObject);
                _item.Destroy();
            }
            else if (_item.Has<RuneItemComponent>())
            {
                EcsWorld.NewEntity().Get<AddRuneEvent>().Rune = _item.Get<RuneItemComponent>().Rune;
            }
        }

    }
}
