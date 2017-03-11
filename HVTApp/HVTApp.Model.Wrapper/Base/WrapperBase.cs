using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HVTApp.Model.Wrapper
{
    public abstract class WrapperBase<T> : NotifyDataErrorInfoBase, IValidatableChangeTracking, IValidatableObject
        where T : BaseEntity
    {
        protected readonly Dictionary<BaseEntity, object> ExistsWrappers;

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

        protected WrapperBase(T model, Dictionary<BaseEntity, object> existsWrappers = null)
        {
            if (model == null) throw new ArgumentNullException(nameof(Model));

            ExistsWrappers = existsWrappers ?? new Dictionary<BaseEntity, object>();
            if (!ExistsWrappers.ContainsKey(model))
                ExistsWrappers.Add(model, this);

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
            //обновляем в WPF весь объект целиком.
            OnPropertyChanged("");
        }

        /// <summary>
        /// Откатить изменения объекта.
        /// </summary>
        public void RejectChanges()
        {
            //устанавливаем в каждое измененное свойство начальное значение.
            foreach (var originalValue in _originalValues)
            {
                typeof(T).GetProperty(originalValue.Key).SetValue(Model, originalValue.Value);
            }
            //очищаем список начальных значений
            _originalValues.Clear();

            //откатываем изменения в сложных свойствах.
            _trackingObjects.ForEach(x => x.RejectChanges());

            //проверка на валидность объекта.
            Validate();

            //обновляем в WPF весь объект целиком.
            OnPropertyChanged("");
        }

        /// <summary>
        /// Вернуть текущее значение свойства модели.
        /// </summary>
        /// <typeparam name="TValue">Тип свойства.</typeparam>
        /// <param name="propertyName">Имя свойства</param>
        /// <returns></returns>
        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue) Model.GetType().GetProperty(propertyName).GetValue(Model);
        }

        /// <summary>
        /// Вернуть оригинальное (начальное) значение свойства модели.
        /// </summary>
        /// <typeparam name="TValue">Тип свойства.</typeparam>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns></returns>
        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            return _originalValues.ContainsKey(propertyName)
                ? (TValue)_originalValues[propertyName]
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
        /// <typeparam name="TValie">Тип свойства.</typeparam>
        /// <param name="newValue">Новое значение свойства.</param>
        /// <param name="propertyName">Имя свойства.</param>
        protected void SetValue<TValie>(TValie newValue, [CallerMemberName] string propertyName = null)
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
                    var errors = results.Where(x => x.MemberNames.Contains(propertyName)).Select(x => x.ErrorMessage).Distinct().ToList();
                    Errors.Add(propertyName, errors);
                    OnErrorsChanged(propertyName); //возбуждаем событие изменения ошибок в свойстве.
                }
            }
            OnPropertyChanged(nameof(IsValid));
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

        /// <summary>
        /// Регистрация сложного (не примитивного) свойства. В нем необходимо отслеживать изменения.
        /// </summary>
        /// <typeparam name="TModel">Тип модели.</typeparam>
        /// <param name="wrapper">Обертка.</param>
        protected void RegisterComplexProperty<TModel>(WrapperBase<TModel> wrapper)
            where TModel : BaseEntity
        {
            RegisterTrackingObject(wrapper);
        }

        /// <summary>
        /// Регистрация объекта для отслеживания в нем изменений.
        /// </summary>
        /// <param name="trackingObject"></param>
        private void RegisterTrackingObject(IValidatableChangeTracking trackingObject)
        {
            //если объект еще не заригистрирован.
            if (!_trackingObjects.Contains(trackingObject))
            {
                //добавляем его в список.
                _trackingObjects.Add(trackingObject);
                //подписываемся на событие изменений его свойств
                trackingObject.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == nameof(IsChanged)) OnPropertyChanged(nameof(IsChanged));
                    if (e.PropertyName == nameof(IsValid)) OnPropertyChanged(nameof(IsValid));
                };
            }
        }

        /// <summary>
        /// Регистрация Complex свойства-коллекции.
        /// </summary>
        /// <typeparam name="TWrapper"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="wrapperCollection">коллекция обертки.</param>
        /// <param name="modelCollection">коллекция модели.</param>
        protected void RegisterCollection<TWrapper, TModel>(ValidatableChangeTrackingCollection<TWrapper> wrapperCollection, ICollection<TModel> modelCollection)
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
        /// Регистрация любого свойства-коллекции.
        /// </summary>
        /// <typeparam name="TCollectionsMember"></typeparam>
        /// <param name="wrapperCollection">коллекция обертки.</param>
        /// <param name="modelCollection">коллекция модели.</param>
        protected void RegisterCollection<TCollectionsMember>(TrackingCollection<TCollectionsMember> wrapperCollection, ICollection<TCollectionsMember> modelCollection)
        {
            //синхронизируем коллекцию модели с коллекцией обертки.
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                //вычищаем модель
                modelCollection.Clear();

                //добавляем в коллекцию модели все элементы коллекции-обертки
                foreach (TCollectionsMember modelItem in wrapperCollection)
                    modelCollection.Add(modelItem);

                //проверяем на валидность
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
        protected virtual void RunInConstructor() { }

        public override string ToString()
        {
            return Model.ToString();
        }
    }
}
