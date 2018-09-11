namespace HVTApp.DataAccess
{
    public partial class TestHusbandConfiguration
    {
        public TestHusbandConfiguration()
        {
            HasOptional(x => x.Wife).WithOptionalPrincipal(x => x.Husband);
            HasMany(x => x.Children).WithRequired(x => x.Husband);
        }
    }

    public partial class TestWifeConfiguration
    {
        public TestWifeConfiguration()
        {
        }
    }
}