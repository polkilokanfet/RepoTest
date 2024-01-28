using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Настройки рассылки отчётов")]
    [DesignationPlural("Настройки рассылки отчётов")]
    public class NotificationsReportsSettings : BaseEntity
    {
        #region ChiefEngineerReport

        [Designation("Когда отправлен последний отчёт ГК ВВА"), OrderStatus(10), Required]
        public DateTime ChiefEngineerReportMoment { get; set; }

        [Designation("Список рассылки отчёта ГК ВВА"), OrderStatus(8), Required]
        public virtual List<User> ChiefEngineerReportDistributionList { get; set; } = new List<User>();

        [Designation("Список рассылки уведомлений о платежах"), OrderStatus(7)]
        public virtual List<User> SavePaymentDocumentDistributionList { get; set; } = new List<User>();

        #endregion
    }
}