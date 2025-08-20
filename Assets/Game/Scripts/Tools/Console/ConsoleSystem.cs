using UnityEngine;
using Leopotam.Ecs;
using System.Collections.Generic;
using System.Reflection;
using System;

public class ConsoleSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter<CommandEvent> _commandEventFilter;
    private EcsFilter<ConsoleOpenCloseEvent> _consoleOpenCloseEventFilter;
    private Dictionary<string, Command> _commands = new Dictionary<string, Command>();
    
    public void Init()
    {
        var methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute<CommandAttribute>();
            if (attr != null)
            {
                _commands[attr.Name.ToLower()] = new Command(attr.Name.ToLower(), method, this, new List<Type>());
            }

            var attrArgs = method.GetCustomAttribute<CommandArgsAttribute>();
            if (attrArgs != null)
            {
                _commands[attrArgs.Name.ToLower()] = new Command(attrArgs.Name.ToLower(), method, this, attrArgs.ParamTypes);
            }
        }
    }

    public void Run()
    {
        foreach (int i in _commandEventFilter)
        {
            string inputText = _commandEventFilter.Get1(i).CommandName;

            string[] parts = inputText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) continue;

            string comandName = parts[0].ToLower();

            if(_commands.TryGetValue(comandName, out var command))
            {
                try
                {
                    if (command.ParamTypes.Count == 0)
                    {
                        command.Method.Invoke(command.Target, null);
                    }
                    else
                    {
                        var args = new object[command.ParamTypes.Count];
                        for(int j = 0; j < command.ParamTypes.Count; j++)
                        {
                            args[j] = Convert.ChangeType(parts[j + 1], command.ParamTypes[j]);
                        }
                        command.Method.Invoke(command.Target,  args);
                    }
                }
                catch(Exception e)
                {
                    Debug.LogError($"Ошибка при выполнении команды {comandName}: {e}");
                }
            }
            else
            {
                Debug.Log($"Команда {comandName} не найдена");
            }

        }
        foreach(int i in _consoleOpenCloseEventFilter)
        {
            var console = UI.Console.gameObject;
            if (console.activeSelf)
            {
                console.SetActive(false);
            }
            else if (!console.activeSelf)
            {
                console.SetActive(true);
            }
        }
    }

    [Command("help")]
    public void Help()
    {
        foreach (var kvp in _commands)
        {
            Debug.Log($"{kvp.Key} - {kvp.Value.Name}");
        }
    }

    [CommandArgs("addhp", typeof(int))]
    public void AddHealth(int value)
    {
        SceneData.PlayerOnScene.GetEntity().Get<HealthComponent>().HealthValue += value;
        Debug.Log($"{SceneData.PlayerOnScene.GetEntity().Get<HealthComponent>().HealthValue}");
    }

    [Command("kill")]
    public void Kill()
    {
        SceneData.PlayerOnScene.GetEntity().Get<HealthComponent>().HealthValue = -100000;
    }
}
