using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Infrastructure.Interfaces.Services.SelectService
{
    public interface ISelectService
    {
        /// <summary>
        /// Регистрация типа
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        void Register<TView, TItem>()
            where TView : Control
            where TItem : class, IBaseEntity;

        /// <summary>
        /// Перерегистрация типа
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        void ReRegister<TView, TItem>()
            where TView : Control
            where TItem : class, IBaseEntity;

        /// <summary>
        /// Выбрать айтем
        /// </summary>
        /// <typeparam name="TItem">Тип айтема</typeparam>
        /// <param name="items">Список, из которого нужно выбрать</param>
        /// <param name="selectedItemId">Предварительно выбранный айтем</param>
        /// <returns></returns>
        TItem SelectItem<TItem>(IEnumerable<TItem> items, Guid? selectedItemId = null)
            where TItem : class, IBaseEntity;

        /// <summary>
        /// Выбрать несколько айтемов
        /// </summary>
        /// <typeparam name="TItem">Тип айтемов</typeparam>
        /// <param name="items">Список, из которого нужно выбрать</param>
        /// <returns></returns>
        IEnumerable<TItem> SelectItems<TItem>(IEnumerable<TItem> items)
            where TItem : class, IBaseEntity;
    }

    public interface ISelectServiceViewModel<TItem> : IDialogRequestClose
        where TItem : IBaseEntity
    {
        void Load(IEnumerable<TItem> entities);

        TItem SelectedItem { get; set; }
        ICommand SelectItemCommand { get; }

        IEnumerable<TItem> SelectedItems { get; }
        ICommand SelectItemsCommand { get; }


        ICommand NewItemCommand { get; }
    }
}