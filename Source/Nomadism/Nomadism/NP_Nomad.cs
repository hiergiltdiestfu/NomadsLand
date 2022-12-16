using RimWorld;
using System.Reflection.Emit;
using UnityEngine;
using Verse;

namespace NP_Nomadism
{
    // Token: 0x02000B94 RID: 2964
    public class ThoughtWorker_NomadSettledTooLong : ThoughtWorker
    {        
        public override string PostProcessLabel(Pawn p, string label)
        {
            float settlementAge = this.AgeOfSettlementInDays(p);
            if (settlementAge >= SettledTooLongLevelMaxInDays)
            {
                return base.PostProcessLabel(p, label) + " (desperately)";
            }

            if (settlementAge >= SettledTooLongLevel2InDays)
            {
                return base.PostProcessLabel(p, label) + " (uncomfortably)";
            }

            if (settlementAge >= SettledTooLongLevel1InDays)
            {
                return base.PostProcessLabel(p, label);
            }

            if (settlementAge < 0)
            {
                return "Happy on the road";
            }

            return "Happy on the road (afterglow)";
        }

        public override string PostProcessDescription(Pawn p, string description)
        {
            float settlementAge = this.AgeOfSettlementInDays(p);

            if (settlementAge < 0)
            {
                return "So glad to be on the road again. Hope we get to see more of the world, and don't stay put for so long next time we need to rest.";
            }

            if (settlementAge < SettledTooLongLevel1InDays)
            {
                return "I still feel kind of elated from my time on the road.";
            }

            return base.PostProcessDescription(p, description);
        }


        public override float MoodMultiplier(Pawn p)
        {
            float settlementAge = this.AgeOfSettlementInDays(p);
            
            if (settlementAge >= SettledTooLongLevelMaxInDays)
            {
                return settlementAge * LevelMaxMoodFactor;
            }

            if (settlementAge >= SettledTooLongLevel2InDays)
            {
                return settlementAge * Level2MoodFactor;
            }

            if (settlementAge >= SettledTooLongLevel1InDays)
            {
                return settlementAge * Level1MoodFactor;
            }

            if (settlementAge < 0)
            {
                return RoadMoodValue;
            }

            if (settlementAge < SettledTooLongLevel1InDays)
            {
                return RoadMoodValue / Mathf.Max(settlementAge, 1);
            }

            return 0;
        }
        
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            return 0 != this.MoodMultiplier(p);
        }

   
        private float AgeOfSettlementInDays(Pawn p)
        {
            float daysElapsed;
            
            
            Map map = p.Map;

            if (map == null || !map.IsPlayerHome) return -1;

            daysElapsed = map.AgeInDays;

            return daysElapsed; 
        }

        //settlement age thresholds
        private const int SettledTooLongLevel1InDays = 7;
        private const int SettledTooLongLevel2InDays = 20;
        private const int SettledTooLongLevelMaxInDays = 60;

        //mood effect values
        private const float RoadMoodValue = 10;       // [+10]
                                                      // [+10, +1.4[                            
        private const float Level1MoodFactor = -0.5f; // [-3.5, -10[
        private const float Level2MoodFactor = -0.75f; // [-15, -45[
        private const float LevelMaxMoodFactor = -1f; // [-60, -inf]                                       
        
    }

    public class ThoughtWorker_SettlerIsSettled : ThoughtWorker
    {
        public override string PostProcessLabel(Pawn p, string label)
        {
            float settlementAge = this.AgeOfSettlementInDays(p);
            if (settlementAge >= SettledLongEnoughLevelMaxInDays)
            {
                return base.PostProcessLabel(p, label) + " (fully at peace)";
            }

            if (settlementAge >= SettledLongEnoughLevel2InDays)
            {
                return base.PostProcessLabel(p, label) + " (comfortable)";
            }

            if (settlementAge >= SettledLongEnoughLevel1InDays)
            {
                return base.PostProcessLabel(p, label);
            }

            if (settlementAge < 0)
            {
                return "Uprooted";
            }
            
            return "Uprooted (aftermath)";
        }

        public override string PostProcessDescription(Pawn p, string description)
        {
            float settlementAge = this.AgeOfSettlementInDays(p);

            if (settlementAge < 0)
            {
                return "I feel lost and uprooted. Hope we find a spot to rest and settle down soon.";
            }

            if (settlementAge < SettledLongEnoughLevel1InDays)
            {
                return "I still feel lost from my time on the road. Hope we can stay here for now.";
            }

            return base.PostProcessDescription(p, description);
        }


        public override float MoodMultiplier(Pawn p)
        {
            float settlementAge = this.AgeOfSettlementInDays(p);

            if (settlementAge >= SettledLongEnoughLevelMaxInDays)
            {
                return LevelMaxMoodValue;
            }

            if (settlementAge >= SettledLongEnoughLevel2InDays)
            {
                return settlementAge * Level2MoodFactor;
            }

            if (settlementAge >= SettledLongEnoughLevel1InDays)
            {
                return settlementAge * Level1MoodFactor;
            }

            if (settlementAge < 0)
            {
                return RoadMoodValue;
            }

            if (settlementAge < SettledLongEnoughLevel1InDays)
            {
                return RoadMoodValue / Mathf.Max(settlementAge, 1);
            }

            return 0;
        }


        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            return this.MoodMultiplier(p) != 0;
        }


        private float AgeOfSettlementInDays(Pawn p)
        {
            float daysElapsed;

            Map map = p.Map;

            if (map == null || !map.IsPlayerHome) return -1;

            daysElapsed = map.AgeInDays;

            return daysElapsed;
        }

        //settlement age thresholds
        private const int SettledLongEnoughLevel1InDays = 7;
        private const int SettledLongEnoughLevel2InDays = 20;
        private const int SettledLongEnoughLevelMaxInDays = 40;

        //mood values
        private const float RoadMoodValue = -10;    // [-10]
                                                    // [-10, -1.4[
        private const float Level1MoodFactor = 0.1f; // [+0.7, +2[
        private const float Level2MoodFactor = 0.25f; // [+5, +10[
        private const float LevelMaxMoodValue = 10f; // [+10]

    }

}
