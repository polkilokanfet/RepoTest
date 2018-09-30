using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Modules.Sales.ViewModels
{
    public interface IGroupsViewModel<TUnit, TParentWrapper>
    {
        void Load(IEnumerable<TUnit> units, TParentWrapper parentWrapper, IUnitOfWork unitOfWork, bool isNew);
        bool IsValid { get; }
        bool IsChanged { get; }
        void AcceptChanges();

        event Action GroupChanged;
    }
}