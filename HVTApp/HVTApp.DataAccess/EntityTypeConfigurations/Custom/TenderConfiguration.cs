namespace HVTApp.DataAccess
{
    public partial class TenderConfiguration 
    {
        public TenderConfiguration()
        {
            HasRequired(tender => tender.Project).WithMany(project => project.Tenders).WillCascadeOnDelete(true);
            HasMany(tender => tender.Types).WithMany();
            Property(tender => tender.DateOpen).IsRequired();
            Property(tender => tender.DateClose).IsRequired();
            Property(tender => tender.DateNotice).IsOptional();
            HasMany(tender => tender.Participants).WithMany();
            HasOptional(tender => tender.Winner).WithMany();
        }
    }
}