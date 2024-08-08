global using Plugin = DamagePopups.DamagePopups;
using BepInEx;
using BepInEx.Logging;
using DamagePopups.Patches;
using TMPro;
using UnityEngine;

namespace DamagePopups
{
    [ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_VERSION, true), BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class DamagePopups : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; } = null!;

        internal new static ManualLogSource Logger { get; private set; } = null!;

        internal static TMP_FontAsset PopupFont { get; private set; } = null!;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            HookAll();

            On.MainMenuHandler.Start += (orig, self) =>
            {
                orig(self);
                PopupFont = GameObject.Find("Canvas/MainPage/HIGH SERVER LOAD").GetComponent<TextMeshProUGUI>().font;
            };

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} v{MyPluginInfo.PLUGIN_VERSION} by {MyPluginInfo.PLUGIN_GUID.Split(".")[0]} has loaded!");
        }

        internal static void HookAll()
        {
            Logger.LogDebug("Hooking...");

            PlayerHook.Init();

            Logger.LogDebug("Finished hooking!");
        }
    }
}
