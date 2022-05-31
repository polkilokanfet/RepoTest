using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceCalculations.ViewModel.Wrapper
{
    public class StructureCost2Wrapper : WrapperBase<StructureCost>
    {
        #region SimpleProperties

        /// <summary>
        /// �����
        /// </summary>
        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string NumberOriginalValue => GetOriginalValue<string>(nameof(Number));
        public bool NumberIsChanged => GetIsChanged(nameof(Number));

        /// <summary>
        /// ����� scc ������������� �����
        /// </summary>
        public string OriginalStructureCostNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string OriginalStructureCostNumberOriginalValue => GetOriginalValue<string>(nameof(OriginalStructureCostNumber));
        public bool OriginalStructureCostNumberIsChanged => GetIsChanged(nameof(OriginalStructureCostNumber));

        /// <summary>
        /// ���������� (���������)
        /// </summary>
        public double AmountNumerator
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
        public double AmountNumeratorOriginalValue => GetOriginalValue<double>(nameof(AmountNumerator));
        public bool AmountNumeratorIsChanged => GetIsChanged(nameof(AmountNumerator));

        /// <summary>
        /// ���������� (�����������)
        /// </summary>
        public double AmountDenomerator
        {
            get { return GetValue<double>(); }
            set { SetValue(value); }
        }
        public double AmountDenomeratorOriginalValue => GetOriginalValue<double>(nameof(AmountDenomerator));
        public bool AmountDenomeratorIsChanged => GetIsChanged(nameof(AmountDenomerator));

        /// <summary>
        /// ������������� �������
        /// </summary>
        public double? UnitPrice
        {
            get { return GetValue<double?>(); }
            set { SetValue(value); }
        }
        public double? UnitPriceOriginalValue => GetOriginalValue<double?>(nameof(UnitPrice));
        public bool UnitPriceIsChanged => GetIsChanged(nameof(UnitPrice));

        /// <summary>
        /// �����������
        /// </summary>
        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        /// <summary>
        /// Id
        /// </summary>
        public System.Guid Id
        {
            get { return GetValue<System.Guid>(); }
            set { SetValue(value); }
        }
        public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
        public bool IdIsChanged => GetIsChanged(nameof(Id));
        #endregion

        #region GetProperties

        /// <summary>
        /// ���������� �� �������
        /// </summary>
        public double Amount => GetValue<double>();

        /// <summary>
        /// Total
        /// </summary>
        public double? Total => GetValue<double?>();
        #endregion

        public StructureCost2Wrapper(StructureCost model) : base(model)
        {
            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(UnitPrice))
                {
                    OnPropertyChanged(nameof(Total));
                }

                if (args.PropertyName == nameof(AmountNumerator) || 
                    args.PropertyName == nameof(AmountDenomerator))
                {
                    OnPropertyChanged(nameof(Amount));
                    OnPropertyChanged(nameof(Total));
                }
            };
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (string.IsNullOrWhiteSpace(Number))
            {
                yield return new ValidationResult("����� ������������ �� ����� ���� ������.", new[] { nameof(Number) });
            }

            if (AmountNumerator <= 0)
            {
                yield return new ValidationResult("���������� (���������) ������ ���� �������������.", new[] { nameof(AmountNumerator) });
            }

            if (AmountDenomerator <= 0)
            {
                yield return new ValidationResult("���������� (�����������) ������ ���� �������������.", new[] { nameof(AmountDenomerator) });
            }
        }

    }
}