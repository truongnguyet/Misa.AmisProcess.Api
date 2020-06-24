using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class Phase
    {
        public Phase()
        {
            FieldData = new HashSet<FieldData>();
        }

        public int Id { get; set; }
        public string PhaseName { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public byte IsFirstPhase { get; set; }
        public byte IsTc { get; set; }
        public byte IsTb { get; set; }
        public byte LimitUser { get; set; }
        public int ProcessId { get; set; }

        public virtual Process Process { get; set; }
        public virtual ICollection<FieldData> FieldData { get; set; }
    }
}
