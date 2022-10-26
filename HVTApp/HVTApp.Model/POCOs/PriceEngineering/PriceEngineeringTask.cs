using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка")]
    [DesignationPlural("Технико-стоимостные проработки")]
    public class PriceEngineeringTask : BaseEntity, IProductBlockContainer
    {
        #region DB

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Designation("№"), OrderStatus(3000)]
        public int Number { get; set; }

        [Designation("Id группы"), OrderStatus(2000)]
        public virtual Guid? ParentPriceEngineeringTasksId { get; set; }


        [Designation("Бюро конструкторов"), Required, OrderStatus(1900)]
        public virtual DesignDepartment DesignDepartment { get; set; }

        [Designation("Конструктор"), OrderStatus(1800)]
        public virtual User UserConstructor { get; set; }

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

        #endregion


        [Designation("Статус"), NotMapped]
        public PriceEngineeringTaskStatusEnum Status
        {
            get
            {
                return Statuses.Any() 
                    ? Statuses.OrderBy(status => status.Moment).Last().StatusEnum 
                    : PriceEngineeringTaskStatusEnum.Created;
            }
        }

        /// <summary>
        /// Задача в процессе проработки конструктором
        /// </summary>
        public bool InProcessByConstructor 
        {
            get
            {
                if (UserConstructor == null) return false;

                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                        return true;
                    case PriceEngineeringTaskStatusEnum.Started:
                        return true;
                    case PriceEngineeringTaskStatusEnum.Stopped:
                        return false;
                    case PriceEngineeringTaskStatusEnum.RejectedByManager:
                        return true;
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return false;
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                        return false;
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return false;
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                        return false;
                    case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                        return false;
                    case PriceEngineeringTaskStatusEnum.VerificationRejectededByHead:
                        return true;
                    default:
                        return false;
                }
            }
        }


        public bool IsFinishedByConstructor
        {
            get
            {
                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                    case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                    case PriceEngineeringTaskStatusEnum.Accepted:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// Статусы этой задачи и всех вложенных
        /// </summary>
        [Designation("Статусы этой задачи и всех вложенных"), NotMapped, NotForListView]
        public IEnumerable<PriceEngineeringTaskStatusEnum> StatusesAll
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

        [Designation("Статусы проработки"), Required, OrderStatus(50)]
        public virtual List<PriceEngineeringTaskStatus> Statuses { get; set; } = new List<PriceEngineeringTaskStatus>();


        [Designation("SalesUnits"), OrderStatus(10)]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("Запрос на проверку от руководителя"), OrderStatus(40)]
        public bool RequestForVerificationFromHead { get; set; } = false;

        [Designation("Запрос на проверку от исполнителя"), OrderStatus(35)]
        public bool RequestForVerificationFromConstructor { get; set; } = false;

        [Designation("Старт"), NotMapped]
        public DateTime? StartMoment
        {
            get
            {
                if (Statuses.Any() == false) return default;
                if (Status == PriceEngineeringTaskStatusEnum.Stopped || Status == PriceEngineeringTaskStatusEnum.Created) return default;

                return this.Statuses
                    .Where(x => x.StatusEnum == PriceEngineeringTaskStatusEnum.Started)
                    .OrderBy(x => x.Moment)
                    .Last()
                    .Moment;
            }
        }

        [NotMapped]
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

        public IEnumerable<StructureCost> GetStructureCosts(string tceNumber = null, int? salesUnitsAmount = null)
        {
            salesUnitsAmount = salesUnitsAmount ?? SalesUnits.Count;

            var structureCostVersion = this.StructureCostVersions.FirstOrDefault(x => x.OriginalStructureCostNumber == ProductBlockEngineer.StructureCostNumber);
            var structureCostNumber = structureCostVersion == null
                ? ProductBlockEngineer.StructureCostNumber
                : $"{tceNumber} V{structureCostVersion.Version:D2}";

            //стракчакост основного блока
            yield return new StructureCost
            {
                Comment = ProductBlockEngineer.ToString().LimitLengh(200),
                Number = structureCostNumber,
                OriginalStructureCostProductBlock = ProductBlockEngineer,
                OriginalStructureCostNumber = ProductBlockEngineer.StructureCostNumber,
                AmountNumerator = 1,
                AmountDenomerator = 1
            };

            //стракчакосты добавленных блоков
            foreach (var blockAdded in ProductBlocksAdded.Where(x => x.IsRemoved == false))
            {

                var structureCostVersion1 = blockAdded.StructureCostVersions.FirstOrDefault(x => x.OriginalStructureCostNumber == blockAdded.ProductBlock.StructureCostNumber);
                var structureCostNumber1 = structureCostVersion1 == null
                    ? blockAdded.ProductBlock.StructureCostNumber
                    : $"{tceNumber} V{structureCostVersion1.Version:D2}";

                yield return new StructureCost
                {
                    Comment = blockAdded.ProductBlock.ToString().LimitLengh(200),
                    Number = structureCostNumber1,
                    OriginalStructureCostProductBlock = blockAdded.ProductBlock,
                    OriginalStructureCostNumber = blockAdded.ProductBlock.StructureCostNumber,
                    AmountNumerator = blockAdded.Amount,
                    AmountDenomerator = blockAdded.IsOnBlock ? 1 : salesUnitsAmount.Value
                };
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
                ? $"Технико-стоимостная проработка объектов: {SalesUnits.Select(x => x.Facility).Distinct().OrderBy(x => x.Name).ToStringEnum(", ")}" 
                : $"Технико-стоимостная проработка блока: {this.ProductBlock}";
        }

        [NotMapped, NotForListView, NotForDetailsView]
        public ProductBlock ProductBlock => this.ProductBlockEngineer;

        /// <summary>
        /// Проработка задачи принята менеджером (со всеми вложенными задачами).
        /// </summary>
        public bool IsTotalAccepted => this.StatusesAll.All(x => x == PriceEngineeringTaskStatusEnum.Accepted);

        /// <summary>
        /// Проработка задачи остановлена менеджером (со всеми вложенными задачами).
        /// </summary>
        public bool IsTotalStopped => this.StatusesAll.All(x => x == PriceEngineeringTaskStatusEnum.Stopped);

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
            return messages.OrderByDescending(x => x.Moment);
        }
    }
}