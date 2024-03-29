﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [AllowEdit(Role.Economist)]
    [Designation("Платежный документ")]
    public partial class PaymentDocument : BaseEntity
    {
        [Designation("Номер"), Required, OrderStatus(10), MaxLength(25)]
        public string Number { get; set; } = "б/н";

        [Designation("Дата"), NotMapped, OrderStatus(20)]
        public DateTime Date => Payments.Any() ? Payments.First().Date : DateTime.Today;

        [Designation("Платежи"), Required]
        public virtual List<PaymentActual> Payments { get; set; } = new List<PaymentActual>();


        [Designation("НДС"), Required]
        public double Vat { get; set; } = GlobalAppProperties.Actual.Vat;

        /// <summary>
        /// Сумма с НДС
        /// </summary>
        [NotMapped]
        public double SumWithVat => (1 + Vat/100) * Payments.Sum(paymentActual => paymentActual.Sum);

        public override string ToString()
        {
            return $"Платеж №{Number} от {Date.ToShortDateString()} г.";
        }
    }
}