using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class FieldData
    {
        public FieldData()
        {
            Option = new HashSet<Option>();
        }

        public int Id { get; set; }
        public string FieldName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public byte Required { get; set; }
        public int PhaseId { get; set; }

        public virtual Phase Phase { get; set; }
        public virtual ICollection<Option> Option { get; set; }
    }
}
