﻿using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Contract : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual Company Contragent { get; set; }
        public virtual List<Specification> Specifications { get; set; }
    }
}