using DamagePopups.Behaviours;
using System.Linq;
using TMPro;
using UnityEngine;

namespace DamagePopups.Patches
{
    public class PlayerHook
    {
        internal static void Init()
        {
            On.Player.TakeDamage += MMHook_Postfix_ShowDamagePopup;
            On.Player.RPCA_Heal += MMHook_Postfix_ShowHealPopup;
            On.Player.RPCA_PlayerDie += MMHook_Postfix_ShowInstakillPopup;
            On.Player.CallRevive += MMHook_Postfix_ShowRevivePopup;
        }

        private static void MMHook_Postfix_ShowHealPopup(On.Player.orig_RPCA_Heal orig, Player self, float healAmount)
        {
            orig(self, healAmount);
            InstantiatePopupAtPlayer(self, Color.green, $"+{Mathf.RoundToInt(healAmount)}");
        }

        private static void MMHook_Postfix_ShowDamagePopup(On.Player.orig_TakeDamage orig, Player self, float damage)
        {
            orig(self, damage);
            if (!self.ai && !self.data.dead && !(self.Center().y < -100f || self.Center().y > 100f))
            {
                InstantiatePopupAtPlayer(self, Color.red, $"{(self.data.recentDamage.Sum(x => x.damage) >= Player.PlayerData.maxHealth ? "INSTAKILL!" : $"-{Mathf.RoundToInt(damage)}")}");
            }
        }

        private static void MMHook_Postfix_ShowInstakillPopup(On.Player.orig_RPCA_PlayerDie orig, Player self)
        {
            orig(self);
            if (!self.data.recentDamage.Any() && self.data.remainingOxygen >= 0f && !(self.Center().y < -100f || self.Center().y > 100f))
            {
                InstantiatePopupAtPlayer(self, (Color.red + Color.black) / 2, "INSTAKILL!");
            }
        }

        private static void MMHook_Postfix_ShowRevivePopup(On.Player.orig_CallRevive orig, Player self)
        {
            orig(self);
            InstantiatePopupAtPlayer(self, Color.blue, "REVIVE!");
        }

        private static void InstantiatePopupAtPlayer(Player player, Color color, string text)
        {
            Vector3 _popupPosition = player.HeadPosition() + (Vector3.up * 0.5f);
            GameObject _damagePopup = new("DMG Popup");
            Transform _damagePopupTransform = _damagePopup.transform;
            _damagePopupTransform.position = _popupPosition;
            var _textMesh = _damagePopup.AddComponent<TextMeshPro>();
            _textMesh.alignment = TextAlignmentOptions.Center;
            _textMesh.color = color;
            _textMesh.text = text;
            _textMesh.fontSize = 5f;
            _textMesh.font = Plugin.PopupFont;
            _damagePopup.AddComponent<PopupFadeOuter>();
            Object.Destroy(GameObject.Find("New Game Object"));
        }
    }
}
