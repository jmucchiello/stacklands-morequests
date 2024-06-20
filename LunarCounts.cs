using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using HarmonyLib;

namespace MoreQuestsModNS
{

    [JsonObject(MemberSerialization.OptIn)]
    public class LunarCounts
    {
        [JsonProperty]
        public int foodMade;
        [JsonProperty]
        public int coinsGained;
        [JsonProperty]
        public int boostersOpened;
        [JsonProperty]
        public int boostersBought;

        public void EndOfMonthBegin() { }
        public void EndOfMonthEnd() { }

        public void OnActionComplete(CardData card, string action) { }
        public void OnSpecialAction(string action) { }
    }
}
