using UnityEngine;
using Leopotam.Ecs;

public class EcsInclude: MonoBehaviour
{
    [SerializeField] private UI _ui;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private SceneData _sceneData;
    private EcsWorld _world;
    private EcsSystems _systems;

    public void Awake()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems
            //Add (new ...
            .Add(new LevelInitSystem())
            .Add(new InputSystem())
            .Add(new MovementSystem())
            .Add(new GroundRaycastSystem())
            .Add(new GroundCheckSystem())
            .Add(new JumpSystem())
            .Add(new DashSystem())
            .OneFrame<JumpInputEvent>()
            .Add(new TriggerSystem())
            .OneFrame<OnTriggerEnterEvent>()
            .Add(new HealthControlSystem())
            .Add(new EyeRaycastSystem())
            

            //OneFrame<..
            .OneFrame<MoveInputEvent>()
            .OneFrame<InteractInputEvent>()
            .OneFrame<DashInputEvent>()
            .OneFrame<NumbersInputEvent>()
            .OneFrame<GroundRaycastEvent>()
            .OneFrame<GroundEvent>()
            .OneFrame<CloseEyeRaycastEvent>()
            .OneFrame<FarEyeRaycastEvent>()


            .Inject(_world)
            .Inject(_gameConfig)
            .Inject(_ui)
            .Inject(_sceneData)

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
