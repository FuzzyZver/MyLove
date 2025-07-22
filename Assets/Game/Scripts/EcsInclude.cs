using UnityEngine;
using Leopotam.Ecs;

public class EcsInclude: MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private GameConfig _gameConfig;
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Awake()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems
            //Add (new ...
            .Add(new InputSystem())

            //OneFrame<..
            .OneFrame<MoveInputEvent>()
            .OneFrame<InteractInputEvent>()
            .OneFrame<JumpInputEvent>()
            .OneFrame<DashInputEvent>()
            .OneFrame<NumbersInputEvent>()


            .Inject(_world)
            .Inject(_gameConfig)
            .Inject(_ui)

            .Init();
    }

    public void Update()
    {
        _systems.Run();
    }

    public void Destroy()
    {
        _systems.Destroy();
        _world.Destroy();
    }
}
