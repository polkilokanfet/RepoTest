using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.PriceEngineering.Messages
{
    public class PriceEngineeringTaskMessageWrapper1 : WrapperBase<PriceEngineeringTaskMessage>
    {
        public PriceEngineeringTaskMessageWrapper1(PriceEngineeringTaskMessage model) : base(model) { }

        #region SimpleProperties

        /// <summary>
        /// Момент
        /// </summary>
        public DateTime Moment
        {
            get => GetValue<System.DateTime>();
            set => SetValue(value);
        }
        public System.DateTime MomentOriginalValue => GetOriginalValue<System.DateTime>(nameof(Moment));
        public bool MomentIsChanged => GetIsChanged(nameof(Moment));

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string MessageOriginalValue => GetOriginalValue<string>(nameof(Message));
        public bool MessageIsChanged => GetIsChanged(nameof(Message));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Автор
        /// </summary>
        public UserEmptyWrapper Author
        {
            get => GetWrapper<UserEmptyWrapper>();
            set => SetComplexValue<User, UserEmptyWrapper>(Author, value);
        }
        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Author), Model.Author == null ? null : new UserEmptyWrapper(Model.Author));
        }
    }
}