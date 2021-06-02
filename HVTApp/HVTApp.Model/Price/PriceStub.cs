namespace HVTApp.Model.Price
{
    /// <summary>
    /// «аглушка из калькул€ции
    /// </summary>
    public class PriceStub : PriceOfUnit
    {
        private readonly string _structureCostNumber;
        public override string Comment => _structureCostNumber ?? "из калькул€ции";

        public override bool ContainsAnyAnalog => false;


        public PriceStub(string name, double amount, double? unitPrice = null, string structureCostNumber = null)
        {
            _structureCostNumber = structureCostNumber;
            Name = name;
            Amount = amount;
            UnitPrice = unitPrice;
        }
    }
}