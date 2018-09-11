using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Note : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool IsImportant { get; set; } = false;
    }
}
