using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public abstract class WrapperBase<TModel> : NotifyDataErrorInfoBase, IWrapper<TModel>
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
            if (model == null) throw new ArgumentNullException(nameof(Model));
            Model = model;

            InitializeComplexProperties();
            InitializeCollectionProperties();

            Validate();

            RunInConstructor();
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

        #endregion

        /// <summary>
        /// Произошли ли изменения каких-либо свойств объекта.
        /// </summary>
        public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(x => x.IsChanged);

        /// <summary>
        /// Все ли свойства валидны.
        /// </summary>
        public bool IsValid => !HasErrors && _trackingObjects.All(x => x.IsValid);

        /// <summary>
        /// Принять изменения объекта.
        /// </summary>
        public void AcceptChanges()
        {
            //очищаем список начальных значений
            _originalValues.Clear();
            //принимаем изменения в сложных свойствах.
            _trackingObjects.ForEach(x => x.AcceptChanges());
            AcceptWrappers();
            //обновляем в WPF весь объект целиком.
            OnPropertyChanged("");
        }

        /// <summary>
        /// Откатить изменения объекта.
        /// </summary>
        public void RejectChanges()
        {
            //устанавливаем в каждое измененное свойство начальное значение в модели.
            foreach (var originalValue in _originalValues)
                Model.GetType().GetProperty(originalValue.Key).SetValue(Model, originalValue.Value); //reject in Model

            //очищаем список начальных значений
            _originalValues.Clear();

            //откатываем изменения в сложных свойствах.
            _trackingObjects.ForEach(x => x.RejectChanges());

            RejectWrappers();

            //проверка на валидность объекта.
            Validate();

            //обновляем в WPF весь объект целиком.
            OnPropertyChanged("");
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
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName + "IsChanged");
                OnPropertyChanged(nameof(IsChanged));
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
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName + "IsChanged");
                OnPropertyChanged(nameof(IsChanged));
            }
        }

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
                    OnPropertyChanged(nameof(IsChanged));
                }
            }
            else
            {
                //добавляем значение свойства в список измененных значений.
                _originalValues.Add(propertyName, originalValue);
                OnPropertyChanged(nameof(IsChanged));
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
                    OnPropertyChanged(nameof(IsChanged));
                }
            }
            else
            {
                //добавляем значение свойства в список измененных значений.
                _originalValues.Add(propertyName, originalValue);
                OnPropertyChanged(nameof(IsChanged));
            }
        }

        protected void RegisterComplex<TModelWrapper>(IWrapper<TModelWrapper> wrapper) 
            where TModelWrapper : class, IBaseEntity
        {
            RegisterTrackingObject(wrapper);
        }

        protected void RegisterCollection<TWrapperCollection, TModelOfItem>(
            IValidatableChangeTrackingCollection<TWrapperCollection> wrapperCollection, 
            List<TModelOfItem> modelCollection) 

            where TModelOfItem : class, IBaseEntity
            where TWrapperCollection : WrapperBase<TModelOfItem>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(w => w.Model));
                Validate();
            };
            RegisterTrackingObject(wrapperCollection);
        }


        /// <summary>
        /// Валидация всех свойств объекта.
        /// </summary>
        private void Validate()
        {
            ClearErrors();

            var context = new ValidationContext(this); //контекст поиска ошибок
            var results = new List<ValidationResult>();
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
            OnPropertyChanged(nameof(IsValid));
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
            if (propertyChangedEventArgs.PropertyName == nameof(IsChanged)) OnPropertyChanged(nameof(IsChanged));
            if (propertyChangedEventArgs.PropertyName == nameof(IsValid)) OnPropertyChanged(nameof(IsValid));
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
        protected virtual void RunInConstructor() { }

        public override string ToString()
        {
            return Model.ToString();
        }

        public void Refresh()
        {
            OnPropertyChanged(string.Empty);
        }

        public string DisplayMember => ToString();
    }
}
