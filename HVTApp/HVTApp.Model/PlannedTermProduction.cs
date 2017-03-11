namespace HVTApp.Model
{
    /// <summary>
    /// ����������� ���� ������������.
    /// </summary>
    public class PlannedTermProduction : BaseEntity
    {
        /// <summary>
        /// ����������� ���� ������������ (��).
        /// </summary>
        public int TermFrom { get; set; }

        /// <summary>
        /// ����������� ���� ������������ (��).
        /// </summary>
        public int TermTo { get; set; }
    }
}