using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    class EquipmentsComparer : IEqualityComparer<Equipment>
    {
        public bool Equals(Equipment x, Equipment y)
        {
            return Equals(x.Product, y.Product) &&
                   x.DependentEquipments.Except(y.DependentEquipments, new EquipmentsComparer()).Any() &&
                   y.DependentEquipments.Except(x.DependentEquipments, new EquipmentsComparer()).Any();
        }

        public int GetHashCode(Equipment obj)
        {
            throw new NotImplementedException();
        }
    }
}