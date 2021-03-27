using System.Linq;

namespace HVTApp.UI.ViewModels
{
    public partial class TenderDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForSelectWinnerCommand = () =>
            {
                return Item.Participants.Select(companyWrapper => companyWrapper.Model).ToList();
            };
        }
    }
}