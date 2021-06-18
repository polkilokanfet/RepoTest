namespace HVTApp.Model.Price
{
    /// <summary>
    /// �������� �� �����������
    /// </summary>
    public class PriceStub : PriceOfUnit
    {
        private readonly string _structureCostNumber;
        public override string Comment => _structureCostNumber ?? "�� �����������";

        public override bool ContainsAnyAnalog => false;

        public override double? LaborHours { get; }

        public PriceStub(string name, double amount, double? unitPrice = null, string structureCostNumber = null, double? laborHours = null)
        {
            _structureCostNumber = structureCostNumber;
            Name = name;
            Amount = amount;
            UnitPrice = unitPrice;
            LaborHours = laborHours;
        }
    }
}