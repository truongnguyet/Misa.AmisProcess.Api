
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class PhaseDelete
    {
        public PhaseDelete()
        {
            UsersHasPhase = new HashSet<UsersHasPhase>();
        }

        public string Id { get; set; }
        public byte LimitUser { get; set; }

        public virtual Process Process { get; set; }
        public virtual ICollection<UsersHasPhase> UsersHasPhase { get; set; }
        public virtual ICollection<UsersHasPhase> UserDelete { get; set; }
    }
}
