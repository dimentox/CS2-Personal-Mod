using cohtml.Net;
using Colossal.UI;
using ExampleMod.UI;
using Game.UI;
using Game.UI.Editor;
using Game.UI.InGame;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Colossal.IO.AssetDatabase;
using Colossal.Json;
using Game.Buildings;
using Game.Debug;
using Game.Prefabs;
using Game.Simulation;
using Game.UI.Localization;
using Unity.Entities;
using UnityEngine;
using Game.City;
using Unity.Mathematics;
using System.Runtime.ConstrainedExecution;
using UnityEngine.Experimental.Rendering;
using Unity.Collections;
using static Game.Prefabs.CharacterGroup;
using Game.Zones;

namespace ExampleMod.Patches
{
    //TerrainPanelSystem
    //private static IEnumerable<AssetItem> GetHeightmaps()
    // {
    // foreach (ImageAsset asset in Colossal.IO.AssetDatabase.AssetDatabase.global.GetAssets<ImageAsset>(new SearchFilter<ImageAsset>()))
    // {
    // SourceMeta meta = asset.GetMeta();
    // AssetItem heightmap = new AssetItem();
    // heightmap.guid = asset.guid;
    // heightmap.fileName = meta.displayName + meta.mimeType;
    // heightmap.displayName = (LocalizedString) meta.displayName;
    // heightmap.image = (string) null;
    // yield return heightmap;
    // }
    // }
    ////DebugLevelUp
    //[HarmonyPatch(typeof(PropertyRenterSystem), "DebugLevelUp")]

    //class DebugSystem_DebugLevelUp
    //{
    //    static bool Prefix(DebugSystem __instance,
    //        ref Entity building,
    //        ref ComponentLookup<BuildingCondition> conditions,
    //        ref ComponentLookup<SpawnableBuildingData> spawnables,
    //        ref ComponentLookup<PrefabRef> prefabRefs,
    //        ref ComponentLookup<ZoneData> zoneDatas,
    //        ref ComponentLookup<BuildingPropertyData> propertyDatas
    //        )
    //    {
    //        try
    //        {

    //            if (conditions.HasComponent(building) && prefabRefs.HasComponent(building))
    //            {
    //                BuildingCondition buildingCondition = conditions[building];
    //                Entity prefab = prefabRefs[building].m_Prefab;
    //                if (spawnables.HasComponent(prefab) && propertyDatas.HasComponent(prefab))
    //                {
    //                    SpawnableBuildingData spawnableBuildingData = spawnables[prefab];
    //                    if (zoneDatas.HasComponent(spawnableBuildingData.m_ZonePrefab))
    //                    {
    //                        var city = (CitySystem)Traverse.Create(__instance).Field("m_CitySystem").GetValue();

    //                        Debug.Log(city.City);
    //                        var buffer = __instance.EntityManager.GetBuffer<CityModifier>(city.City, true);
    //                        Debug.Log(__instance.EntityManager.RemoveComponent<Abandoned>(building));
    //                        int levelingCost = BuildingUtils.GetLevelingCost(zoneDatas[spawnableBuildingData.m_ZonePrefab].m_AreaType, propertyDatas[prefab], (int)spawnableBuildingData.m_Level, buffer);
    //                                     Debug.Log(levelingCost);
    //                        buildingCondition.m_Condition = -3 * levelingCost / 2;


    //                        Debug.Log(buildingCondition);
    //                        conditions[building] = buildingCondition;

    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogException(e);
    //        }

    //        return true;
    //    }

    //}
    
    //[HarmonyPatch(typeof(TerrainSystem), "ReplaceHeightmap")]
    //class TerrainSystem_ReplaceHeightmap
    //{
    //    static bool Prefix(ref TerrainSystem __instance, Texture srcHeightmap)
    //    {
    //        try
    //        {
         
    //            foreach (ImageAsset imageAsset in AssetDatabase.global.GetAssets<ImageAsset>(default(SearchFilter<ImageAsset>)))
    //            {
    //                SourceMeta meta = imageAsset.GetMeta();
    //                if (meta.displayName == "worldmap")
    //                {
    //                    Debug.Log("Loading Height map for world: " + meta.fileName);
    //                    __instance.worldHeightmap = imageAsset.Load();
    //                }
    //            }

    //            //TextureAsset asset2 = AssetDatabase.global.GetAsset<TextureAsset>(new Colossal.Hash128("4281ab9d49876d65aab7cb0860c7e181"));
    //            //if (asset2 != __instance.worldMapAsset)
    //            //{
    //            //    //if (__instance.worldMapAsset != null)
    //            //    //{
    //            //    //    __instance.worldMapAsset.Unload(false);
    //            //    //}
    //            //    //__instance.worldMapAsset = asset2;
    //            //    //TextureAsset worldMapAsset = __instance.worldMapAsset;
    //            //    //__instance.worldHeightmap = worldMapAsset.Load(0, TextureAsset.KeepOnCPU.Dont);
    //            //    //Debug.Log("LOADED WORLD MAP");
    //            //    ////var worldHeightmap = new RenderTexture(__instance.worldHeightmap.width, __instance.worldHeightmap.height, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UNorm)
    //            //    ////{
    //            //    //    hideFlags = HideFlags.HideAndDontSave,
    //            //    //    enableRandomWrite = true,
    //            //    //    name = "TerrainWorldHeights",
    //            //    //    filterMode = FilterMode.Bilinear,
    //            //    //    wrapMode = TextureWrapMode.Clamp
    //            //    //};
    //            //    //worldHeightmap.Create();
    //            //   // Traverse.Create(__instance).Field("m_WorldMapEditable").SetValue(worldHeightmap);
    //            //}
               
    //            RenderTexture heightmap = Traverse.Create(__instance).Field("m_Heightmap").GetValue() as RenderTexture;


    //            Graphics.Blit(srcHeightmap, heightmap);
               
    //            // Graphics.CopyTexture(srcHeightmap, 0, 0, heightmap, 0, 0);
    //            Traverse.Create(__instance).Field("m_Heightmap").SetValue(heightmap);

    //            Traverse.Create(__instance).Field("m_CascadeReset").SetValue(true);
    //            Debug.Log(srcHeightmap.ToJSONString());
    //            //Graphics.CopyTexture(srcHeightmap, 0, 0, __instance.m_Heightmap, 0, 0);
    //            Shader.SetGlobalTexture("colossal_TerrainTexture", heightmap);
    //            // __instance.m_CascadeReset = true;
    //            // Texture2D worldMap = srcHeightmap as Texture2D; // This is the texture we want to use
    //            // __instance.worldHeightmap = worldMap;

    //            //RenderTexture worldHeightmap = Traverse.Create(__instance).Field("worldHeightmap").GetValue() as RenderTexture;
    //            //Graphics.Blit(srcHeightmap, worldHeightmap);
    //            //Traverse.Create(__instance).Field("m_WorldMapEditable").SetValue(worldHeightmap);
    //            ////  Traverse.Create(__instance).Field("m_HeightmapCascade").SetValue(worldMap);
    //            //Traverse.Create(__instance).Field("m_CascadeReset").SetValue(true);
           
                
    //         //   var worldHeightmap = new RenderTexture(__instance.worldHeightmap.width, __instance.worldHeightmap.height, 0, UnityEngine.Experimental.Rendering.GraphicsFormat.R16_UNorm)
    //         //   {
    //         //       hideFlags = HideFlags.HideAndDontSave,
    //         //       enableRandomWrite = true,
    //         //       name = "TerrainWorldHeights",
    //         //       filterMode = FilterMode.Bilinear,
    //         //       wrapMode = TextureWrapMode.Clamp
    //         //   };
    //         //   worldHeightmap.Create();
    //         //   Traverse.Create(__instance).Field("m_WorldMapEditable").SetValue(worldHeightmap);

    //         //   Graphics.Blit(srcHeightmap, heightmap);
    //         //   Traverse.Create(__instance).Property("worldSize").SetValue(new float2(57334f, 57334f));
    //         //   Traverse.Create(__instance).Property("playableOffset").SetValue(new float2(-7168f, -7168f));
    //         //   Traverse.Create(__instance).Property("worldOffset").SetValue(new float2(28672f, -28672f));
    //         //  Traverse.Create(__instance).Property("heightScaleOffset").SetValue(new float2(210f, 0f));
    //         //   Traverse.Create(__instance).Property("baseLod").SetValue(1);
              
              
               
    //         //   //float2 @float = new float2(14336f, 14336f);
    //         //   float2 @float = new float2(57334f, 57334f);
    //         //   float2 float2 = @float;
    //         //   float2 float3 = @float * -0.5f;
    //         //   float2 float4 = float2 * -0.5f;
    //         //   float2 zero = float2.zero;
    //         ////   Traverse.Create(__instance).Method("SetTerrainData", new Type[] {typeof(Texture2D), typeof(float2), typeof(float2), typeof(float2), typeof(float2), typeof(float2), typeof(float2) }).GetValue((Texture2D)null, new float2(208f, 0), float3, @float, float4, @float5, new float2(0f, 0f));

    //            return false;
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogException(e);

    //        }
    //        return false; // Run default function

    //    }
    //}


    //[HarmonyPatch(typeof(TerrainPanelSystem), "GetHeightmaps")]
    //class TerrainPanelSystem_OnGetHeightmaps
    //{
    //    static bool Prefix(TerrainPanelSystem __instance)
    //    {
    //        try
    //        {

               

    //            foreach (ImageAsset asset in Colossal.IO.AssetDatabase.AssetDatabase.global.GetAssets<ImageAsset>(
    //                         new SearchFilter<ImageAsset>()))
    //            {
    //                SourceMeta meta = asset.GetMeta();
    //                Debug.Log("Custom mod GetHeightmaps request: " + meta.fileName);
    //                Debug.Log("Custom mod GetHeightmaps request: " + meta.package);
    //                Debug.Log("Custom mod GetHeightmaps request: " + meta.packageName);
    //                Debug.Log("Custom mod GetHeightmaps request: " + meta.path);
    //                Debug.Log("Custom mod GetHeightmaps request: " + meta.uri);
    //                Debug.Log("Custom mod GetHeightmaps request: " + meta.ToJSONString());
                   

    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.LogException(e);
                
    //        }
    //        return true; // Run default function

    //    }
    //}

    [HarmonyPatch( typeof( GameUIResourceHandler ), "OnResourceRequest" )]
    class GameUIResourceHandler_OnResourceRequestPatch
    {
        // We keep this because we need to unload old textures if they update
        private static Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>( );

        static bool Prefix( GameUIResourceHandler __instance, IResourceRequest request, IResourceResponse response )
        {
            try
            {
                var url = request.GetURL( );

                if ( url?.ToLower().StartsWith( "examplemod://" ) == true )
                {
                    __instance.coroutineHost.StartCoroutine( LoadCustomResource( __instance, url, response ) );
                    Debug.Log( "Custom mod resource request: " + url );
                    return false; // Override default function it's one of our requests!
                }
            }
            catch ( Exception ex )
            {
                Debug.LogException( ex );
                response.Finish( ResourceResponse.Status.Failure );
            }
            return true; // Run default function
        }

        static System.Collections.IEnumerator LoadCustomResource( GameUIResourceHandler __instance, string url, IResourceResponse response )
        {
            var fileName = System.IO.Path.GetFileName( url );
            try
            {
                var texture = ImageHelper.Get( fileName );

                if ( !cache.ContainsKey( fileName ) )
                {
                    cache.Add( fileName, texture );
                }
                else // We need to tell it to update the existing reference
                {
                    var oldTexture = cache[fileName];
                    cache[fileName] = texture;

                    __instance.userImagesManager.UpdateNativePtr( oldTexture, texture );
                }

                response.ReceiveUserImage( __instance.userImagesManager.GetUserImageData( texture, UserImagesManager.ResourceType.Managed, false, new Rect( 0, 0, texture.width, texture.height ) ) );

                response.Finish( ResourceResponse.Status.Success );
            }
            catch
            {
                response.Finish( ResourceResponse.Status.Failure );
            }

            yield return null;
        }
    }
}
