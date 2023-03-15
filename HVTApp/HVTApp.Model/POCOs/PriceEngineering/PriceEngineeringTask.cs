using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Services;

namespace HVTApp.Model.POCOs
{
    //то, что фиксируется в БД
    [Designation("Технико-стоимостная проработка")]
    [DesignationPlural("Технико-стоимостные проработки")]
    public partial class PriceEngineeringTask : BaseEntity, IProductBlockContainer, IBasePriorityTask, IStructureCostVersionsContainer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Designation("№"), OrderStatus(3000)]
        public int Number { get; set; }

        [Designation("Id группы"), OrderStatus(2000)]
        public virtual Guid? ParentPriceEngineeringTasksId { get; set; }


        [Designation("Бюро конструкторов"), Required, OrderStatus(1900)]
        public virtual DesignDepartment DesignDepartment { get; set; }

        [Designation("Конструктор"), OrderStatus(1800)]
        public virtual User UserConstructor { get; set; }

        [Designation("Плановик"), OrderStatus(1700)]
        public virtual User UserPlanMaker { get; set; }

        /// <summary>
        /// Если задача инициирована конструктором (например, для добавления площадки обслуживания выключателя)
        /// </summary>
        [Designation("Инициатор подзадачи")]
        public virtual User UserConstructorInitiator { get; set; }


        [Designation("Количество блоков продукта"), Required, OrderStatus(950)]
        public int Amount { get; set; }

        [Designation("Блок продукта от менеджера"), Required, OrderStatus(900)]
        public virtual ProductBlock ProductBlockManager { get; set; }

        [Designation("Блок продукта от инженера-конструктора"), Required, OrderStatus(850)]
        public virtual ProductBlock ProductBlockEngineer { get; set; }

        [Designation("Добавленные блоки продукта от инженера-конструктора"), OrderStatus(800)]
        public virtual List<PriceEngineeringTaskProductBlockAdded> ProductBlocksAdded { get; set; } = new List<PriceEngineeringTaskProductBlockAdded>();


        [Designation("Файлы технических требований"), Required, OrderStatus(610)]
        public virtual List<PriceEngineeringTaskFileTechnicalRequirements> FilesTechnicalRequirements { get; set; } = new List<PriceEngineeringTaskFileTechnicalRequirements>();

        [Designation("Файлы ответов ОГК"), OrderStatus(600)]
        public virtual List<PriceEngineeringTaskFileAnswer> FilesAnswers { get; set; } = new List<PriceEngineeringTaskFileAnswer>();


        [Designation("Переписка"), OrderStatus(500)]
        public virtual List<PriceEngineeringTaskMessage> Messages { get; set; } = new List<PriceEngineeringTaskMessage>();


        [Designation("Id материнской задачи"), OrderStatus(100)]
        public virtual Guid? ParentPriceEngineeringTaskId { get; set; }

        [Designation("Дочерние задачи"), OrderStatus(90)]
        public virtual List<PriceEngineeringTask> ChildPriceEngineeringTasks { get; set; } = new List<PriceEngineeringTask>();

        [Designation("Версии SCC"), OrderStatus(80)]
        public virtual List<StructureCostVersion> StructureCostVersions { get; set; } = new List<StructureCostVersion>();


        [Designation("Строки расчётов ПЗ"), OrderStatus(80)]
        public virtual List<PriceCalculationItem> PriceCalculationItems { get; set; } = new List<PriceCalculationItem>();

        [Designation("Приоритет проработки задачи"), OrderStatus(81)]
        public virtual DateTime? TermPriority { get; set; }

        [Designation("Статусы проработки"), Required, OrderStatus(50)]
        public virtual List<PriceEngineeringTaskStatus> Statuses { get; set; } = new List<PriceEngineeringTaskStatus>();

        [Designation("SalesUnits"), OrderStatus(10)]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("Запрос на проверку от руководителя"), OrderStatus(40)]
        public bool RequestForVerificationFromHead { get; set; } = false;

        [Designation("Запрос на проверку от исполнителя"), OrderStatus(35)]
        public bool RequestForVerificationFromConstructor { get; set; } = false;

        [Designation("ТЗ валидно для производства"), OrderStatus(36)]
        public bool IsValidForProduction { get; set; }
    }
    
    public partial class PriceEngineeringTask
    {
        [Designation("Статус"), NotMapped]
        public ScriptStep Status
        {
            get
            {
                var status1 = Statuses.Any() ? Statuses.OrderBy(status => status.Moment).Last().StatusEnum : 0;
                return ScriptStep.FromValue(status1);
            }
        }

        /// <summary>
        /// Задача в процессе проработки конструктором
        /// </summary>
        public bool IsInProcessByConstructor 
        {
            get
            {
                if (UserConstructor == null) return false;

                var statuses = new List<ScriptStep>
                {
                    ScriptStep.Create,
                    ScriptStep.Start,
                    ScriptStep.RejectByManager,
                    ScriptStep.VerificationRejectByHead
                };

                return statuses.Contains(Status);
            }
        }

        public bool IsFinishedByConstructor
        {
            get
            {
                var statuses = new List<ScriptStep>
                {
                    ScriptStep.FinishByConstructor,
                    ScriptStep.VerificationRequestByConstructor,
                    ScriptStep.VerificationAcceptByHead,
                    ScriptStep.Accept
                };

                return statuses.Contains(Status);
            }
        }

        public bool IsAccepted
        {
            get
            {
                var steps = new[]
                {
                    ScriptStep.Accept,
                    ScriptStep.LoadToTceStart,
                    ScriptStep.LoadToTceFinish,
                    ScriptStep.ProductionRequestStart,
                    ScriptStep.ProductionRequestFinish
                };

                return steps.Contains(this.Status);
            }
        }

        /// <summary>
        /// Проработка задачи принята менеджером (со всеми вложенными задачами).
        /// </summary>
        public bool IsAcceptedTotal
        {
            get
            {
                var steps = new[]
                {
                    ScriptStep.Accept,
                    ScriptStep.LoadToTceStart,
                    ScriptStep.LoadToTceFinish,
                    ScriptStep.ProductionRequestStart,
                    ScriptStep.ProductionRequestFinish
                };

                return this.StatusesAll.All(step => steps.Contains(step));
            }
        }

        /// <summary>
        /// Проработка задачи остановлена менеджером (со всеми вложенными задачами).
        /// </summary>
        public bool IsStoppedTotal => this.StatusesAll.All(x => x.Equals(ScriptStep.Stop));

        /// <summary>
        /// Статусы этой задачи и всех вложенных
        /// </summary>
        [Designation("Статусы этой задачи и всех вложенных"), NotMapped, NotForListView]
        public IEnumerable<ScriptStep> StatusesAll
        {
            get
            {
                yield return this.Status;
                foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
                {
                    foreach (var taskStatus in childPriceEngineeringTask.StatusesAll)
                    {
                        yield return taskStatus;
                    }
                }
            }
        }

        [Designation("Старт"), NotMapped]
        public DateTime? StartMoment
        {
            get
            {
                if (IsStarted == false)
                    return default;

                return this.Statuses
                    .Where(x => x.StatusEnum == ScriptStep.Start.Value)
                    .OrderBy(x => x.Moment)
                    .Last()
                    .Moment;
            }
        }

        public bool IsStarted => !Status.Equals(ScriptStep.Stop) && 
                                 !Status.Equals(ScriptStep.Create);

        public bool HasSccInTce
        {
            get
            {
                if (this.StructureCostVersions.Any(x => x.Version.HasValue && x.OriginalStructureCostNumber == this.ProductBlock.StructureCostNumber) == false)
                    return false;

                if (this.ProductBlocksAdded.Where(x => x.IsRemoved == false).Any(x => x.HasSccInTce == false))
                    return false;

                if (this.ChildPriceEngineeringTasks.Any(x => x.HasSccInTce == false))
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Задача подходит для загрузки в ТС
        /// </summary>
        /// <returns></returns>
        public bool IsSuitableForLoadInTce()
        {
            var scriptSteps = new List<ScriptStep>
            {
                ScriptStep.LoadToTceStart,
                ScriptStep.LoadToTceFinish
            };

            return GetAllPriceEngineeringTasks().All(priceEngineeringTask => scriptSteps.Contains(priceEngineeringTask.Status));
        }

        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForOpenOrder(User user)
        {
            foreach (var priceEngineeringTask in this.GetAllPriceEngineeringTasks())
            {
                if (priceEngineeringTask.Status.Equals(ScriptStep.ProductionRequestStart) &&
                    priceEngineeringTask.UserPlanMaker?.Id == user.Id)
                    yield return priceEngineeringTask;
            }
        }


        /// <summary>
        /// Вернуть все задачи, которые прорабатывает данный User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForWork(User user)
        {
            if (Equals(user.Id, this.UserConstructor?.Id))
            {
                yield return this;
            }

            foreach (var childPriceEngineeringTask in ChildPriceEngineeringTasks)
            {
                foreach (var priceEngineeringTask in childPriceEngineeringTask.GetSuitableTasksForWork(user))
                {
                    yield return priceEngineeringTask;
                }
            }
        }


        /// <summary>
        /// Вернуть все задачи, которые прорабатывает бюро пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForInstruct(User user)
        {
            if (this.DesignDepartment?.Head.Id == user.Id)
            {
                yield return this;
            }

            foreach (var childPriceEngineeringTask in ChildPriceEngineeringTasks)
            {
                foreach (var priceEngineeringTask in childPriceEngineeringTask.GetSuitableTasksForInstruct(user))
                {
                    yield return priceEngineeringTask;
                }
            }
        }

        public IEnumerable<DesignDepartment> GetDepartments()
        {
            yield return this.DesignDepartment;
            foreach (var childPriceEngineeringTask in ChildPriceEngineeringTasks)
            {
                foreach (var department in childPriceEngineeringTask.GetDepartments())
                {
                    yield return department;
                }
            }
        }

        public IEnumerable<StructureCost> GetStructureCosts(string tceNumber = null, int? salesUnitsAmount = null, IPriceService priceService = null)
        {
            salesUnitsAmount = salesUnitsAmount ?? SalesUnits.Count;

            //стракчакост основного блока
            var structureCostNumber = GetStructureCostNumber(this, tceNumber, priceService);
            yield return GetNewStructureCost(ProductBlockEngineer, structureCostNumber, 1, 1);

            //стракчакосты добавленных блоков
            foreach (var blockAdded in ProductBlocksAdded.Where(productBlockAdded => productBlockAdded.IsRemoved == false))
            {
                var structureCostNumber1 = GetStructureCostNumber(blockAdded, tceNumber, priceService);
                yield return GetNewStructureCost(blockAdded.ProductBlock, structureCostNumber1, blockAdded.Amount, blockAdded.IsOnBlock ? 1 : salesUnitsAmount.Value);
            }

            //стракчакосты вложенных задач
            foreach (var childPriceEngineeringTask in ChildPriceEngineeringTasks)
            {
                foreach (var structureCost in childPriceEngineeringTask.GetStructureCosts(tceNumber, salesUnitsAmount))
                {
                    yield return structureCost;
                }
            }
        }

        private string GetStructureCostNumber(IStructureCostVersionsContainer structureCostVersionsContainer, string tceNumber, IPriceService priceService)
        {
            if (tceNumber != null && structureCostVersionsContainer.GetStructureCostVersion() != null)
            {
                return $"{tceNumber} V{structureCostVersionsContainer.GetStructureCostVersion().Version:D2}";
            }

            if (string.IsNullOrWhiteSpace(structureCostVersionsContainer.ProductBlock.StructureCostNumber) == false)
            {
                return structureCostVersionsContainer.ProductBlock.StructureCostNumber;
            }

            if (priceService != null)
            {
                return priceService.GetAnalogWithPrice(this.ProductBlock)?.StructureCostNumber;
            }

            return "no scc";
        }

        private StructureCost GetNewStructureCost(ProductBlock productBlock, string structureCostNumber, double amountNumerator, double amountDenomerator)
        {
            return new StructureCost
            {
                Comment = productBlock.ToString().LimitLength(200),
                Number = structureCostNumber,
                OriginalStructureCostProductBlock = productBlock,
                OriginalStructureCostNumber = productBlock.StructureCostNumber,
                AmountNumerator = amountNumerator,
                AmountDenomerator = amountDenomerator
            };
        }

        public IEnumerable<PriceEngineeringTask> GetAllPriceEngineeringTasks()
        {
            yield return this;

            foreach (var childPriceEngineeringTask in ChildPriceEngineeringTasks)
            {
                foreach (var priceEngineeringTask in childPriceEngineeringTask.GetAllPriceEngineeringTasks())
                {
                    yield return priceEngineeringTask;
                }
            }
        }

        public override string ToString()
        {
            return SalesUnits.Any() 
                ? $"Технико-стоимостная проработка объектов: {SalesUnits.Select(salesUnit => salesUnit.Facility).Distinct().OrderBy(x => x.Name).ToStringEnum(", ")}" 
                : $"Технико-стоимостная проработка блока: {this.ProductBlock}";
        }

        [NotForListView, NotForDetailsView]
        public ProductBlock ProductBlock => this.ProductBlockEngineer;

        /// <summary>
        /// Формирует продукт из проработанных блоков
        /// </summary>
        /// <returns></returns>
        public Product GetProduct()
        {
            return new Product
            {
                ProductBlock = this.ProductBlockEngineer,
                DependentProducts = this.ChildPriceEngineeringTasks
                    .Where(x => x.UserConstructorInitiator == null)
                    .Select(x =>
                    new ProductDependent
                    {
                        Product = x.GetProduct(),
                        Amount = 1
                    }).ToList()
            };
        }

        public string GetDirectoryName()
        {
            return $"{this.Number:D4}";
        }

        /// <summary>
        /// Сообщения + "сообщения-статусы"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMessage> GetMessages()
        {
            var messages = new List<IMessage>(this.Messages);
            messages.AddRange(this.Statuses.Select(PriceEngineeringTaskStatusMessage.Convert));
            return messages.OrderByDescending(message => message.Moment);
        }
    }
}