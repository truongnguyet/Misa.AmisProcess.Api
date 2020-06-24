using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class Option
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int FieldDataId { get; set; }

        public virtual FieldData FieldData { get; set; }
    }
}
