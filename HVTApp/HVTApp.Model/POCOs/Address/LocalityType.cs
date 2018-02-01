using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ��� ����������� ������.
    /// </summary>
    public partial class LocalityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, ({ShortName})";
        }
    }
}