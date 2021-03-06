﻿using System;
using System.Collections.Generic;

namespace MisaWebApi.Models
{
    public partial class UsersHasProcess
    {
        public string UsersId { get; set; }
        public string ProcessId { get; set; }

        public virtual Process Process { get; set; }
        public virtual Users Users { get; set; }
    }
}
