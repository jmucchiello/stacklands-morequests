using System;
using System.Collections.Generic;
using CommonModNS;
using Newtonsoft.Json;

namespace MoreQuestsModNS
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Counts
    {
        public Counts() { }
        public static Counts FromString(string json)
        {
            return JsonConvert.DeserializeObject<Counts>(json);
        }

        [JsonProperty("Blueprints")]
        public int blueprint_complete { get; set; }

        [JsonProperty]
        public LunarCounts lunarCount = new LunarCounts();

        public void OnActionComplete(CardData card, string action)
        {
            I.Log($"OnActionComplete {card.Id} {action}");
            if (action == "finish_blueprint")
            {
                ++blueprint_complete;
                I.Log($"Blueprints Completed {blueprint_complete}");
            }
        }

        public void OnSpecialAction(string action)
        {
            if (action != "pause_game") I.Log($"OnSpecialAction {action}");
            if (action == "month_end")
            {
                lunarCount.EndOfMonthEnd();
                lunarCount = new();
            }                
        }

        public void OnCardCreate(CardData card)
        { }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
