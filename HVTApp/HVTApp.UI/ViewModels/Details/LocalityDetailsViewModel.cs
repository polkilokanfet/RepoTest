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

            //���� ������� ������ ������� ������
            if (this.Item.IsCountryCapitalIsChanged)
            {
                //��������� ������� ������
                var otherCapitals = UnitOfWork.Repository<Locality>()
                    .Find(locality => locality.IsCountryCapital)
                    .Where(locality => Equals(locality.Region.District.Country, Item.Model.Region.District.Country))
                    .ToList();
                otherCapitals.Remove(Item.Model);

                if (Item.IsCountryCapital)
                {
                    //������� ������ ������� � ���������
                    otherCapitals.ForEach(locality => locality.IsCountryCapital = false);
                }
                else
                {
                    if (!otherCapitals.Any())
                    {
                        messageService.Message("����������", "�� �� ������ ������ ������ ������������ �������!\n�������� �� ���������!");
                        return false;
                    }
                }
            }

            //���� ������� ������ ������� ������
            if (this.Item.IsDistrictCapitalIsChanged)
            {
                //��������� ������� ������
                var otherCapitals = UnitOfWork.Repository<Locality>()
                    .Find(locality => locality.IsDistrictCapital)
                    .Where(locality => Equals(locality.Region.District, Item.Model.Region.District))
                    .ToList();
                otherCapitals.Remove(Item.Model);

                if (Item.IsDistrictCapital)
                {
                    //������� ������ ������� � ���������
                    otherCapitals.ForEach(locality => locality.IsDistrictCapital = false);
                }
                else
                {
                    if (!otherCapitals.Any())
                    {
                        messageService.Message("����������", "�� �� ������ ������ ����� ������������ �������!\n�������� �� ���������!");
                        return false;
                    }
                }
            }

            //���� ������� ������ ������� �������
            if (this.Item.IsRegionCapitalIsChanged)
            {
                //��������� ������� �������
                var otherCapitals = UnitOfWork.Repository<Locality>()
                    .Find(locality => locality.IsRegionCapital)
                    .Where(locality => Equals(locality.Region, Item.Model.Region))
                    .ToList();
                otherCapitals.Remove(Item.Model);

                if (Item.IsRegionCapital)
                {
                    //������� ������ ������� � ���������
                    otherCapitals.ForEach(locality => locality.IsRegionCapital = false);
                }
                else
                {
                    if (!otherCapitals.Any())
                    {
                        messageService.Message("����������", "�� �� ������ ������ ������ ������������ �������!\n�������� �� ���������!");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}