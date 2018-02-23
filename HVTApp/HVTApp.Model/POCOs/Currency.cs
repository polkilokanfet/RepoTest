using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Валюта
    /// </summary>
    public partial class Currency : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }
}