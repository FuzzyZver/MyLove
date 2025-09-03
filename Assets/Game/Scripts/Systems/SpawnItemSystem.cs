using UnityEngine;
using Leopotam.Ecs;

public class SpawnItemSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<SpawnItemEvent> _spawnItemEventFilter;
    private ItemsConfig _itemsConfig;

    public void Init()
    {
        _itemsConfig = GameConfig.ItemsConfig;
    }

    public void Run()
    {
        foreach(int i in _spawnItemEventFilter)
        {
            ItemActor item = GameObject.Instantiate(_itemsConfig.ItemActor, _spawnItemEventFilter.Get1(i).SpawnPoint, Quaternion.identity);
            item.Init(EcsWorld);
            var itemEntity = item.GetEntity();

            if (_spawnItemEventFilter.Get1(i).Weapon != null)
            {
                itemEntity.Get<WeaponItemComponent>().Weapon = _spawnItemEventFilter.Get1(i).Weapon;
                itemEntity.Get<SpriteRef>().SpriteRenderer.sprite = _spawnItemEventFilter.Get1(i).Weapon.Sprite;
            }
            else if (_spawnItemEventFilter.Get1(i).Rune != null)
            {
                itemEntity.Get<RuneItemComponent>().Rune = _spawnItemEventFilter.Get1(i).Rune;
                itemEntity.Get<SpriteRef>().SpriteRenderer.sprite = _spawnItemEventFilter.Get1(i).Rune.Sprite;
            }
        }
    }
}
