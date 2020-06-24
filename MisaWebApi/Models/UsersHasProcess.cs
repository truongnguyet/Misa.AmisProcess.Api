using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class UsersHasProcess
    {
        public int UsersId { get; set; }
        public int ProcessId { get; set; }

        public virtual Process Process { get; set; }
        public virtual Users Users { get; set; }
    }
}
