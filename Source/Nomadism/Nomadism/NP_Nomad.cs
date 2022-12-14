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
            float num = this.AgeOfSettlementInDays(p);
            if (num >= SettledTooLongLevelMaxInDays)
            {
                return base.PostProcessLabel(p, label) + " (desperately)";
            }

            if (num >= SettledTooLongLevel2InDays)
            {
                return base.PostProcessLabel(p, label) + " (uncomfortably)";
            }

            if (num >= SettledTooLongLevel1InDays)
            {
                return base.PostProcessLabel(p, label) + " (getting started)";
            }

            if (num < 0)
            {
                return base.PostProcessLabel(p, label) + " (fulfilled)";
            }           

            return base.PostProcessLabel(p, label);
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
                return RoadMoodFactor;
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

        private const int SettledTooLongLevel1InDays = 1;//7;
        private const int SettledTooLongLevel2InDays = 2;//14;
        private const int SettledTooLongLevelMaxInDays = 3;//30;

        private const float Level1MoodFactor = -0.1f;
        private const float Level2MoodFactor = -0.5f;
        private const float LevelMaxMoodFactor = -1.5f;

        private const float RoadMoodFactor = 10;

    }

    public class ThoughtWorker_SettlerIsSettled : ThoughtWorker
    {
        public override string PostProcessLabel(Pawn p, string label)
        {
            float num = this.AgeOfSettlementInDays(p);
            if (num >= SettledLongEnoughLevelMaxInDays)
            {
                return base.PostProcessLabel(p, label) + " (fully at peace)";
            }

            if (num >= SettledLongEnoughLevel2InDays)
            {
                return base.PostProcessLabel(p, label) + " (comfortable)";
            }

            if (num >= SettledLongEnoughLevel1InDays)
            {
                return base.PostProcessLabel(p, label) + " (onset)";
            }

            if (num < 0)
            {
                return base.PostProcessLabel(p, label) + " (uprooted)";
            }

            return base.PostProcessLabel(p, label);
        }


        public override float MoodMultiplier(Pawn p)
        {
            float settlementAge = this.AgeOfSettlementInDays(p);

            if (settlementAge >= SettledLongEnoughLevelMaxInDays)
            {
                return LevelMaxMoodFactor;
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
                return RoadMoodFactor;
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

        private const int SettledLongEnoughLevel1InDays = 1;//7;
        private const int SettledLongEnoughLevel2InDays = 2;//14;
        private const int SettledLongEnoughLevelMaxInDays = 3;//30;

        private const float Level1MoodFactor = 0.1f;
        private const float Level2MoodFactor = 0.6f;
        private const float LevelMaxMoodFactor = 10f;

        private const float RoadMoodFactor = -10;

    }

}
