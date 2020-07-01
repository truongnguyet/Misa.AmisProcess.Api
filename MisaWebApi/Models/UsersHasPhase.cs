using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class UsersHasPhase
    {
        public string UsersId { get; set; }
        public string PhaseId { get; set; }

        public virtual Phase Phase { get; set; }
        public virtual Users Users { get; set; }
    }
}
