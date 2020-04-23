using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrapper
{
    public partial class DocumentWrapper
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