namespace HVTApp.Model
{
    /// <summary>
    /// Тип населенного пункта.
    /// </summary>
    public class LocalityType : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }
}