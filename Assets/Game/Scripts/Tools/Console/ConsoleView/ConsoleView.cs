using UnityEngine;
using Leopotam.Ecs;
using TMPro;

public class ConsoleView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _consoleText;
    [SerializeField] private TextMeshProUGUI _comandInputField;
    private EcsWorld _world;

    public void Init(EcsWorld ecsWorld) => _world = ecsWorld;

    public void SendMessage()
    {
        string input = _comandInputField.text;
        if (string.IsNullOrWhiteSpace(input)) return;
        _world.NewEntity().Get<CommandEvent>().CommandName = input;
        Debug.Log($"<{input}>");
        _consoleText.text = $"\n> {input}";
        _comandInputField.text = " ";
    }

}
