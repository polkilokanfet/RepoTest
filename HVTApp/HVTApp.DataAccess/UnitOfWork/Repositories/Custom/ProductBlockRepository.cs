using System;
using System.Data.Entity;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProductBlockRepository
    {
        protected override IQueryable<ProductBlock> GetQuary()
        {
            return Context.Set<ProductBlock>().AsQueryable()
                .Include(block => block.Prices)
                .Include(block => block.FixedCosts)
                .Include(block => block.Parameters);
        }

        public override UnitOfWorkOperationResult Add(ProductBlock productBlock)
        {
            if (this.GetById(productBlock.Id) != null)
            {
                return new UnitOfWorkOperationResult(new Exception("ProductBlock � ����� Id ��� ������������ � �����������"));
            }

            if (this.FindAsNoTracking(x => x.Equals(productBlock)).Any())
            {
                return new UnitOfWorkOperationResult(new Exception("ProductBlock � ����� ������� ���������� ��� ������������ � �����������"));
            }

            return base.Add(productBlock);
        }

    }
}