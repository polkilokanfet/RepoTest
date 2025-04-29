using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTaskListItemObserver : PriceEngineeringTaskListItemBase
    {
        public PriceEngineeringTaskListItemObserver(PriceEngineeringTask entity) : base(entity)
        {
        }

        public override string StatusString
        {
            get
            {
                if (Entity.StartMoment.HasValue)
                {
                    if (this.Entity.UserConstructor == null)
                        return "������� ���������";

                    if (Entity.Status.Equals(ScriptStep.VerificationRequestByConstructor))
                        return "�� �������� � ������������";

                    if (Entity.Status.Equals(ScriptStep.RejectByConstructor))
                        return "��������� ��������� ������������";

                    return Entity.IsFinishedByConstructor
                        ? "����������� ������������"
                        : "��������������� ������������";
                }

                return "�����������";
            }
        }

        public override bool ToShow
        {
            get
            {
                if (Entity.Status.Equals(ScriptStep.Stop)) return false;
                if (Entity.Status.Equals(ScriptStep.RejectByHead)) return false;
                if (Entity.Status.Equals(ScriptStep.RejectByConstructor)) return false;
                if (Entity.Status.Equals(ScriptStep.Accept)) return false;
                if (Entity.Status.Equals(ScriptStep.VerificationAccept)) return false;

                return Entity.Status.Equals(ScriptStep.Start) ||
                       Entity.Status.Equals(ScriptStep.VerificationRequestByConstructor);
            }
        }
    }
}