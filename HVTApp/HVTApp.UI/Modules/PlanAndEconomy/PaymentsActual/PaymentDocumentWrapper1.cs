using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class PaymentDocumentWrapper1 : WrapperBase<PaymentDocument>
    {
        private readonly IMessageService _messageService;

        #region SimpleProperties

        //Number
        public string Number
        {
            get => Model.Number;
            set => SetValue(value);
        }
        public string NumberOriginalValue => GetOriginalValue<string>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        //Vat
        public double Vat
        {
            get => Model.Vat;
            set => SetValue(value);
        }
        public double VatOriginalValue => GetOriginalValue<double>(nameof(Vat));
        public bool VatIsChanged => GetIsChanged(nameof(Vat));

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<PaymentActualWrapper2> Payments { get; }

        #endregion

        /// <summary>
        /// Дата платежей
        /// </summary>
        public DateTime DockDate
        {
            get => Model.Date;
            set
            {
                if (value > DateTime.Today.AddYears(50))
                {
                    _messageService.ShowOkMessageDialog("Предупреждение", "Даты позже 50 лет с текущей даты недопустимы!");
                    return;
                }
                Payments.ForEach(payment => payment.Date = value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Сумма платежного документа с НДС
        /// </summary>
        public double DockSumWithVat
        {
            get
            {
                return Payments.Any()
                    ? Payments.Sum(payment => payment.SumWithVat)
                    : 0;
            }
            set
            {
                if (value < 0)
                {
                    _messageService.ShowOkMessageDialog("Предупреждение", "Отрицательные платежи недопустимы!");
                    return;
                }

                if (!Payments.Any())
                {
                    _messageService.ShowOkMessageDialog("Предупреждение", "Добавте в платежку оборудование.");
                    return;
                }

                //неоплаченное без учета текущего платежа (c НДС)
                var notPaidWithVat = Payments.Sum(payment => payment.SalesUnit.SumNotPaidWithVat) + Payments.Sum(x => x.SumWithVat);

                //если предложена сумма, превышающая, не пропускаем
                if (value - notPaidWithVat > 0.000001)
                {
                    _messageService.ShowOkMessageDialog("Предупреждение", $"Сумма платежки слишком велика. Возможный максимум: {notPaidWithVat:C}");
                    return;
                }

                Payments.ForEach(payment => payment.SumWithVat = value * ((payment.SalesUnit.SumNotPaidWithVat + payment.SumWithVat) / notPaidWithVat));
            }
        }


        public PaymentDocumentWrapper1(PaymentDocument model, IUnitOfWork unitOfWork, IMessageService messageService) : base(model)
        {
            _messageService = messageService;

            #region InitializeCollectionProperties

            if (Model.Payments == null) throw new ArgumentException("Payments cannot be null");
            Payments = new ValidatableChangeTrackingCollection<PaymentActualWrapper2>(Model.Payments.Select(paymentActual => new PaymentActualWrapper2(paymentActual, unitOfWork.Repository<SalesUnit>().GetById(paymentActual.SalesUnitId), model)));
            RegisterCollection(Payments, Model.Payments);

            #endregion

            this.Payments.CollectionChanged += (sender, args) =>
            {
                //добавляем в SalesUnit новые платежи
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var paymentActualWrapper in args.NewItems.Cast<PaymentActualWrapper2>())
                    {
                        paymentActualWrapper.SalesUnit.PaymentsActual.Add(paymentActualWrapper.Model);
                    }
                }

                //удаляем из SalesUnit старые платежи
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var paymentActualWrapper in args.OldItems.Cast<PaymentActualWrapper2>())
                    {
                        paymentActualWrapper.SalesUnit.PaymentsActual.Remove(paymentActualWrapper.Model);
                    }
                }
            };

            this.Payments.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged(nameof(DockSumWithVat));
                OnPropertyChanged(nameof(DockDate));
            };
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (Vat < 0)
            {
                yield return new ValidationResult("НДС не может быть отрицательным", new[] { nameof(Vat) });
            }

        }
    }
}