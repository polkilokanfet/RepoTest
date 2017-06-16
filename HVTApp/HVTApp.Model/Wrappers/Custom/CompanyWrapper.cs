using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class CompanyWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += OnParentCompanyChanged1;
            this.ChildCompanies.CollectionChanged += ChildCompaniesOnCollectionChanged;
        }

        private void OnParentCompanyChanged1(object sender, PropertyChangedEventArgs e)
        {
            var company = (CompanyWrapper) sender;
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

        /// <summary>
        /// Реакция на событие изменения головной компании
        /// </summary>
        /// <param name="oldPropVal"></param>
        /// <param name="newPropVal"></param>
        /// <param name="propertyName"></param>
        private void OnParentCompanyChanged(object oldPropVal, object newPropVal, string propertyName)
        {
            if (propertyName != nameof(ParentCompany)) return;

            CompanyWrapper oldParent = oldPropVal as CompanyWrapper;
            if (oldParent != null && oldParent.ChildCompanies.Contains(this))
                oldParent.ChildCompanies.Remove(this);

            CompanyWrapper newParent = newPropVal as CompanyWrapper;
            if (newParent != null && !newParent.ChildCompanies.Contains(this))
                newParent.ChildCompanies.Add(this);
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


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FullName))
                yield return new ValidationResult("FullName is required", new[] { nameof(FullName) });

            if (string.IsNullOrWhiteSpace(ShortName))
                yield return new ValidationResult("ShortName is required", new[] { nameof(ShortName) });

            if (Form == null)
                yield return new ValidationResult("Form is required", new[] { nameof(Form) });

            if (!ActivityFilds.Any())
                yield return new ValidationResult("У компании должна быть хотябы одна сфера деятельности.", new[] {nameof(ActivityFilds)});
        }
    }
}