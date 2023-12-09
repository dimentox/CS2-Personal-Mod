using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Colossal.Plugins;
using Colossal.Serialization.Entities;
using Game;
using Game.Modding;
using Game.SceneFlow;
using HarmonyLib;
using MyFirstPlugin;
using Unity.Entities;
using UnityEngine;


namespace TargetMethodsDemo.Patches;

[HarmonyPatch(typeof(GameManager), "CreateSystems")]
public class GameManager_CreateSystems_Patch
{
    public static void Postfix(GameManager __instance)
    {
        Plugin.Wrapper = new Wrapper();
        Plugin.Wrapper.Start();
    }
}

public class Wrapper
{
    public MyPluginManager managerTest;

    public void Start()
    {
        Debug.LogWarning("*****LOADED MANAGER *****");
        managerTest =
            new MyPluginManager(
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Mods")).FullName, typeof(IMod));
    }
}

public class MyPluginManager : PluginManager
{
    public List<IMod> Mods = new();
    public List<PluginInfo> Plugins = new();

    public MyPluginManager(string rootPath, params Type[] types) : base(rootPath, types)
    {
        GameManager.instance.onGameLoadingComplete += OnGameLoadingComplete;
    }

    public event Action<PluginInfo> PluginLoaded;
    public event Action<PluginInfo> PluginUnloaded;
    public event Action OnDispose;

    public void Start()
    {
    }

    private void OnGameLoadingComplete(Purpose purpose, GameMode mode)
    {
        Debug.LogWarning("****GAMELOADING COMPLETE*****");
        if (mode == GameMode.MainMenu)
        {
            Debug.LogWarning("*****INMENU*****");
            StartWatching();
        }
        else if (mode == GameMode.Game)
        {
            Debug.LogWarning("*****INGAME*****");
            Debug.LogWarning("*****PLUGINS COUNT*****");
            Debug.LogWarning(Plugins.Count);
            Debug.LogWarning("*****Making UpdateSystem*****");
            var update = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<UpdateSystem>();
            foreach (var pluginInfo in Plugins)
            {
                Debug.LogWarning("*****Loading a mod*****");
                var mod = loaMod(pluginInfo);
                if (mod != null)
                {
                    Debug.LogWarning("*****Mod loaded sending world*****");
                    mod.OnCreateWorld(update);
                    Mods.Add(mod);
                }
            }

            Debug.LogWarning("****MODCOUNT*****");
            Debug.LogWarning(Mods.Count);
        }
    }

    private IMod loaMod(PluginInfo info)
    {
        var tmods = new List<IMod>();
        try
        {
            var assem = info.assembly;

            info.assembly.GetTypes()
                .Where(t => t != typeof(IMod) && typeof(IMod).IsAssignableFrom(t))
                .ToList()
                .ForEach(x =>
                    {
                        var mod = (IMod)Activator.CreateInstance(x);
                        mod.OnLoad();
                        tmods.Add(mod);
                    }
                );
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }

        return tmods.FirstOrDefault();
    }

    protected override void OnPluginLoaded(PluginInfo info)
    {
        base.OnPluginLoaded(info);
        Debug.LogWarning(("*****LOADED PLUGIN {0}*****", info.assemblyPath));
        PluginLoaded?.Invoke(info);
        Plugins.Add(info);
    }

    private void Dispose()
    {
        var OnOnDispose = OnDispose;
        if (OnOnDispose != null) OnOnDispose();

        base.Dispose();
        PluginLoaded = null;
        PluginUnloaded = null;
    }

    protected override void OnPluginUnloaded(PluginInfo info)
    {
        base.OnPluginUnloaded(info);
        PluginUnloaded?.Invoke(info);
    }
}