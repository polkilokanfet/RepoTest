using System.Linq;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class IncludeInSpecificationCommandInTask : IncludeInSpecificationCommand
    {
        public IncludeInSpecificationCommandInTask(IUnityContainer container) : base(container, () => true)
        {
        }

        protected override bool AllowExecute(ISalesUnitsContainer salesUnitsContainer)
        {
            if (base.AllowExecute(salesUnitsContainer) == false)
                return false;

            if (salesUnitsContainer is PriceEngineeringTask priceEngineeringTask)
            {
                if (priceEngineeringTask.StatusesAll.Contains(ScriptStep.LoadToTceStart) == false)
                {
                    Container.Resolve<IMessageService>().Message("�����", "�� �� ������ ������������ ��������� ��� ������ � TeamCenter.");
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}