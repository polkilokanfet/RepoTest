using System.Collections.Generic;

namespace HVTApp.Infrastructure.Interfaces.Services.ChooseService
{
    public interface IChooseService
    {
        TItem ChooseDialog<TItem>(IEnumerable<TItem> items);
        TItem ChooseDialog<TItem>(IEnumerable<TItem> items, TItem selectedItem);
    }
}