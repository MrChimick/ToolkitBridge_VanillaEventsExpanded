using System.Collections.Generic;
using RimWorld;
using TwitchToolkit.Store;
using VEE;
using Verse;

namespace ToolkitBridge_VanillaEventsExpanded
{
    public class IncidentBridge_HailStorm : IncidentHelper
    {
        private IncidentParms parms;
        private IncidentWorker worker;
        
        public override bool IsPossible()
        {
            worker = new IncidentWorker_MakeGameConditionVEE();
            worker.def = IncidentDef.Named("VEE_HailStorm");
            parms = new IncidentParms();
            parms.target = Current.Game.RandomPlayerHomeMap;
            return (worker.CanFireNow(parms));
        }

        public override void TryExecute() => worker.TryExecute(parms);
    }
}