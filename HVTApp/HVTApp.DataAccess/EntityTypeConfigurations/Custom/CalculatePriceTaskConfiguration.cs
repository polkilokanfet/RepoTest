namespace HVTApp.DataAccess
{
    public partial class CalculatePriceTaskConfiguration
    {
        public CalculatePriceTaskConfiguration()
        {
            Ignore(x => x.Status);
            Ignore(x => x.Sum);
            Ignore(x => x.Date);
            Ignore(x => x.ProductBlock);
            Ignore(x => x.Projects);
            Ignore(x => x.Offers);
            Ignore(x => x.Specifications);
        }
    }
}