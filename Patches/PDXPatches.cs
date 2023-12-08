using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering;
using PDX.SDK.Internal.Service.Mods;
using PDX.SDK.Contracts;


namespace TargetMethodsDemo.Patches
{
   

    [HarmonyPatch(typeof(PDX.SDK.Internal.Service.Mods.PlaysetAndModsSyncService), "Sync")]
    public class PlaysetAndModsSyncService_Sync
    {
        static bool Prefix(ref Task<Result> __result)
        {
            __result = Task.FromResult(PDX.SDK.Contracts.Result.ResultSuccessful());

            return false; // Ignore original function
        }
    }
}
