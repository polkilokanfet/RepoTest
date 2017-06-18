using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
    public abstract class WrapperBase<T> : NotifyDataErrorInfoBase, IWrapper<T>
        where T : class, IBaseEntity
    {
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

        protected WrapperBase(T model)
        {
            if (model == null) throw new ArgumentNullException(nameof(Model));

            Model = model;

            //if (!WrappersFactory.Wrappers.ContainsKey(model))
                WrappersFactory.Wrappers.Add(model, this);

            InitializeComplexProperties();
            InitializeCollectionSimpleProperties();
            InitializeCollectionComplexProperties();

            Validate();

            RunInConstructor();
        }

        #region InitializeProperties

        /// <summary>
        /// Инициализация свойств-коллекций объекта.
        /// </summary>
        protected virtual void InitializeCollectionComplexProperties() { }

        public virtual void InitializeCollectionSimpleProperties() { }

        /// <summary>
        /// Инициализация свойств сложных (не примитивных) типов.
        /// </summary>
        public virtual void InitializeComplexProperties() { }

        #endregion

        public bool IsChangedMethod(IDictionary<IBaseEntity, IValidatableChangeTracking> risedDictionary)
        {
            if (risedDictionary.ContainsKey(this.Model)) return false;
            risedDictionary.Add(this.Model, this);

            var risedWrappers = risedDictionary.Select(x => x.Value);

            bool result = _originalValues.Count > 0 || _trackingObjects.Except(risedWrappers).Any(x => x.IsChangedMethod(risedDictionary));

            return result;
        }


        /// <summary>
        /// Произошли ли изменения каких-либо свойств объекта.
        /// </summary>
        public bool IsChanged => IsChangedMethod(new Dictionary<IBaseEntity, IValidatableChangeTracking>());

        //public bool IsChanged => _originalValues.Count > 0 || _trackingObjects.Any(x => x.IsChanged);

        public bool IsValidMethod(IList<IBaseEntity> risedList)
        {
            if (risedList.Contains(this.Model)) return true;
            risedList.Add(this.Model);

            return !HasErrors && _trackingObjects.All(x => x.IsValidMethod(risedList));
        }


        /// <summary>
        /// Все ли свойства валидны.
        /// </summary>
        public bool IsValid => IsValidMethod(new List<IBaseEntity>());

        //public bool IsValid => !HasErrors && _trackingObjects.All(x => x.IsValid);

        public void AcceptChangesMethod(IList<IBaseEntity> acceptedModels)
        {
            if (acceptedModels.Contains(this.Model)) return;
            acceptedModels.Add(this.Model);

            //очищаем список начальных значений
            _originalValues.Clear();
            //принимаем изменения в сложных свойствах.
            _trackingObjects.ForEach(x => x.AcceptChangesMethod(acceptedModels));
            //обновляем в WPF весь объект целиком.
            OnPropertyChanged(this, "");
        }


        /// <summary>
        /// Принять изменения объекта.
        /// </summary>
        public void AcceptChanges()
        {
            AcceptChangesMethod(new List<IBaseEntity>());
        }

        public void RejectChangesMethod(IList<IBaseEntity> rejectedModels)
        {
            if (rejectedModels.Contains(this.Model)) return;
            rejectedModels.Add(this.Model);

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
            _trackingObjects.ForEach(x => x.RejectChangesMethod(rejectedModels));

            //проверка на валидность объекта.
            Validate();

            //обновляем в WPF весь объект целиком.
            OnPropertyChanged(this, "");
        }
        /// <summary>
        /// Откатить изменения объекта.
        /// </summary>
        public void RejectChanges()
        {
            RejectChangesMethod(new List<IBaseEntity>());
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
            if (propertyChangedEventArgs.PropertyName == nameof(IsChanged)) OnPropertyChanged(sender, nameof(IsChanged));
            if (propertyChangedEventArgs.PropertyName == nameof(IsValid)) OnPropertyChanged(sender, nameof(IsValid));
        }


        /// <summary>
        /// Регистрация коллекции.
        /// </summary>
        /// <typeparam name="TWrapper"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="wrapperCollection">коллекция обертки.</param>
        /// <param name="modelCollection">коллекция модели.</param>
        protected void RegisterCollection<TWrapper, TModel>(IValidatableChangeTrackingCollection<TWrapper> wrapperCollection, ICollection<TModel> modelCollection)
            where TWrapper : class, IWrapper<TModel>
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

        protected TWrapper GetComplexProperty<TWrapper, TModel>(TModel model)
            where TWrapper : class, IWrapper<TModel>
            where TModel : class, IBaseEntity
        {
            if (model == null) return null;

            return GetWrapper<TWrapper, TModel>(model);
        }

        public event Action<ComplexPropertyChangedEventArgs> ComplexPropertyChanged; 

        protected void SetComplexProperty<TWrapper, TModel>(TWrapper oldValue, TWrapper newValue, [CallerMemberName] string propertyName = null)
            where TWrapper : class, IWrapper<TModel>
            where TModel : class, IBaseEntity
        {
            if (Equals(oldValue, newValue) && _trackingObjects.Contains(oldValue)) return;

            if (oldValue != null) UnRegisterTrackingObject(oldValue);
            if (newValue != null) RegisterTrackingObject(newValue);
            SetValue(newValue?.Model, propertyName);
            if (!Equals(oldValue, newValue)) OnComplexPropertyChanged(new ComplexPropertyChangedEventArgs(oldValue, newValue, propertyName));

            //обновление оригинального значения комплексного свойства
            if (Equals(newValue?.Model, GetOriginalValue<TModel>(propertyName)))
                this.GetType().GetProperty(propertyName + "OriginalValue").SetValue(this, newValue);
        }

        protected TWrapper GetWrapper<TWrapper, TModel>(TModel model)
            where TModel : class, IBaseEntity
            where TWrapper : class, IWrapper<TModel>
        {
            if (model == null) return null;

            return WrappersFactory.GetWrapper<TModel, TWrapper>(model);
        }

        protected virtual void OnComplexPropertyChanged(ComplexPropertyChangedEventArgs obj)
        {
            ComplexPropertyChanged?.Invoke(obj);
        }
    }

    public class ComplexPropertyChangedEventArgs : EventArgs
    {
        public object OldValue { get; }
        public object NewValue { get; }
        public string PropertyName { get; }

        public ComplexPropertyChangedEventArgs(object oldValue, object newValue, string propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
            PropertyName = propertyName;
        }
    }
}
