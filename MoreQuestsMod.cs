using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;
using CommonModNS;

namespace MoreQuestsModNS
{
    public class MoreQuestsMod : Mod
    {
        public static MoreQuestsMod instance;
        public static void Log(string msg) => instance.Logger.Log(msg);
        public static void LogError(string msg) => instance.Logger.LogError(msg);

        public static Counts Counts = new Counts();

        private void Awake()
        {
            instance = this;
            WorldManagerPatches.LoadSaveRound += WM_OnLoad;
            WorldManagerPatches.GetSaveRound += WM_OnSave;
            WorldManagerPatches.StartNewRound += WM_OnNewGame;
            //WorldManagerPatches.Play += WM_OnPlay;
            //WorldManagerPatches.Update += WM_OnUpdate;
            WorldManagerPatches.ApplyPatches(Harmony);
            Harmony.PatchAll(typeof(MoreQuests));
            Harmony.PatchAll(typeof(Patches));
        }

        public void WM_OnNewGame(WorldManager _)
        {
            Counts = new Counts();
        }

        public void WM_OnSave(WorldManager _, SaveRound round)
        {
            try
            {
                round.ExtraKeyValues.SetOrAdd("more_quests_mod", Counts.ToString());
            }
            catch (Exception e)
            {
                I.GS.AddNotification("More Quests Mod - Save Error", e.Message);
            }
        }

        public void WM_OnLoad(WorldManager _, SaveRound round)
        {
            var data = round.ExtraKeyValues.GetWithKey("more_quests_mod");
            if (data != null && !String.IsNullOrEmpty(data.Value))
            {
                try
                {
                    Counts = Counts.FromString(data.Value);
                }
                catch (Exception e)
                {
                    I.GS.AddNotification("More Quests Mod - Load Error", e.Message);
                    Counts = new Counts();
                }
            }
            else
            {
                I.GS.AddNotification("More Quests Mod", "No mod data in save file");
                Counts = new Counts();
            }
        }

        public override void Ready()
        {
            Log("Ready!");
        }
    }
}