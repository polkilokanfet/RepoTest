namespace HVTApp.Model
{
    public class TenderInfo : BaseEntity
    {
        public virtual ProductMain ProductMain { get; set; }

        /// <summary>
        /// ������������� - ����������.
        /// </summary>
        public virtual Company ProducerWinner { get; set; }

        public virtual CostInfo CostInfo { get; set; }
    }
}