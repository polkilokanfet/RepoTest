using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Model.Wrappers;

namespace HVTApp.Model
{
    public interface IProduct
    {
        ProductWrapper Product { get; }
    }
}
