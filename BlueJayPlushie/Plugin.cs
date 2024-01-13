using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using LethalLib.Modules;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace BlueJayPlushie
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class Plugin : BaseUnityPlugin
    {
        public static AssetBundle BlueJayAssets;
        public static ConfigFile config;

        private void Awake()
        {
            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            BlueJayAssets = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "bluejaymod"));
            if (BlueJayAssets == null)
            {
                Logger.LogError("Failed to load custom assets.");
                return;
            }
            int iRarity = 100;
            Item blueJayPlushie = BlueJayAssets.LoadAsset<Item>("Assets/Bluejayplushie.asset");
            Utilities.FixMixerGroups(blueJayPlushie.spawnPrefab);
            NetworkPrefabs.RegisterNetworkPrefab(blueJayPlushie.spawnPrefab);
            Items.RegisterScrap(blueJayPlushie, iRarity, Levels.LevelTypes.All);
            new Harmony("BlueJayPlushie").PatchAll();
            Logger.LogMessage("Blue Jay Plushie Mod loaded!");

        }
    }
}