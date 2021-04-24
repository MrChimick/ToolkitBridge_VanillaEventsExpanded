using System.Collections.Generic;
using RimWorld;
using TwitchToolkit.Store;
using VEE.RegularEvents;
using Verse;

namespace ToolkitBridge_VanillaEventsExpanded
{
    public class IncidentBridge_HuntingParty : IncidentHelper
    {
        private IncidentParms parms;
        private IncidentWorker worker;
        
        public override bool IsPossible()
        {
            worker = new HuntingParty();
            worker.def = IncidentDef.Named("VEE_HuntingParty");
            parms = new IncidentParms();
            parms.target = Current.Game.RandomPlayerHomeMap;
            return (worker.CanFireNow(parms));
        }

        public override void TryExecute() => worker.TryExecute(parms);
    }
}