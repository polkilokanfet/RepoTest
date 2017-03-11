namespace HVTApp.Model
{
    /// <summary>
    /// Планируемый срок производства.
    /// </summary>
    public class PlannedTermProduction : BaseEntity
    {
        /// <summary>
        /// Планируемый срок изготовления (от).
        /// </summary>
        public int TermFrom { get; set; }

        /// <summary>
        /// Планируемый срок изготовления (до).
        /// </summary>
        public int TermTo { get; set; }
    }
}