using CommonModNS;
using HarmonyLib;
using UnityEngine;

namespace MoreQuestsModNS
{

    public class Patches
    {
        [HarmonyPatch(typeof(WorldManager), "EndOfMonth")]
        [HarmonyPrefix]
        private static void WorldManager_EndOfMonth(WorldManager __instance)
        {
            MoreQuestsMod.Counts.lunarCount.EndOfMonthBegin();
        }

        [HarmonyPatch(typeof(WorldManager), "CreateCard", [typeof(Vector3),typeof(CardData), typeof(bool), typeof(bool), typeof(bool), typeof(bool)])]
        [HarmonyPostfix]
        private static void WorldManager_CreateCard(WorldManager __instance, CardData __result)
        {
            I.Log($"Card Created {__result.Id}");
        }
    }

}
