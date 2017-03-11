using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class ProductBase : BaseEntity
    {
        private CostInfo _costInfo;

        public virtual ProductsMainGroup ProductsMainGroup { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual TenderInfo TenderInfo { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
        public virtual DateInfo DateInfo { get; set; }
        public virtual TermsInfo TermsInfo { get; set; }
        public CostInfo CostInfo
        {
            get { return _costInfo; }
            set
            {
                if (_costInfo != null)
                    _costInfo.CostWithVatChanged -= OnCostWithVatChanged;

                _costInfo = value;
                if (_costInfo != null)
                    _costInfo.CostWithVatChanged += OnCostWithVatChanged;
            }
        }
        public virtual PaymentsInfo PaymentsInfo { get; set; }



        /// <summary>
        /// Единицы ТКП, сформированные на основании настоящего изделия.
        /// </summary>
        public virtual List<OfferProduct> OfferProducts { get; set; }



        /// <summary>
        /// Событие изменения стоимости с НДС.
        /// </summary>
        public event EventHandler CostWithVatChanged;

        protected virtual void OnCostWithVatChanged(object sender, EventArgs e)
        {
            CostWithVatChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}