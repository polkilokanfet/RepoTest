using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
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
                    if (Entity.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification)
                        return "������� ��������";

                    if (Entity.Status == PriceEngineeringTaskStatusEnum.RejectedByConstructor)
                        return "�������� (���������� ��������� �� ���������)";

                    return Entity.IsFinishedByConstructor
                        ? "�������� (����������� ������������)"
                        : "�������� (��������������� ������������)";
                }

                return "�������� (������ �����������)";
            }
        }

        public override bool ToShow => Entity.Status != PriceEngineeringTaskStatusEnum.Stopped &&
                                       (Entity.UserConstructor == null ||
                                        Entity.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification);
    }
}