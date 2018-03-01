using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TenderLookup
    {
        public string Type
        {
            get
            {
                string result = string.Empty;
                foreach (var entityType in Entity.Types.OrderBy(x => x.Type))
                {
                    switch (entityType.Type)
                    {
                        case (TenderTypeEnum.ToProject):
                            result += "��������������; ";
                            break;
                        case (TenderTypeEnum.ToSupply):
                            result += "��������; ";
                            break;
                        case (TenderTypeEnum.ToWork):
                            result += "������; ";
                            break;
                    }
                }
                return result;
            }
        }
    }
}