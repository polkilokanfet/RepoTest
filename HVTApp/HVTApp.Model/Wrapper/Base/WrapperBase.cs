using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper.Base
{
    public abstract partial class WrapperBase<TModel> : NotifyDataErrorInfoBase, IWrapper<TModel>
        where TModel : class, IBaseEntity
    {
        protected readonly Dictionary<string, IWrapper<IBaseEntity>> OriginalWrappers = new Dictionary<string, IWrapper<IBaseEntity>>();
        protected readonly Dictionary<string, IWrapper<IBaseEntity>> CurrentWrappers = new Dictionary<string, IWrapper<IBaseEntity>>();

        // Словарь оригинальных значений. В словарь вносятся только те оригинальные значения, которые были изменены.
        private readonly Dictionary<string, object> _originalValues = new Dictionary<string, object>();

        // Список объектов в которых отслеживаются изменения (свойства объекта не примитивного типа и коллекции).
        private readonly List<IValidatableChangeTracking> _trackingObjects = new List<IValidatableChangeTracking>();

        // Объект, обертка которого создана в этом классе.
        public TModel Model { get; }

        protected WrapperBase(TModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(Model));

            InitializeComplexProperties();
            InitializeCollectionProperties();

            InitializeOther();

            Validate();
        }

        #region InitializeProperties

        /// <summary>
        /// Инициализация свойств-коллекций объекта.
        /// </summary>
        protected virtual void InitializeCollectionProperties() { }

        /// <summary>
        /// Инициализация свойств сложных (не примитивных) типов.
        /// </summary>
        public virtual void InitializeComplexProperties() { }

        public virtual void InitializeOther() { }

        #endregion

        /// <summary>
        /// Произошли ли изменения каких-либо свойств объекта.
        /// </summary>
        public virtual bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(x => x.IsChanged);

        /// <summary>
        /// Все ли свойства валидны.
        /// </summary>
        public virtual bool IsValid => !HasErrors && _trackingObjects.All(x => x.IsValid);

        /// <summary>
        /// Принять изменения объекта.
        /// </summary>
        public virtual void AcceptChanges()
        {
            foreach (var originalValue in _originalValues)
            {
                PropertyChangeAccepted?.Invoke(this.Model, originalValue.Key);
            }
            //очищаем список начальных значений
            _originalValues.Clear();
            //принимаем изменения в сложных свойствах.
            _trackingObjects.ForEach(x => x.AcceptChanges());
            AcceptWrappers();
            //обновляем в WPF весь объект целиком.
            RaisePropertyChanged("");
        }

        /// <summary>
        /// Принято изменение свойства
        /// </summary>
        public event Action<TModel, string> PropertyChangeAccepted;

        /// <summary>
        /// Откатить изменения объекта.
        /// </summary>
        public void RejectChanges()
        {
            //устанавливаем в каждое измененное свойство начальное значение в модели.
            foreach (var originalValue in _originalValues)
                Model.GetType().GetProperty(originalValue.Key).SetValue(Model, originalValue.Value); //reject in Entity

            //очищаем список начальных значений
            _originalValues.Clear();

            //откатываем изменения в сложных свойствах.
            _trackingObjects.ForEach(x => x.RejectChanges());

            RejectWrappers();

            //проверка на валидность объекта.
            Validate();

            //обновляем в WPF весь объект целиком.
            RaisePropertyChanged("");
        }

        /// <summary>
        /// Текущее значение свойства модели.
        /// </summary>
        /// <typeparam name="TValue">Тип свойства.</typeparam>
        /// <param name="propertyName">Имя свойства</param>
        /// <returns></returns>
        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
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
                RaisePropertyChanged(propertyName);
                RaisePropertyChanged(propertyName + "IsChanged");
                RaisePropertyChanged(nameof(IsChanged));
            }
        }

        private void AcceptWrappers()
        {
            foreach (var currentWrapper in CurrentWrappers)
            {
                OriginalWrappers[currentWrapper.Key] = currentWrapper.Value;
            }
        }

        private void RejectWrappers()
        {
            foreach (var originalWrapper in OriginalWrappers)
            {
                CurrentWrappers[originalWrapper.Key] = originalWrapper.Value;
            }
        }

        protected TWrapper GetWrapper<TWrapper>([CallerMemberName] string propertyName = null)
            where TWrapper : IWrapper<IBaseEntity>
        {
            return (TWrapper) CurrentWrappers[propertyName];
        }

        protected void SetComplexValue<TModelValue, TWrapperValue>(TWrapperValue currentValue, TWrapperValue newValue, [CallerMemberName] string propertyName = null)
            where TModelValue : class, IBaseEntity
            where TWrapperValue : IWrapper<TModelValue>
        {
            PropertyInfo propertyInfo = Model.GetType().GetProperty(propertyName);
            if (!Equals(newValue?.Model.Id, currentValue?.Model.Id))
            {
                UpdateOriginalValue(propertyName, currentValue?.Model, newValue?.Model); //обновляем список оригинальных значений.
                propertyInfo.SetValue(Model, newValue?.Model); //устанавливаем в свойство модели новое значение.

                UnRegisterComplex(currentValue);

                if (newValue != null)
                    RegisterComplex(newValue);

                CurrentWrappers[propertyName] = newValue;

                Validate();
                RaisePropertyChanged(propertyName);
                RaisePropertyChanged(propertyName + "IsChanged");
                RaisePropertyChanged(nameof(IsChanged));
            }
        }

        /// <summary>
        /// Инициализация комплексного свойства.
        /// </summary>
        /// <typeparam name="TWrapper"></typeparam>
        /// <param name="propertyName"> Имя свойства </param>
        /// <param name="wrapper"></param>
        protected void InitializeComplexProperty<TWrapper>(string propertyName, TWrapper wrapper)
            where TWrapper : IWrapper<IBaseEntity>
        {
            OriginalWrappers.Add(propertyName, null);
            CurrentWrappers.Add(propertyName, null);
            if (wrapper != null)
            {
                OriginalWrappers[propertyName] = wrapper;
                CurrentWrappers[propertyName] = OriginalWrappers[propertyName];
                RegisterComplex(wrapper);
            }
        }


        protected void UnRegisterComplex<TModelWrapper>(IWrapper<TModelWrapper> wrapper) 
            where TModelWrapper : class, IBaseEntity
        {
            if (wrapper == null || !_trackingObjects.Contains(wrapper)) return;

            _trackingObjects.Remove(wrapper);
            wrapper.PropertyChanged -= TrackingObjectOnPropertyChanged;
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
                    RaisePropertyChanged(nameof(IsChanged));
                }
            }
            else
            {
                //добавляем значение свойства в список измененных значений.
                _originalValues.Add(propertyName, originalValue);
                RaisePropertyChanged(nameof(IsChanged));
            }
        }

        private void UpdateOriginalValueComplexProp(string propertyName, object originalValue, object newValue)
        {
            //Если оригинальное значение уже менялось (содержится в списке измененных значений).
            if (_originalValues.ContainsKey(propertyName))
            {
                //Если оригинальное (начальное) значение свойства совпадает с новым (присваеваемым).
                if (Equals(_originalValues[propertyName], newValue))
                {
                    //удаляем значение свойства из списка измененных значений.
                    _originalValues.Remove(propertyName);
                    RaisePropertyChanged(nameof(IsChanged));
                }
            }
            else
            {
                //добавляем значение свойства в список измененных значений.
                _originalValues.Add(propertyName, originalValue);
                RaisePropertyChanged(nameof(IsChanged));
            }
        }

        protected void RegisterComplex<TModelWrapper>(IWrapper<TModelWrapper> wrapper) 
            where TModelWrapper : class, IBaseEntity
        {
            RegisterTrackingObject(wrapper);
        }

        /// <summary>
        /// Регистрация коллекции и синхронизация ее с моделью.
        /// </summary>
        /// <typeparam name="TWrapperCollection">Тип обертки</typeparam>
        /// <typeparam name="TModelOfItem">Тип модели</typeparam>
        /// <param name="wrapperCollection">Коллекция оберток</param>
        /// <param name="modelCollection">Коллекция моделей</param>
        protected void RegisterCollection<TWrapperCollection, TModelOfItem>(
            IValidatableChangeTrackingCollection<TWrapperCollection> wrapperCollection,
            List<TModelOfItem> modelCollection)

            where TModelOfItem : class, IBaseEntity
            where TWrapperCollection : WrapperBase<TModelOfItem>
        {
            //реакция на изменение состава членов коллекции
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(w => w.Model));
                Validate();
            };

            //реакция на изменение в каком-либо члене коллекции
            wrapperCollection.PropertyChanged += (sender, args) =>
            {
                Validate();
            };

            RegisterTrackingObject(wrapperCollection);
        }

        protected void RegisterCollectionWithoutSynch<TWrapperCollection>(IValidatableChangeTrackingCollection<TWrapperCollection> wrapperCollection)
            where TWrapperCollection : IWrapper<IBaseEntity>
        {
            wrapperCollection.CollectionChanged += (s, e) => { Validate(); };
            RegisterTrackingObject(wrapperCollection);
        }


        /// <summary>
        /// Валидация всех свойств объекта.
        /// </summary>
        protected void Validate()
        {
            Errors.Reboot();

            var context = new ValidationContext(this); //контекст поиска ошибок
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true); //класс ищущий ошибки по специальным атрибутам.
            
            //если валидатор нашел ошибки.
            foreach (var validationResult in results)
            {
                foreach (var propertyName in validationResult.MemberNames)
                {
                    Errors.Add(new DataErrorInfo(propertyName, validationResult.ErrorMessage));
                }
            }

            if (Errors.IsChanged)
            {
                foreach (var propertyName in Errors.ChangedErrors.Select(x => x.PropertyName).Distinct())
                {
                    OnErrorsChanged(propertyName);
                }
            }

            //if (results.Any())
            //{
            //    //список имен свойств, содержащих ошибки
            //    var propertyNames = results
            //        .SelectMany(validationResult => validationResult.MemberNames)
            //        .Distinct()
            //        .ToList();

            //    foreach (string propertyName in propertyNames)
            //    {
            //        var errors =
            //            results.Where(validationResult => validationResult.MemberNames.Contains(propertyName))
            //                   .Select(validationResult => validationResult.ErrorMessage)
            //                   .Distinct()
            //                   .ToList();
            //        Errors.Add(propertyName, errors);
            //        OnErrorsChanged(propertyName); //возбуждаем событие изменения ошибок в свойстве.
            //    }
            //}
            RaisePropertyChanged(nameof(IsValid));
        }

        /// <summary>
        /// Регистрация объекта для отслеживания в нем изменений.
        /// </summary>
        /// <param name="trackingObject"></param>
        private void RegisterTrackingObject(IValidatableChangeTracking trackingObject)
        {
            //если объект еще не зарегистрирован.
            if (_trackingObjects.Contains(trackingObject)) return;
            //добавляем его в список.
            _trackingObjects.Add(trackingObject);
            //подписываемся на событие изменений его свойств
            trackingObject.PropertyChanged += TrackingObjectOnPropertyChanged;
        }

        /// <summary>
        /// Реакция на изменение в трекинг-свойстве.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        private void TrackingObjectOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(IsChanged)) RaisePropertyChanged(nameof(IsChanged));
            if (propertyChangedEventArgs.PropertyName == nameof(IsValid)) RaisePropertyChanged(nameof(IsValid));
        }

        /// <summary>
        /// Валидация по атрибутам.
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();

            //тип модели
            var modelType = Model.GetType();
            //все её свойства
            var props = modelType.GetProperties();
            foreach (var prop in props)
            {
                //обязательные поля
                var reqAttr = prop.GetCustomAttribute<RequiredAttribute>();
                if (reqAttr != null || (prop.PropertyType == typeof(string) && (string) prop.GetValue(Model) == string.Empty))
                {
                    if (prop.GetValue(Model) == null)
                    {
                        result.Add(new ValidationResult($"\"{prop.Name}\" не может быть пустым.", new[] {prop.Name}));
                    }
                }

                //обязательные коллекции (они не должны быть пусты)
                if (reqAttr != null && prop.PropertyType.IsCollection())
                {
                    if (((ICollection)prop.GetValue(Model)).Count < 1)
                    {
                        result.Add(new ValidationResult($"Список \"{prop.Name}\" не может быть пустым.", new[] { prop.Name }));
                    }
                }


                //поля с ограничением на длину строки
                if (prop.PropertyType == typeof(string))
                {
                    var lenghAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
                    if (lenghAttr != null && prop.GetValue(Model) != null && ((string)prop.GetValue(Model)).Length > lenghAttr.Length)
                    {
                        result.Add(new ValidationResult($"количество символов в поле \"{prop.Name}\" не может превышать {lenghAttr.Length}.", new[] { prop.Name }));
                    }
                }
            }
            
            return result.Concat(ValidateOther());
        }

        protected virtual IEnumerable<ValidationResult> ValidateOther()
        {
            yield break;
        }

        public override string ToString()
        {
            return Model.ToString();
        }

        public void Refresh()
        {
            RaisePropertyChanged(string.Empty);
        }
    }

    public abstract partial class WrapperBase<TModel> : IComparable
    {
        public virtual int CompareTo(object obj)
        {
            return this.ToString().CompareTo(obj.ToString());
        }
    }

}
