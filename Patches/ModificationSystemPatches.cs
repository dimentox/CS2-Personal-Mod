using Colossal.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Plugins;
using UnityEngine;
using PluginManagerTest = TargetMethodsDemo.Patches.PluginManagerTest;
using Game.Citizens;
using Game.SceneFlow;
using MyFirstPlugin;
using static Game.Audio.Radio.Radio;
using Unity.Burst.Intrinsics;
using Unity.Entities.UniversalDelegates;
using Game;
using Game.Serialization;
using Unity.Entities;
using IMod = Game.Modding.IMod;
using HarmonyLib;
using UnityEngine.InputSystem.HID;

namespace Game.Debug.Tests
{
    [TestDescriptor("Test Modding", Category.General, false, TestPhase.Default, false)]
    // Token: 0x0200000D RID: 13
    public class TestScenarioExample : TestScenario
    {
        // Token: 0x06000051 RID: 81 RVA: 0x000043C8 File Offset: 0x000025C8
        protected override async Task OnPrepare()
        {
            TestScenario.log.Info("OnPrepare");
            TestUtility.SetDefaultTestConditions();
            await Task.CompletedTask;
        }

        // Token: 0x06000052 RID: 82 RVA: 0x00004404 File Offset: 0x00002604
        protected override async Task OnCleanup()
        {
            TestScenario.log.Info("OnCleanup");
            await Task.CompletedTask;
        }

        // Token: 0x06000053 RID: 83 RVA: 0x0000443F File Offset: 0x0000263F
        [TestPrepare]
        private void TestPrepare()
        {
            TestScenario.log.Info("TestPrepare");
        }

        // Token: 0x06000054 RID: 84 RVA: 0x00004450 File Offset: 0x00002650
        [TestCleanup]
        private void TestCleanup()
        {
            TestScenario.log.Info("TestPrepare");
        }

        // Token: 0x06000055 RID: 85 RVA: 0x00004461 File Offset: 0x00002661
        [Test]
        private void TestExampleVoid()
        {
            PluginManagerTest managerTest = new PluginManagerTest();
            managerTest.Start();
            TestScenario.log.Info("TestExampleVoid");
        }

        // Token: 0x06000056 RID: 86 RVA: 0x00004474 File Offset: 0x00002674
        [Test]
        private async Task TestExampleAsync()
        {
            TestScenario.log.Info("TestExampleAsync");
            await Task.CompletedTask;
        }
    }
}

namespace TargetMethodsDemo.Patches
{

    [HarmonyPatch(typeof(GameManager), "CreateSystems")]
    public class GameManager_CreateSystems_Patch
    {
        public static PluginManagerTest managerTest = new PluginManagerTest();

        public static void Postfix(GameManager __instance)
        {
            Debug.LogWarning("*****LOADED MANAGER *****");

            managerTest.Start();
        }
    }

    public partial class MyPluginManager : PluginManager
    {
        public event Action<PluginInfo> PluginLoaded;
        public event Action<PluginInfo> PluginUnloaded;
        public event Action OnDispose;

        public MyPluginManager(string rootPath, params Type[] types) : base(rootPath, types)
        {
        }

        protected override void OnPluginLoaded(PluginInfo info)
        {
            base.OnPluginLoaded(info);
            Debug.LogWarning(("*****LOADED PLUGIN {0}*****", info.assemblyPath.ToString()));
            this.PluginLoaded?.Invoke(info);

        }

        void Dispose()
        {
            var OnOnDispose = this.OnDispose;
            if (OnOnDispose != null) OnOnDispose();

            base.Dispose();
            this.PluginLoaded = null;
            this.PluginUnloaded = null;
        }

        protected override void OnPluginUnloaded(PluginInfo info)
        {
            base.OnPluginUnloaded(info);
            this.PluginUnloaded?.Invoke(info);
        }
    }
}
//public class PluginManagerTest : MonoBehaviour
//    {

//        public List<Game.Modding.IMod> Mods = new List<IMod>();
//        // Token: 0x0600000F RID: 15 RVA: 0x000020D4 File Offset: 0x000002D4
//        public void Start()
//        {
//            this.World = World.DefaultGameObjectInjectionWorld;


//            GameManager.instance.onGameLoadingComplete += this.OnGameLoadingComplete;
            
//            Debug.LogWarning("******START*****");

//            MyPluginManager pm = new MyPluginManager();
//            pm.PluginLoaded += (PluginInfo info) =>
//            {
               
//                var type = info.assembly.GetType("IMod");
//                var runnable = Activator.CreateInstance(type) as Game.Modding.IMod;
//                if (runnable == null) throw new Exception("broke");
//                runnable.OnLoad();
//                Debug.LogWarning(("*****LOADED PLUGIN {0}*****", info.assemblyPath.ToString()));
//                Mods.Add(runnable);

//            };
//            pm.OnDispose += () =>
//            {
//                foreach (var mod in Mods)
//                {
//                    mod.OnDispose();
//                }
//            };
//            //ImporterPluginManager importerPluginManager = new ImporterPluginManager(Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Mods")).FullName);
           
//            //C:\Program Files(x86)\Steam\steamapps\common\Cities Skylines II\Cities2_Data\Plugins\x86_64
//            try
//            {
//                pm.StartWatching();
                

                
//            }
//            catch (Exception e)
//            {
//                Debug.LogError(e.Message);
//            }
//        }
        
//        private void OnGameLoadingComplete(Colossal.Serialization.Entities.Purpose purpose, GameMode mode)
//        {
//            if (this.World != World.All[0])
//            {
//                this.World = World.All[0];

//            }
           
//            switch (mode)
//            {
//                case GameMode.MainMenu:
//                    Debug.LogWarning("******OnGameLoadingComplete MainMenu*****");
                    
//                    Mods.ForEach(x => x.OnCreateWorld(World.GetOrCreateSystemManaged<UpdateSystem>()));
//                    break;
//                case GameMode.Game:
//                    Debug.LogWarning("******OnGameLoadingComplete IN GAME*****");
                    
//                    break;
//            }
//            switch (purpose)
//            {
//                case Colossal.Serialization.Entities.Purpose.LoadGame:
//                    Debug.LogWarning("******OnGameLoadingComplete Load Game*****");
//                    break;
//                case Colossal.Serialization.Entities.Purpose.NewMap:
//                    Debug.LogWarning("******OnGameLoadingComplete NewMap*****");
//                    break;
//                case Colossal.Serialization.Entities.Purpose.LoadMap:
//                    Debug.LogWarning("******OnGameLoadingComplete LoadMap*****");
//                    break;
//            }
//        }

//        public World World { get; private set; }
//        public UpdateSystem UpdateSystem { get; private set; }
//    }
//}
