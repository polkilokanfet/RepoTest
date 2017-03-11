namespace HVTApp.Model
{
    /// <summary>
    /// Округ страны.
    /// </summary>
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public virtual Country Country { get; set; }
    }
}