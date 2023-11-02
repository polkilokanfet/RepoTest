using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class LocalityDetailsViewModel
    {
        protected override bool AllowSave()
        {
            var messageService = Container.Resolve<IMessageService>();

            //если сменили статус столицы страны
            if (this.Item.IsCountryCapitalIsChanged)
            {
                //остальные столицы страны
                var otherCapitals = UnitOfWork.Repository<Locality>()
                    .Find(locality => locality.IsCountryCapital)
                    .Where(locality => Equals(locality.Region.District.Country, Item.Model.Region.District.Country))
                    .ToList();
                otherCapitals.Remove(Item.Model);

                if (Item.IsCountryCapital)
                {
                    //снимаем статус столицы с остальных
                    otherCapitals.ForEach(locality => locality.IsCountryCapital = false);
                }
                else
                {
                    if (!otherCapitals.Any())
                    {
                        messageService.Message("Информация", "Вы не можете лишить страну единственной столицы!\nСущность не сохранена!");
                        return false;
                    }
                }
            }

            //если сменили статус столицы округа
            if (this.Item.IsDistrictCapitalIsChanged)
            {
                //остальные столицы округа
                var otherCapitals = UnitOfWork.Repository<Locality>()
                    .Find(locality => locality.IsDistrictCapital)
                    .Where(locality => Equals(locality.Region.District, Item.Model.Region.District))
                    .ToList();
                otherCapitals.Remove(Item.Model);

                if (Item.IsDistrictCapital)
                {
                    //снимаем статус столицы с остальных
                    otherCapitals.ForEach(locality => locality.IsDistrictCapital = false);
                }
                else
                {
                    if (!otherCapitals.Any())
                    {
                        messageService.Message("Информация", "Вы не можете лишить округ единственной столицы!\nСущность не сохранена!");
                        return false;
                    }
                }
            }

            //если сменили статус столицы региона
            if (this.Item.IsRegionCapitalIsChanged)
            {
                //остальные столицы региона
                var otherCapitals = UnitOfWork.Repository<Locality>()
                    .Find(locality => locality.IsRegionCapital)
                    .Where(locality => Equals(locality.Region, Item.Model.Region))
                    .ToList();
                otherCapitals.Remove(Item.Model);

                if (Item.IsRegionCapital)
                {
                    //снимаем статус столицы с остальных
                    otherCapitals.ForEach(locality => locality.IsRegionCapital = false);
                }
                else
                {
                    if (!otherCapitals.Any())
                    {
                        messageService.Message("Информация", "Вы не можете лишить регион единственной столицы!\nСущность не сохранена!");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}