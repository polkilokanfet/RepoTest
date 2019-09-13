using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapper
    {
        public override void InitializeOther()
        {
            base.InitializeOther();
            if (Model.Number == null)
            {
                Model.Number = new DocumentNumber();
            }
        }
    }
}
