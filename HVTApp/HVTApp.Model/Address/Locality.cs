namespace HVTApp.Model
{
    /// <summary>
    /// ���������� �����.
    /// </summary>
    public class Locality : BaseEntity
    {
        public string Name { get; set; }
        public virtual LocalityType LocalityType { get; set; }
        public virtual DistrictsRegion DistrictsRegion { get; set; }
    }

    /// <summary>
    /// ��� ����������� ������.
    /// </summary>
    public class LocalityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }
}