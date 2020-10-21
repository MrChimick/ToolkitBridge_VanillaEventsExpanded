using RimWorld;
using System.Collections.Generic;
using Verse;
using VEE;
using TwitchToolkit.Store;

namespace ToolkitBridge_VanillaEventsExpanded
{
    public class IncidentBridge_HailStorm : IncidentHelper
    {
        private IncidentParms parms;
        private IncidentWorker worker;
        

        public override bool IsPossible()
        {
            this.worker = (IncidentWorker) new IncidentWorker_VEE();
            this.worker.def = IncidentDef.Named("TB_VEE_HailStorm");
            this.parms = new IncidentParms();
            List<Map> maps = Current.Game.Maps;
            ((IList<Map>) maps).Shuffle<Map>();
            using (List<Map>.Enumerator enumerator = maps.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    this.parms.target = enumerator.Current;
                    if (this.worker.CanFireNow(this.parms, false)) return true;
                }
            }
            return false;
        }

        public override void TryExecute() => this.worker.TryExecute(this.parms);
    }
}