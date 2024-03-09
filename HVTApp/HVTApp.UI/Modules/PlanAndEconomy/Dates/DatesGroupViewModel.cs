using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates
{
    public class DatesGroupViewModel : BindableBase
    {

        public IEnumerable<DatesUnitViewModel> Units { get; }
        public SalesUnit Model => Units.First().Model;

        #region Dates

        public DateTime? PickingDate
        {
            get => Units.First().PickingDate;
            set => Units.ForEach(unit => unit.PickingDate = value);
        }

        public DateTime? EndProductionDate
        {
            get => Units.First().EndProductionDate;
            set => Units.ForEach(unit => unit.EndProductionDate = value);
        }

        public DateTime? ShipmentDate
        {
            get => Units.First().ShipmentDate;
            set => Units.ForEach(unit => unit.ShipmentDate = value);
        }

        public DateTime? DeliveryDate
        {
            get => Units.First().DeliveryDate;
            set => Units.ForEach(unit => unit.DeliveryDate = value);
        }

        public DateTime? RealizationDate
        {
            get => Units.First().RealizationDate;
            set => Units.ForEach(unit => unit.RealizationDate = value);
        }

        #endregion

        public string SerialNumber
        {
            get
            {
                var serialNumbers = this.Units
                    .Where(x => string.IsNullOrEmpty(x.SerialNumber) == false)
                    .Select(x => x.SerialNumber)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                return serialNumbers.Any() 
                    ? serialNumbers.ToStringEnum() 
                    : string.Empty;
            }
            set => this.Units.ForEach(unit => unit.SerialNumber = value);
        }

        public bool HasFullInformation => Units.All(unit => unit.HasFullInformation);

        /// <summary>
        /// Заказ укомплектован?
        /// </summary>
        public bool IsCompleted => Units.All(unit => unit.IsCompleted);

        public DatesGroupViewModel(IEnumerable<DatesUnitViewModel> units)
        {
            Units = units.OrderBy(unit => unit.Model.GetOrderPosition()).ToList();
            Units.ForEach(unit =>
            {
                unit.ValueSetToPropertyEvent += () =>
                {
                    RaisePropertyChanged(nameof(HasFullInformation));
                    RaisePropertyChanged(nameof(IsCompleted));
                };

                unit.CalculatedDeliveryDateSetEvent += date =>
                {
                    RaisePropertyChanged(nameof(DeliveryDate));
                };

                unit.CalculatedRealizationDateSetEvent += date =>
                {
                    RaisePropertyChanged(nameof(RealizationDate));
                };


                unit.SerialNumberSetIntEvent += sn => { this.Units.ForEach(x => x.SetSerialNumber(sn)); };
                unit.SerialNumberSetStringEvent += sn => { this.Units.ForEach(x => x.SetSerialNumber(sn)); };
            });
        }

        /// <summary>
        /// Простановка даты реализации и даты доставки по дате отгрузки.
        /// </summary>
        public void SetCalculatedDates()
        {
            Units.ForEach(unit => unit.SetCalculatedDates());
        }
    }
}