﻿using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TaskInvoiceForPayment1.ForPlanMaker
{
    public class TechnicalRequrementsWrapperTip : WrapperBase<TechnicalRequrements>
    {
        public TechnicalRequrementsWrapperTip(TechnicalRequrements model) : base(model) { }

        #region CollectionProperties

        /// <summary>
        /// SalesUnits
        /// </summary>
        public IValidatableChangeTrackingCollection<SalesUnitWrapperTip> SalesUnits { get; private set; }

        #endregion

        protected override void InitializeCollectionProperties()
        {
            if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
            SalesUnits = new OrderPositionsCollection(Model.SalesUnits.Select(e => new SalesUnitWrapperTip(e)));
            RegisterCollection(SalesUnits, Model.SalesUnits);
        }
    }
}