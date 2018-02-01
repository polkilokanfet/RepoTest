using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Единица измерения
    /// </summary>
    public partial class Measure : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, {ShortName}";
        }

    }
}