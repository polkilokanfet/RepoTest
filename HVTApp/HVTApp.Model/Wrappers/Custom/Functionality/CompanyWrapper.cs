using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class CompanyWrapper
    {
        protected override void RunInConstructor()
        {
            this.ComplexPropertyChanged += OnParentCompanyChanged;
            this.ChildCompanies.CollectionChanged += ChildCompaniesOnCollectionChanged;
        }

        /// <summary>
        /// Реакция на событие изменения головной компании
        /// </summary>
        /// <param name="obj"></param>
        private void OnParentCompanyChanged(ComplexPropertyChangedEventArgs obj)
        {
            if (obj.PropertyName != nameof(ParentCompany)) return;

            CompanyWrapper oldParent = obj.OldValue as CompanyWrapper;
            if (oldParent != null && oldParent.ChildCompanies.Contains(this))
                oldParent.ChildCompanies.Remove(this);

            CompanyWrapper newParent = obj.NewValue as CompanyWrapper;
            if (newParent != null && !newParent.ChildCompanies.Contains(this))
                newParent.ChildCompanies.Add(this);
        }


        /// <summary>
        /// Реакция на событие изменения коллекции дочерних компаний
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="notifyCollectionChangedEventArgs"></param>
        private void ChildCompaniesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var newChilds = notifyCollectionChangedEventArgs.NewItems;

            if (newChilds != null)
            {
                foreach (var child in newChilds)
                {
                    if (!Equals(((CompanyWrapper)child).ParentCompany, this))
                        ((CompanyWrapper)child).ParentCompany = this;
                }
            }


            var oldChilds = notifyCollectionChangedEventArgs.OldItems;

            if (oldChilds != null)
            {
                foreach (var child in oldChilds)
                {
                    ((CompanyWrapper) child).ParentCompany = null;
                }
            }
        }

        public IEnumerable<CompanyWrapper> GetAllParents()
        {
            CompanyWrapper parentCompany = this.ParentCompany;
            while (parentCompany != null)
            {
                yield return parentCompany;
                parentCompany = parentCompany.ParentCompany;
            }
        }

        public IEnumerable<CompanyWrapper> GetAllChilds()
        {
            List<CompanyWrapper> childs = this.ChildCompanies.ToList();
            List<CompanyWrapper> result = new List<CompanyWrapper>(childs);

            foreach (var child in childs)
                result.AddRange(child.GetAllChilds());

            return result;
        }
    }
}