namespace HVTApp.Model
{
    /// <summary>
    /// Населенный пункт.
    /// </summary>
    public class Locality : BaseEntity
    {
        public string Name { get; set; }
        public virtual LocalityType LocalityType { get; set; }
        public virtual DistrictsRegion DistrictsRegion { get; set; }
    }

    /// <summary>
    /// Тип населенного пункта.
    /// </summary>
    public class LocalityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }
}