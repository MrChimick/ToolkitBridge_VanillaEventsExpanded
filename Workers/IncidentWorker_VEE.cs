using RimWorld;
using System;
using System.Collections.Generic;
using VEE;
using Verse;

namespace ToolkitBridge_VanillaEventsExpanded
{
    public class IncidentWorker_VEE : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            GameConditionManager conditionManager = parms.target.GameConditionManager;
            if (conditionManager == null)
            {
                Log.ErrorOnce(string.Format("Couldn't find condition manager for incident target {0}", (object) parms.target), 70849667);
                return false;
            }
            if (conditionManager.ConditionIsActive(this.def.gameCondition))
                return false;
            List<GameCondition> activeConditions = conditionManager.ActiveConditions;
            for (int index = 0; index < activeConditions.Count; ++index)
            {
                if (activeConditions[index].def == VEE_DefOf.IceAge || activeConditions[index].def == VEE_DefOf.GlobalWarming || !this.def.gameCondition.CanCoexistWith(activeConditions[index].def))
                    return false;
            }
            return true;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            GameConditionManager conditionManager = parms.target.GameConditionManager;
            GameCondition cond = GameConditionMaker.MakeCondition(this.def.gameCondition, Convert.ToInt32(this.def.durationDays.RandomInRange * 60000f));
            conditionManager.RegisterCondition(cond);
            parms.letterHyperlinkThingDefs = cond.def.letterHyperlinks;
            this.SendStandardLetter((TaggedString) this.def.letterLabel, (TaggedString) this.def.letterText, this.def.letterDef, parms, LookTargets.Invalid);
            return true;
        }
    }
}