using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrapper
{
    public partial class SupervisionWrapper
    {
        public bool IsNew { get; set; } = false;

        public SupervisionWrapper(SalesUnit product) : this(new Supervision { SalesUnit = product })
        {
            IsNew = true;
        }

        public SupervisionWrapper(SalesUnit product, SalesUnit productSupervision) : this(new Supervision { SalesUnit = product, SupervisionUnit = productSupervision })
        {
            IsNew = true;
        }

    }
}