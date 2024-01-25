using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTaskListItemDesignDepartmentHead : PriceEngineeringTaskListItemBase
    {
        public PriceEngineeringTaskListItemDesignDepartmentHead(PriceEngineeringTask entity) : base(entity)
        {
        }

        public override string StatusString
        {
            get
            {
                if (this.Entity.UserConstructor == null)
                {
                    return this.Entity.StartMoment.HasValue
                        ? "������� ���������"
                        : "������ �����������";
                }

                if (Entity.StartMoment.HasValue)
                {
                    if (Entity.Status.Equals(ScriptStep.VerificationRequestByConstructor))
                        return "������� ��������";

                    if (Entity.Status.Equals(ScriptStep.RejectByConstructor))
                        return "�������� (���������� ��������� �� ���������)";

                    return Entity.IsFinishedByConstructor
                        ? "�������� (����������� ������������)"
                        : "�������� (��������������� ������������)";
                }

                return "�������� (������ �����������)";
            }
        }

        public override bool ToShow => !Entity.Status.Equals(ScriptStep.Stop) &&
                                       !Entity.Status.Equals(ScriptStep.RejectByHead) &&
                                       (Entity.UserConstructor == null ||
                                        Entity.Status.Equals(ScriptStep.VerificationRequestByConstructor));
    }
}