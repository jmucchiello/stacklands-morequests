using System;
using CommonModNS;
using HarmonyLib;

namespace MoreQuestsModNS
{
    [HarmonyPatch]
    public class MoreQuests
    {
        [HarmonyPatch(typeof(QuestManager), nameof(QuestManager.ActionComplete))]
        [HarmonyPrefix]
        private static void MoreQuests_OnActionComplete(QuestManager __instance, CardData card, string action)
        {
            MoreQuestsMod.Counts.OnActionComplete(card, action);
        }

        [HarmonyPatch(typeof(QuestManager), nameof(QuestManager.SpecialActionComplete))]
        [HarmonyPrefix]
        private static void MoreQuests_OnSpecialActionComplete(QuestManager __instance, string action)
        {
            MoreQuestsMod.Counts.OnSpecialAction(action);
        }

        [HarmonyPatch(typeof(QuestManager),nameof(QuestManager.GetAllQuests))]
        [HarmonyPostfix]
        private static void MoreQuests_GetAllQuests(QuestManager __instance, List<Quest> __result)
        {
            Quest[] quests = [
                new Quest("have_100_wood")
                {
                    OnCardCreate = (CardData card) => card.Id == Cards.wood && WorldManager.instance.GetCardCountWithChest(Cards.wood) >= 100,
                    QuestGroup = QuestGroup.Resources,
                    DescriptionTermOverride = "quest_have_wood_text",
                    RequiredCount = 100,
                },
                new Quest("have_100_stone") {
                    OnCardCreate = (CardData card) => card.Id == Cards.stone && WorldManager.instance.GetCardCountWithChest(Cards.stone) >= 100,
                    QuestGroup = QuestGroup.Resources,
                    DescriptionTermOverride = "quest_have_stone_text",
                    RequiredCount = 100,
                },
                new Quest("reach_month_50")
                {
                    OnSpecialAction = (string action) => action == "month_end" && WorldManager.instance.CurrentMonth >= 50,
                    QuestGroup = QuestGroup.Survival,
                    DescriptionTermOverride = "quest_reach_month_text",
                    RequiredCount = 50
                },
                new Quest("reach_month_100")
                {
                    OnSpecialAction = (string action) => action == "month_end" && WorldManager.instance.CurrentMonth >= 100,
                    QuestGroup = QuestGroup.Survival,
                    DescriptionTermOverride = "quest_reach_month_text",
                    RequiredCount = 100
                },
                new Quest("reach_month_250")
                {
                    OnSpecialAction = (string action) => action == "month_end" && WorldManager.instance.CurrentMonth >= 250,
                    QuestGroup = QuestGroup.Survival,
                    DescriptionTermOverride = "quest_reach_month_text",
                    RequiredCount = 250
                },
                new Quest("have_100_food")
                {
                    OnCardCreate = (CardData card) => WorldManager.instance.GetFoodCount(allowDebug: false) >= 100,
                    QuestGroup = QuestGroup.Resources,
                    DescriptionTermOverride = "quest_have_food_text",
                    RequiredCount = 100
                },
                new Quest("have_500_food") {
                    OnCardCreate = (CardData card) => WorldManager.instance.GetFoodCount(allowDebug: false) >= 500,
                    QuestGroup = QuestGroup.Resources,
                    DescriptionTermOverride = "quest_have_food_text",
                    RequiredCount = 500
                },
                new Quest("have_5_mushrooms")
                {
                    OnCardCreate = (CardData card) => card.Id == Cards.mushroom && I.WM.AllCards.Count(x => x.MyBoard.IsCurrent && x.CardData.Id == Cards.mushroom) >= 5,
                    DefaultVisible = true,
                    QuestGroup = QuestGroup.Resources,
                    DescriptionTermOverride = "quest_have_mushrooms_text",
                    RequiredCount = 5
                },
                new Quest("have_50_mushrooms")
                {
                    OnCardCreate = (CardData card) => card.Id == Cards.mushroom && I.WM.AllCards.Count(x => x.MyBoard.IsCurrent && x.CardData.Id == Cards.mushroom) >= 50,
                    QuestGroup = QuestGroup.Resources,
                    DescriptionTermOverride = "quest_have_mushrooms_text",
                    RequiredCount = 50
                },
                new Quest("perform_10_subprints")
                {
                    OnActionComplete = (CardData card, string action) => action == "finish_blueprint" && MoreQuestsMod.Counts.blueprint_complete >= 10,
                    QuestGroup = QuestGroup.Other,
                    DescriptionTermOverride = "quest_blueprints_completed_text",
                    RequiredCount = 10
                },
                new Quest("perform_100_subprints")
                {
                    OnActionComplete = (CardData card, string action) => action == "finish_blueprint" && MoreQuestsMod.Counts.blueprint_complete >= 100,
                    QuestGroup = QuestGroup.Other,
                    DescriptionTermOverride = "quest_blueprints_completed_text",
                    RequiredCount = 100
                },
                new Quest("perform_1000_subprints")
                {
                    OnActionComplete = (CardData card, string action) => action == "finish_blueprint" && MoreQuestsMod.Counts.blueprint_complete >= 1000,
                    QuestGroup = QuestGroup.Other,
                    DescriptionTermOverride = "quest_blueprints_completed_text",
                    RequiredCount = 1000
                },
                new Quest("villager_2_statuseffects")
                {
                    OnSpecialAction = (string action) => action.StartsWith("add_status_") && I.WM.AllCards.Any(x => x.CardData is BaseVillager && x.MyBoard.IsCurrent && x.CardData.StatusEffects.Count >= 2),
                    QuestGroup = QuestGroup.Other,
                    DescriptionTermOverride = "quest_villager_statuseffects_text",
                    RequiredCount = 2
                }
            ];

            __result.AddRange(quests);
        }
    }
}

/*
 * Reach moon 100
Reach moon 500
Have 100 food
Have 500 food
Have 50 Mushrooms
Have 20 Gold Ore
Have 30 Flint
Have 20 Coin Chests
Have 20 Full Shell Chests
Have 20 Rabbit Hats
Have 5 Villagers born the same month
Name 100 villagers, uniquely
Eat 10,000 food

Have 50 Unopened Booster Packs
beat the demon lord with both curses on the board
have one of each fish in one aquarium

 */