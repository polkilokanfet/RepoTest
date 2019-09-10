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
            _getEntitiesForSelectWinnerCommand = async () =>
            {
                Func<List<Company>> func = () =>
                {
                    return Item.Participants.Select(x => x.Model).ToList();
                };
                return await Task.Run(func);
            };

            _getEntitiesForAddInTypesCommand = async () =>
            {
                var types = await UnitOfWork.Repository<TenderType>().GetAllAsync();
                return types.Except(Item.Model.Types).ToList();
            };
        }
    }
}