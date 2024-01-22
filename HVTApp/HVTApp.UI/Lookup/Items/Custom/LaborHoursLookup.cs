using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class LaborHoursLookup
    {
        private List<ProductBlock> _blocks;

        /// <summary>
        /// Ѕлоки, которые перекрываютс€ этими нормо-часами
        /// </summary>
        public List<ProductBlock> Blocks
        {
            get => _blocks;
            set
            {
                _blocks = value;
                RaisePropertyChanged(nameof(this.BlocksString));
            }
        }

        public string BlocksString => 
            Blocks?.Select(block => block.Designation).Distinct().OrderBy(x => x).ToStringEnum();
    }
}