﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка")]
    [DesignationPlural("Технико-стоимостные проработки")]
    public class PriceEngineeringTask : BaseEntity
    {
        [Designation("Id группы"), OrderStatus(2000)]
        public virtual Guid ParentPriceEngineeringTasksId { get; set; }


        [Designation("Конструктор"), OrderStatus(1800)]
        public virtual User UserConstructor { get; set; }


        [Designation("Количество блоков продукта"), Required, OrderStatus(950)]
        public int Amount { get; set; }

        [Designation("Блок продукта от менеджера"), Required, OrderStatus(900)]
        public virtual ProductBlock ProductBlockManager { get; set; }

        [Designation("Блок продукта от инженера-конструктора"), Required, OrderStatus(850)]
        public virtual ProductBlock ProductBlockEngineer { get; set; }

        [Designation("Добавленные блоки продукта от инженера-конструктора"), OrderStatus(800)]
        public virtual List<PriceEngineeringTaskProductBlockAdded> ProductBlocksAdded { get; set; } = new List<PriceEngineeringTaskProductBlockAdded>();


        [Designation("Файлы технических требований"), OrderStatus(610)]
        public virtual List<PriceEngineeringTaskFileTechnicalRequirements> FilesTechnicalRequirements { get; set; } = new List<PriceEngineeringTaskFileTechnicalRequirements>();

        [Designation("Файлы ответов ОГК"), OrderStatus(600)]
        public virtual List<PriceEngineeringTaskFileAnswer> FilesAnswers { get; set; } = new List<PriceEngineeringTaskFileAnswer>();


        [Designation("Переписка"), OrderStatus(500)]
        public virtual List<PriceEngineeringTaskMessage> Messages { get; set; } = new List<PriceEngineeringTaskMessage>();


        [Designation("Id материнской задачи"), OrderStatus(100)]
        public virtual Guid ParentPriceEngineeringTaskId { get; set; }

        [Designation("Дочерние задачи"), OrderStatus(90)]
        public List<PriceEngineeringTask> ChildPriceEngineeringTasks { get; set; } = new List<PriceEngineeringTask>();


        [Designation("Статусы проработки"), Required, OrderStatus(50)]
        public virtual List<PriceEngineeringTaskStatus> Statuses { get; set; } = new List<PriceEngineeringTaskStatus>();


        [Designation("SalesUnits"), OrderStatus(10)]
        public List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

    }
}