namespace HVTApp.Model
{
    public class BankDetails : BaseEntity
    {
        public string BankName { get; set; }
        public string BankIdentificationCode { get; set; }
        public string CorrespondentAccount { get; set; }
        public string CheckingAccount { get; set; }
    }
}