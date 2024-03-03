using System.Collections.Generic;
using HostInitActions.Stages;

namespace HostInitActions
{
    internal class InitContext
    {
        public List<StageActionCollection> StageCollections = new List<StageActionCollection>();
    }
}
