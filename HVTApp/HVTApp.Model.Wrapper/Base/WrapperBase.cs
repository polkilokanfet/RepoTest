using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
    public abstract class WrapperBase<T> : NotifyDataErrorInfoBase, IWrapper<T>
        where T : class, IBaseEntity
    {
        //уже созданные обертки сущностей
        private readonly IDictionary<IBaseEntity, object> _existsWrappers;

        /// <summary>
        /// Словарь оригинальных значений. В словарь вносятся только те оригинальные значения, которые были изменены.
        /// </summary>
        private readonly Dictionary<string, object> _originalValues = new Dictionary<string, object>();

        /// <summary>
        /// Список объектов в которых отслеживаются изменения (свойства объекта не примитивного типа и коллекции).
        /// </summary>
        private readonly List<IValidatableChangeTracking> _trackingObjects = new List<IValidatableChangeTracking>();

        /// <summary>
        /// Объект, обертка которого создана в этом классе.
        /// </summary>
        public T Model { get; }

        public bool EqualsModels(IWrapper<T> wrapper)
        {
            return Equals(this.Model, wrapper.Model);
        }

        protected WrapperBase(T model, IDictionary<IBaseEntity, object> existsWrappers)
        {
            if (model == null) throw new ArgumentNullException(nameof(Model));

            _existsWrappers = existsWrappers;
            _existsWrappers.Add(model, this);

            Model = model;

            InitializeComplexProperties(model);
            InitializeCollectionComplexProperties(model);
            InitializeCollectionSimpleProperties(model);

            Validate();

            RunInConstructor();
        }

        /// <summary>
        /// Инициализация свойств-коллекций объекта.
        /// </summary>
        /// <param name="model"></param>
        protected virtual void InitializeCollectionComplexProperties(T model) { }

        protected virtual void InitializeCollectionSimpleProperties(T model) { }

        /// <summary>
        /// Инициализация свойств сложных (не примитивных) типов.
        /// </summary>
        /// <param name="model"></param>
        protected virtual void InitializeComplexProperties(T model) { }

        public List<string> ProcessesInWork { get; } = new List<string>();

        /// <summary>
        /// Произошли ли изменения каких-либо свойств объекта.
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (ProcessesInWork.Contains(nameof(IsChanged)))
                    return false;

                ProcessesInWork.Add(nameof(IsChanged));

                if (_originalValues.Count > 0)
                {
                    ProcessesInWork.Remove(nameof(IsChanged));
                    return true;
                }

                bool result =
                    _trackingObjects.Where(x => !x.ProcessesInWork.Contains(nameof(IsChanged))).Any(x => x.IsChanged);

                ProcessesInWork.Remove(nameof(IsChanged));
                return result;
            }
        }

        //public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(x => x.IsChanged);

        /// <summary>
        /// Все ли свойства валидны.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (ProcessesInWork.Contains(nameof(IsValid)))
                    return true;

                ProcessesInWork.Add(nameof(IsValid));

                if (HasErrors)
                {
                    ProcessesInWork.Remove(nameof(IsValid));
                    return false;
                }

                bool result =
                    _trackingObjects.Where(x => !x.ProcessesInWork.Contains(nameof(IsValid))).All(x => x.IsValid);

                ProcessesInWork.Remove(nameof(IsValid));
                return result;
            }
        }

        //public bool IsValid => !HasErrors && _trackingObjects.All(x => x.IsValid);

        /// <summary>
        /// Принять изменения объекта.
        /// </summary>
        public void AcceptChanges()
        {
            if (ProcessesInWork.Contains(nameof(AcceptChanges)))
                return;

            ProcessesInWork.Add(nameof(AcceptChanges));

            //очищаем список начальных значений
            _originalValues.Clear();
            //принимаем изменения в сложных свойствах.
            _trackingObjects.Where(x => !x.ProcessesInWork.Contains(nameof(AcceptChanges)))
                .ToList().ForEach(x => x.AcceptChanges());
            //обновляем в WPF весь объект целиком.
            OnPropertyChanged(this, "");

            ProcessesInWork.Remove(nameof(AcceptChanges));
        }

        /// <summary>
        /// Откатить изменения объекта.
        /// </summary>
        public void RejectChanges()
        {
            if (ProcessesInWork.Contains(nameof(RejectChanges)))
                return;

            ProcessesInWork.Add(nameof(RejectChanges));

            //устанавливаем в каждое измененное свойство начальное значение.
            foreach (var originalValue in _originalValues)
            {
                Model.GetType().GetProperty(originalValue.Key).SetValue(Model, originalValue.Value); //reject in Model

                var originalValueInWrapper = this.GetType().GetProperty(originalValue.Key + "OriginalValue").GetValue(this); //reject in Wrapper
                this.GetType().GetProperty(originalValue.Key).SetValue(this, originalValueInWrapper); 
            }

            //очищаем список начальных значений
            _originalValues.Clear();

            //откатываем изменения в сложных свойствах.
            _trackingObjects.Where(x => !x.ProcessesInWork.Contains(nameof(RejectChanges)))
                .ToList().ForEach(x => x.RejectChanges());

            //проверка на валидность объекта.
            Validate();

            //обновляем в WPF весь объект целиком.
            OnPropertyChanged(this, "");

            ProcessesInWork.Remove(nameof(RejectChanges));
        }

        /// <summary>
        /// Текущее значение свойства модели.
        /// </summary>
        /// <typeparam name="TValue">Тип свойства.</typeparam>
        /// <param name="propertyName">Имя свойства</param>
        /// <returns></returns>
        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue) Model.GetType().GetProperty(propertyName).GetValue(Model);
        }

        /// <summary>
        /// Оригинальное (начальное) значение свойства модели.
        /// </summary>
        /// <typeparam name="TValue">Тип свойства.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns></returns>
        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName)
                ? (TValue) _originalValues[propertyName]
                : GetValue<TValue>(propertyName);
        }

        /// <summary>
        /// Поменялось ли свойство модели.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns></returns>
        protected bool GetIsChanged(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName);
        }

        /// <summary>
        /// Установка нового значения свойства.
        /// </summary>
        /// <typeparam name="TValue">Тип свойства.</typeparam>
        /// <param name="newValue">Новое значение свойства.</param>
        /// <param name="propertyName">Имя свойства.</param>
        protected void SetValue<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            PropertyInfo propertyInfo = Model.GetType().GetProperty(propertyName);
            object currentValue = propertyInfo.GetValue(Model); //текущее значение свойства
            if (!Equals(newValue, currentValue))
            {
                UpdateOriginalValue(propertyName, currentValue, newValue); //обновляем список оригинальных значений.
                propertyInfo.SetValue(Model, newValue); //устанавливаем в свойство модели новое значение.

                Validate();
                OnPropertyChanged(this, propertyName);
                OnPropertyChanged(this, propertyName + "IsChanged");
                OnPropertyChanged(this, nameof(IsChanged));
            }
        }

        /// <summary>
        /// Обновить (добавить или удалить) оригинальное (начальное) значение свойства в списке оригинальных значений.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <param name="originalValue">Оригинальное значение.</param>
        /// <param name="newValue">Новое значение.</param>
        private void UpdateOriginalValue(string propertyName, object originalValue, object newValue)
        {
            //Если оригинальное значение уже менялось (содержится в списке измененных значений).
            if (_originalValues.ContainsKey(propertyName))
            {
                //Если оригинальное (начальное) значение свойства совпадает с новым (присваеваемым).
                if (Equals(_originalValues[propertyName], newValue))
                {
                    //удаляем значение свойства из списка измененных значений.
                    _originalValues.Remove(propertyName);
                    OnPropertyChanged(this, nameof(IsChanged));
                }
            }
            else
            {
                //добавляем значение свойства в список измененных значений.
                _originalValues.Add(propertyName, originalValue);
                OnPropertyChanged(this, nameof(IsChanged));
            }
        }

        /// <summary>
        /// Валидация всех свойств объекта.
        /// </summary>
        private void Validate()
        {
            ClearErrors();

            ValidationContext context = new ValidationContext(this); //контекст поиска ошибок
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true); //класс ищущий ошибки по специальным атрибутам.
            //если валидатор нашел ошибки.
            if (results.Any())
            {
                //список имен свойств, содержащих ошибки
                List<string> propertyNames = results.SelectMany(x => x.MemberNames).Distinct().ToList();

                foreach (string propertyName in propertyNames)
                {
                    var errors =
                        results.Where(x => x.MemberNames.Contains(propertyName))
                            .Select(x => x.ErrorMessage)
                            .Distinct()
                            .ToList();
                    Errors.Add(propertyName, errors);
                    OnErrorsChanged(propertyName); //возбуждаем событие изменения ошибок в свойстве.
                }
            }
            OnPropertyChanged(this, nameof(IsValid));
        }

        /// <summary>
        /// Отмена регистрации сложного (не примитивного) свойства. В нем необходимо отслеживать изменения.
        /// </summary>
        /// <typeparam name="TModel">Тип модели.</typeparam>
        /// <param name="wrapper">Обертка.</param>
        protected void UnRegisterComplexProperty<TModel>(IWrapper<TModel> wrapper)
            where TModel : class, IBaseEntity
        {
            if (wrapper == null) return;
            if (_complexProperties.ContainsKey(wrapper.Model)) _complexProperties.Remove(wrapper.Model);
            UnRegisterTrackingObject(wrapper);
        }

        /// <summary>
        /// Регистрация сложного (не примитивного) свойства. В нем необходимо отслеживать изменения.
        /// </summary>
        /// <typeparam name="TModel">Тип модели.</typeparam>
        /// <param name="wrapper">Обертка.</param>
        protected void RegisterComplexProperty<TModel>(IWrapper<TModel> wrapper)
            where TModel : class, IBaseEntity
        {
            if (wrapper == null) return;
            if (!_complexProperties.ContainsKey(wrapper.Model)) _complexProperties.Add(wrapper.Model, wrapper);
            RegisterTrackingObject(wrapper);
        }

        /// <summary>
        /// Удаление трекинг-объекта из реестра отслеживания в нем изменений.
        /// </summary>
        /// <param name="trackingObject"></param>
        private void UnRegisterTrackingObject(IValidatableChangeTracking trackingObject)
        {
            //если объект зарегистрирован.
            if (_trackingObjects.Contains(trackingObject))
            {
                //изымаем его из списока.
                _trackingObjects.Remove(trackingObject);
                //отписываемся от события изменений его свойств
                trackingObject.PropertyChanged -= TrackingObjectOnPropertyChanged;
            }
        }

        /// <summary>
        /// Регистрация объекта для отслеживания в нем изменений.
        /// </summary>
        /// <param name="trackingObject"></param>
        private void RegisterTrackingObject(IValidatableChangeTracking trackingObject)
        {
            //если объект еще не зарегистрирован.
            if (!_trackingObjects.Contains(trackingObject))
            {
                //добавляем его в список.
                _trackingObjects.Add(trackingObject);
                //подписываемся на событие изменений его свойств
                trackingObject.PropertyChanged += TrackingObjectOnPropertyChanged;
            }
        }

        /// <summary>
        /// реакция на изменение в трекинг-свойстве.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        private void TrackingObjectOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(IsChanged))
                OnPropertyChanged(sender, nameof(IsChanged));
            if (propertyChangedEventArgs.PropertyName == nameof(IsValid)) OnPropertyChanged(sender, nameof(IsValid));
        }


        /// <summary>
        /// Регистрация коллекции.
        /// </summary>
        /// <typeparam name="TWrapper"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="wrapperCollection">коллекция обертки.</param>
        /// <param name="modelCollection">коллекция модели.</param>
        protected void RegisterCollection<TWrapper, TModel>(
            IValidatableChangeTrackingCollection<TWrapper> wrapperCollection, ICollection<TModel> modelCollection)
            where TWrapper : WrapperBase<TModel>
            where TModel : BaseEntity
        {
            //синхронизируем коллекцию модели с коллекцией обертки.
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();

                foreach (TModel modelItem in wrapperCollection.Select(x => x.Model))
                    modelCollection.Add(modelItem);

                Validate();
            };

            RegisterTrackingObject(wrapperCollection);
        }

        /// <summary>
        /// Для валидации не по атрибутам, а в классе.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }


        /// <summary>
        /// Запустить в конструкторе.
        /// </summary>
        protected virtual void RunInConstructor()
        {
        }

        public override string ToString()
        {
            return Model.ToString();
        }

        //public override bool Equals(object obj)
        //{
        //    WrapperBase<T> other = obj as WrapperBase<T>;
        //    return other != null && Model.Equals(other.Model);
        //}

        private readonly Dictionary<IBaseEntity, object> _complexProperties = new Dictionary<IBaseEntity, object>();

        protected TProp GetComplexProperty<TProp, TModel>(TModel model)
            where TProp : class, IWrapper<TModel>
            where TModel : class, IBaseEntity
        {
            if (model != null && _complexProperties.ContainsKey(model))
                return (TProp)_complexProperties[model];
            return null;
        }

        protected void SetComplexProperty<TProp, TModel>(TProp oldValue, TProp newValue, [CallerMemberName] string propertyName = null)
            where TProp : class, IWrapper<TModel>
            where TModel : class, IBaseEntity
        {
            if (Equals(oldValue, newValue)) return;

            UnRegisterComplexProperty(oldValue);
            RegisterComplexProperty(newValue);
            SetValue(newValue?.Model, propertyName);
            OnComplexPropertyChanged(oldValue, newValue, propertyName); //событие изменения комплексного свойства

            //обновление оригинального значения комплексного свойства
            if (Equals(newValue?.Model, GetOriginalValue<TModel>(propertyName)))
                this.GetType().GetProperty(propertyName + "OriginalValue").SetValue(this, newValue);
        }


        protected TWrapper GetWrapper<TWrapper, TModel>(TModel model)
            where TWrapper : class, IWrapper<TModel>
            where TModel : class, IBaseEntity
        {
            if (model == null)
                return null;

            if (this._existsWrappers.ContainsKey(model))
                return (TWrapper)this._existsWrappers[model];

            return (TWrapper) Activator.CreateInstance(typeof(TWrapper), model, this._existsWrappers);
        }
    }

    public interface IWrapper<TModel> : IValidatableChangeTracking, IValidatableObject
    where TModel : class, IBaseEntity
    {
        TModel Model { get; }
        bool EqualsModels(IWrapper<TModel> wrapper);
    }
}
