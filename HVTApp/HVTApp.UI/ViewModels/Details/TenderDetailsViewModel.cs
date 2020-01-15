using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class TenderDetailsViewModel
    {
        protected override void InitSpecialGetMethods()
        {
            _getEntitiesForSelectWinnerCommand = () =>
            {
                return Item.Participants.Select(x => x.Model).ToList();
            };

            _getEntitiesForAddInTypesCommand = () =>
            {
                var types = UnitOfWork.Repository<TenderType>().GetAll();
                return types.Except(Item.Model.Types).ToList();
            };
        }
    }
}