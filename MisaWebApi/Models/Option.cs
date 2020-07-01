using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class Option
    {
        public string Id { get; set; }
        public string Value { get; set; }
        public string FieldDataId { get; set; }

        public virtual FieldData FieldData { get; set; }
    }
}
