using Game.SceneFlow;
using HarmonyLib;


namespace ExampleMod.Patches
{

    //System.Threading.Tasks.Task`1<PDX.SDK.Contracts.Result> PDX.SDK.Internal.Service.Mods.PlaysetAndModsSyncService::Sync(PDX.SDK.Contracts.Service.Mods.Enums.SyncDirection, PDX.SDK.Contracts.Internal.FlowData)



  

    [HarmonyPatch(typeof(Game.UI.Menu.MenuUISystem), "IsPdxModsUIEnabled")]
    class MenuUISystem_IsPdxModsUIEnabledPatch
    {
        static bool Prefix(ref bool __result)
        {
            __result = true;

            return false; // Ignore original function
        }
    }

    [HarmonyPatch(typeof(Game.UI.Menu.MenuUISystem), "IsModdingEnabled")]
    class MenuUISystem_IsModdingEnabledPatch
    {
        static bool Prefix(ref bool __result)
        {
            __result = true;

            return false; // Ignore original function
        }
    }


    /// <summary>
    /// Force enable developer mode
    /// </summary>
    /// <remarks>
    /// (This can bypass achievement so the unlock tool gives you achievements I think?)
    /// </remarks>
    [HarmonyPatch( typeof( GameManager ), "ParseOptions" )]
    class GameManager_ParseOptionsPatch
    {
        static void Postfix( GameManager __instance )
        {
            var configuration = __instance.configuration;

            if ( configuration != null )
            {
               __instance.
                configuration.uiDeveloperMode = true;
                configuration.developerMode = true;
                configuration.qaDeveloperMode = true;
                configuration.captureStdout = GameManager.Configuration.StdoutCaptureMode.Console;
                UnityEngine.Debug.Log( "Turned on Developer Mode! Press TAB for the dev/debug menu." );
            }
        }
    }
}
