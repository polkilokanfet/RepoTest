namespace HVTApp.Model.Wrappers
{
    public partial class SalesUnitWrapper
    {
        public ProjectWrapper Project => OfferUnit.ProjectUnit.Project;
    }
}