using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class Process
    {
        public Process()
        {
            Phase = new HashSet<Phase>();
            UsersHasProcess = new HashSet<UsersHasProcess>();
        }

        public int Id { get; set; }
        public string NameProcess { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<Phase> Phase { get; set; }
        public virtual ICollection<UsersHasProcess> UsersHasProcess { get; set; }
    }
}
