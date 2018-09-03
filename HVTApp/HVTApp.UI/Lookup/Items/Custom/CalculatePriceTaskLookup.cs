namespace HVTApp.UI.Lookup
{
    public partial class CalculatePriceTaskLookup
    {
        public string StatusString => Status.ToString();
        public int Projects => Entity.Projects.Count;
        public int Offers => Entity.Offers.Count;
        public int Specifications => Entity.Specifications.Count;
    }
}