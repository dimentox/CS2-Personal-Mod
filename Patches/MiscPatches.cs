using System.Collections.Generic;
using Colossal.PSI.Common;
using Game.Dlc;
using Game.Prefabs;
using Game.Simulation;
using Game.UI.Menu;
using HarmonyLib;
using UnityEngine;

namespace ExampleMod.Patches
{
    /// <summary>
    /// Enable editor options (These may not be fully functional yet)
    /// </summary>
    [HarmonyPatch( typeof( MenuUISystem ), "IsEditorEnabled" )]
    class MenuUISystems_IsEditorEnabledPatch
    {
        static bool Prefix( ref bool __result )
        {
            __result = true;

            return false; // Ignore original function
        }
    }

    [HarmonyPatch(typeof(XPBuiltSystem), "OnUpdate")]
    public class XPBuiltSystem_patch
    {
        public static bool Prefix(XPBuiltSystem __instance)
        {

          //  Debug.LogWarning("XPBuiltSystem_patch");
            return false;
        }
    }


}
