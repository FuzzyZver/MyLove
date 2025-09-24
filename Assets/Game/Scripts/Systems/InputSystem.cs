using Input = UnityEngine.InputSystem.InputSystem;
using UnityEngine.InputSystem;
using UnityEngine;
using Leopotam.Ecs;

public class InputSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private InputAction _moveInputAction;
    private InputAction _jumpInputAction;
    private InputAction _dashInputAction;
    private InputAction _interacionInputAction;

    private InputAction _closeAttackInputAction;
    private InputAction _farAttackInputAction;
    private InputAction _abilityInputAction;
    private InputAction _nextFightStyleInputAction;
    private InputAction _lastFightStyleInputAction;
    private InputAction[] _numberInputActions;

    private InputAction _cosoleInputAction;

    public void Init()
    {
        // оепбши рхо напюанрйх хмосрю вепег лернд RUN
        // рюйни рхо опхлемъеряъ онрнлс врн б RUN онярнъммн якедсер онксвюрэ щрнр ббнд мю йюфднл йюдпе
        string moveKeyTag = GameConfig.InputConfig.MoveKeyTag;
        _moveInputAction = Input.actions.FindAction(moveKeyTag);
        if (_moveInputAction == null)
            Debug.LogError($"[INPUT SYSTEM] Key tag |{moveKeyTag}| for move is not recognized!" +
                           "Please check Input Config or Input System Settings!");

        ////////////////////////////////////////////////////////////////////////////////////////////////

        // брнпни рхо напюанрйх хмосрю вепег лерндш, йнрнпше яксьючр янашрхъ
        // опх мюфюрхх ймнойх япюаюршбюер янашрхе х ондохяюммши лернд вепег += япюаюршбюер
        // б лернде лш сфе нропюбкъел ECS-EVENT, йнрнпши кнбхряъ б яннрберярбсчыеи яхяреле
        string jumpKeytag = GameConfig.InputConfig.JumpKeyTag;
        _jumpInputAction = Input.actions.FindAction(jumpKeytag);
        if (_jumpInputAction != null)
            _jumpInputAction.performed += OnJunpKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{jumpKeytag}| for jump is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string dashKeyTag = GameConfig.InputConfig.DashKeyTag;
        _dashInputAction = Input.actions.FindAction(dashKeyTag);
        if (_dashInputAction != null)
            _dashInputAction.performed += OnDashKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{dashKeyTag}| for dash is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string interactionKeytag = GameConfig.InputConfig.InteractionKeyTag;
        _interacionInputAction = Input.actions.FindAction(interactionKeytag);
        if (_interacionInputAction != null)
            _interacionInputAction.performed += OnInteractionKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{interactionKeytag}| for interaction is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string closeAttackKeyTag = GameConfig.InputConfig.CloseAttackKeyTag;
        _closeAttackInputAction = Input.actions.FindAction(closeAttackKeyTag);
        if (_closeAttackInputAction != null)
            _closeAttackInputAction.performed += OnCloseAttackKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{closeAttackKeyTag}| for close attack is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string farAttackKeyTag = GameConfig.InputConfig.FarAttackKeyTag;
        _farAttackInputAction = Input.actions.FindAction(farAttackKeyTag);
        if (_farAttackInputAction != null)
            _farAttackInputAction.performed += OnFarAttackKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{farAttackKeyTag}| for far attack is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string abilityKeyTag = GameConfig.InputConfig.AbilityKeyTag;
        _abilityInputAction = Input.actions.FindAction(abilityKeyTag);
        if (_abilityInputAction != null)
            _abilityInputAction.performed += OnAbilityKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{abilityKeyTag}| for ability is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string nextFightStyleKeyTag = GameConfig.InputConfig.NextFightStyleKeyTag;
        _nextFightStyleInputAction = Input.actions.FindAction(nextFightStyleKeyTag);
        if (_nextFightStyleInputAction != null)
            _nextFightStyleInputAction.performed += OnNFSKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{nextFightStyleKeyTag}| for next fight style is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string lastFightStyleKeyTag = GameConfig.InputConfig.LastFightStyleKeyTag;
        _lastFightStyleInputAction = Input.actions.FindAction(lastFightStyleKeyTag);
        if (_lastFightStyleInputAction != null)
            _lastFightStyleInputAction.performed += OnLFSKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{lastFightStyleKeyTag}| for last fight style is not recognized!" +
                               "Please check Input Config or Input System Settings!");

        string consoleTag = GameConfig.InputConfig.ConsoleTag;
        _cosoleInputAction = Input.actions.FindAction(consoleTag);
        if (_cosoleInputAction != null)
            _cosoleInputAction.performed += OnConsoleKeyPress;
        else
            Debug.LogError($"[INPUT SYSTEM] Key tag |{consoleTag}| for last fight style is not recognized!" +
                               "Please check Input Config or Input System Settings!");
        ////////////////////////////////////////////////////////////////////////////////////////////////

        // рперхи рхо напюанрйх хмосрю вепег люяяхб дкъ жхтп
        // щрн 1 б 1 йюй брнпни рхо, мн опнярн я опнцнмнл бяеу жхтп вепег жхйк
        // б мюьел яксвюе мсфем дкъ ярхкеи анъ
        _numberInputActions = new InputAction[5];
        for (int i = 0; i < 5; i++)
        {
            string numberKeyTag = (i+1).ToString();
            _numberInputActions[i] = Input.actions.FindAction(numberKeyTag);
            if (_numberInputActions[i] != null)
            {
                var i1 = i+1;
                _numberInputActions[i].performed += _ => OnNumberKeyPress(i1);
            }
            else
                Debug.LogError($"[INPUT SYSTEM] Key tag |{numberKeyTag}| for number {i} is not recognized!" +
                               "Please check Input Config or Input System Settings!");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////


    }

    private void OnNumberKeyPress(int number)
    {
        EcsWorld.NewEntity().Get<NumbersInputEvent>().Number = number;
    }

    private void OnJunpKeyPress(InputAction.CallbackContext callbackContext)
    {
        EcsWorld.NewEntity().Get<JumpInputEvent>();
    }

    private void OnDashKeyPress(InputAction.CallbackContext callbackContext)
    {
        EcsWorld.NewEntity().Get<DashInputEvent>();
    }

    private void OnInteractionKeyPress(InputAction.CallbackContext callbackContext)
    {
        EcsWorld.NewEntity().Get<InteractInputEvent>();
    }

    private void OnCloseAttackKeyPress(InputAction.CallbackContext callbackContext)
    {
        EcsWorld.NewEntity().Get<CloseAttackInputEvent>();
    }

    private void OnFarAttackKeyPress(InputAction.CallbackContext callbackContext)
    {
        EcsWorld.NewEntity().Get<FarAttackInputEvent>();
    }

    private void OnAbilityKeyPress(InputAction.CallbackContext callbackContext)
    {

    }

    private void OnNFSKeyPress(InputAction.CallbackContext callbackContext)
    {

    }

    private void OnLFSKeyPress(InputAction.CallbackContext callbackContext)
    {

    }

    private void OnConsoleKeyPress(InputAction.CallbackContext callbackContext)
    {
        EcsWorld.NewEntity().Get<ConsoleOpenCloseEvent>();
    }

    public void Run()
    {
        var moveInputValue = _moveInputAction.ReadValue<Vector2>();
        EcsWorld.NewEntity().Get<MoveInputEvent>().Vector2 = moveInputValue;

    }
}
