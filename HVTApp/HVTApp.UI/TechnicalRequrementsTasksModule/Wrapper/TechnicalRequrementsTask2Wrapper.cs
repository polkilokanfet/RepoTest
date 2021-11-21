using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper
{
    public class TechnicalRequrementsTask2Wrapper : WrapperBase<TechnicalRequrementsTask>
    {
        #region SimpleProperties

        public string ProjectName
        {
            get
            {
                if (Requrements.Any() && Requrements.First().SalesUnit != null)
                {
                    return Requrements.First().SalesUnit.Project.Name;
                }

                return "no info";
            }
        }


        //Требуемая дата проработки
        public DateTime? DesiredFinishDate
        {
            get => GetValue<DateTime?>();
            set
            {
                if (value.HasValue && value.Value < DateTime.Today)
                {
                    Model.DesiredFinishDate = DateTime.Today;
                    OnPropertyChanged();
                    return;
                }

                SetValue(value);
            }
        }
        public DateTime? DesiredFinishDateOriginalValue => GetOriginalValue<DateTime?>(nameof(DesiredFinishDate));
        public bool DesiredFinishDateIsChanged => GetIsChanged(nameof(DesiredFinishDate));


        public bool LogisticsCalculationRequired
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool LogisticsCalculationRequiredOriginalValue => GetOriginalValue<bool>(nameof(LogisticsCalculationRequired));
        public bool LogisticsCalculationRequiredIsChanged => GetIsChanged(nameof(LogisticsCalculationRequired));

        public bool ExcelFileIsRequired
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public bool ExcelFileIsRequiredOriginalValue => GetOriginalValue<bool>(nameof(ExcelFileIsRequired));
        public bool ExcelFileIsRequiredIsChanged => GetIsChanged(nameof(ExcelFileIsRequired));

        public string TceNumber
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string TceNumberOriginalValue => GetOriginalValue<string>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));

        public DateTime? Start => Model.Start;

        #endregion

        #region ComplexProperties

        public UserWrapper BackManager
        {
            get { return GetWrapper<UserWrapper>(); }
            set { SetComplexValue<User, UserWrapper>(BackManager, value); }
        }

        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<TechnicalRequrements2Wrapper> Requrements { get; private set; }

        public IValidatableChangeTrackingCollection<AnswerFileTceWrapper> AnswerFiles { get; private set; }

        public IValidatableChangeTrackingCollection<TechnicalRequrementsTaskHistoryElementWrapper> HistoryElements { get; private set; }

        public IValidatableChangeTrackingCollection<ShippingCostFileWrapper> ShippingCostFiles { get; private set; }

        #endregion

        public IEnumerable<PriceCalculation> PriceCalculations => this.Model.PriceCalculations.OrderByDescending(x => x.TaskOpenMoment);

        public TechnicalRequrementsTask2Wrapper(TechnicalRequrementsTask model) : base(model)
        {
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(BackManager), Model.BackManager == null ? null : new UserWrapper(Model.BackManager));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Requrements == null) throw new ArgumentException("Requrements cannot be null");
            Requrements = new ValidatableChangeTrackingCollection<TechnicalRequrements2Wrapper>(Model.Requrements.Select(e => new TechnicalRequrements2Wrapper(e)));
            RegisterCollection(Requrements, Model.Requrements);

            if (Model.AnswerFiles == null) throw new ArgumentException("AnswerFiles cannot be null");
            AnswerFiles = new ValidatableChangeTrackingCollection<AnswerFileTceWrapper>(Model.AnswerFiles.Select(e => new AnswerFileTceWrapper(e)));
            RegisterCollection(AnswerFiles, Model.AnswerFiles);

            if (Model.HistoryElements == null) throw new ArgumentException("HistoryElements cannot be null");
            HistoryElements = new ValidatableChangeTrackingCollection<TechnicalRequrementsTaskHistoryElementWrapper>(Model.HistoryElements.OrderBy(element => element.Moment).Select(element => new TechnicalRequrementsTaskHistoryElementWrapper(element)));
            RegisterCollection(HistoryElements, Model.HistoryElements);

            if (Model.ShippingCostFiles == null) throw new ArgumentException("ShippingCostFiles cannot be null");
            ShippingCostFiles = new ValidatableChangeTrackingCollection<ShippingCostFileWrapper>(Model.ShippingCostFiles.Select(e => new ShippingCostFileWrapper(e)));
            RegisterCollection(ShippingCostFiles, Model.ShippingCostFiles);
        }

        public string ValidationResult
        {
            get
            {
                var sb = new StringBuilder();
                if (this.HasErrors)
                {
                    sb.Append("Ошибки в задаче: ");
                    sb.AppendLine(this.Errors.Select(x => x.Value.ToString()).Distinct().ToStringEnum());
                }

                foreach (var requrement in this.Requrements)
                {
                    if (requrement.HasErrors)
                    {
                        sb.Append($"Ошибки в требовании {requrement.Model.Id}: ");
                        sb.AppendLine(requrement.Validate(null).Select(x => x.ErrorMessage.ToString()).Distinct().ToStringEnum());
                    }

                    foreach (var file in requrement.Files)
                    {
                        if (file.HasErrors)
                        {
                            sb.Append($"Ошибки в файле {file.Model.Id}: ");
                            sb.AppendLine(file.Validate(null).Select(x => x.ErrorMessage.ToString()).Distinct().ToStringEnum());
                        }
                    }
                }

                return sb.ToString();
            }
        }

    }
}