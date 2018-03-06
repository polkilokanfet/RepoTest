namespace HVTApp.DataAccess
{
    public partial class TenderConfiguration 
    {
        public TenderConfiguration()
        {
            HasRequired(x => x.Project).WithMany();
            HasMany(x => x.Types).WithMany();
            Property(x => x.DateOpen).IsRequired();
            Property(x => x.DateClose).IsRequired();
            Property(x => x.DateNotice).IsOptional();
            HasMany(x => x.Participants).WithMany();
            HasOptional(x => x.Winner).WithMany();
        }
    }
}