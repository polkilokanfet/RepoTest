using System;

namespace HVTApp.Model
{
    public class CostInfo : BaseEntity
    {
        private double _cost;
        private double _vat;

        /// <summary>
        /// Цена без НДС.
        /// </summary>
        public double Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnCostWithVatChanged();
            }
        }

        /// <summary>
        /// Себестоимость.
        /// </summary>
        public double CostPrice { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public double Vat
        {
            get { return _vat; }
            set
            {
                _vat = value;
                OnCostWithVatChanged();
            }
        }

        /// <summary>
        /// Цена с НДС.
        /// </summary>
        public double CostWithVat => Cost*(1 + Vat/100);

        /// <summary>
        /// Маржинальный доход в деньгах.
        /// </summary>
        public double MarginalIncome => Cost - CostPrice;

        /// <summary>
        /// Маржинальный доход в процентах.
        /// </summary>
        public double MarginalIncomePercent
        {
            get
            {
                if (Cost.Equals(0))
                    return 0;

                return MarginalIncome/Cost*100;
            }
        }

        /// <summary>
        /// Событие изменения стоимости с НДС.
        /// </summary>
        public event EventHandler CostWithVatChanged;

        protected virtual void OnCostWithVatChanged()
        {
            CostWithVatChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}