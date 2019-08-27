using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;

namespace HVTApp.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel
    {

        #region AfterSaveEvents

        private void AfterSaveProjectEventExecute(Project project)
        {
            var projectLookup = Projects.GetById(project);

            //если необходимо обновить существующий проект
            if (projectLookup != null)
            {
                projectLookup.Refresh(project);

                if (ShownAllProjects || projectLookup.InWork)
                {
                    LoadGroups(projectLookup);
                }
                else
                {
                    //удяляем нерабочие проекты
                    if (Projects.ProjectsToShow.Contains(projectLookup))
                    {
                        Projects.ProjectsToShow.Remove(projectLookup);
                    }
                }
            }
            else
            {
                var lookup = new ProjectLookup(project);
                Projects.Add(lookup);

                if (ShownAllProjects || lookup.InWork)
                    Projects.ProjectsToShow.Add(lookup);
            }
        }

        private void AfterSaveTenderEventExecute(Tender tender)
        {
            var tenders = Projects.SelectMany(x => x.Tenders).ToList();
            //если необходимо обновить существующий тендер
            if (tenders.ContainsById(tender))
            {
                tenders.GetById(tender)?.Refresh(tender);
            }
            //если необходимо добавть созданный тендер
            else
            {
                Projects.GetById(tender.Project)?.Tenders.Add(new TenderLookup(tender));
            }

            //обновляем проект, содержащий тендер
            Projects.SingleOrDefault(x => x.Tenders.ContainsById(tender))?.Refresh();
        }

        private void AfterSaveOfferEventExecute(Offer offer)
        {
            var offers = Projects.SelectMany(x => x.Offers).ToList();

            //если необходимо обновить существующее ТКП
            if (offers.ContainsById(offer))
            {
                offers.SingleOrDefault(x => x.Id == offer.Id)?.Refresh(offer);
                return;
            }

            //если необходимо добавить созданное ТКП
            var lookupNew = new OfferLookup(offer);
            //добавляет ТКП в проект
            Projects.GetById(offer.Project)?.Offers.Add(lookupNew);
            //добавляет ТКП в список ТКП
            if (offer.Project.Id == SelectedProjectLookup?.Id) Offers.Add(lookupNew);
            //обновление
            lookupNew.Refresh();
        }

        private void AfterSaveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            //целевой проект
            var project = Projects.GetById(salesUnit.Project);

            //костыль - нужно добавить предварительно проект
            if (project == null) return;

            //обновляем или добавляем
            if (project.SalesUnits.ContainsById(salesUnit))
            {
                project.SalesUnits.GetById(salesUnit).Refresh(salesUnit);
            }
            else
            {
                project.SalesUnits.Add(new SalesUnitLookup(salesUnit));
            }

            //обновляем целевой проект
            project.Refresh();

            //обновляем отображение оборудования
            if (Equals(project, SelectedProjectLookup))
            {
                LoadGroups(SelectedProjectLookup);
            }
        }

        private void AfterSaveOfferUnitEventExecute(OfferUnit offerUnit)
        {
            //целевое ТКП
            var offer = Projects.SelectMany(x => x.Offers).GetById(offerUnit.Offer);

            //костыль - нужно добавить предварительно проект
            if (offer == null) return;

            //обновляем или добавляем
            if (offer.OfferUnits.ContainsById(offerUnit))
            {
                offer.OfferUnits.GetById(offerUnit)?.Refresh(offerUnit);
            }
            else
            {
                offer.OfferUnits.Add(new OfferUnitLookup(offerUnit));
            }

            //обновляем целевое ТКП
            offer.Refresh();
        }



        #endregion

        #region AfterRemoveEvent

        private void AfterRemoveOfferUnitEventExecute(OfferUnit offerUnit)
        {
            var offer = Projects.SelectMany(x => x.Offers).GetById(offerUnit.Offer);
            if (offer == null) return;
            var lookup = offer.OfferUnits.GetById(offerUnit);
            offer.OfferUnits.Remove(lookup);
            offer.Refresh();
        }

        private void AfterRemoveSalesUnitEventExecute(SalesUnit salesUnit)
        {
            var project = Projects.GetById(salesUnit.Project);
            if (project == null) return;
            var lookup = project.SalesUnits.GetById(salesUnit);
            project.SalesUnits.Remove(lookup);
            project.Refresh();
        }

        private void AfterRemoveProjectEventExecute(Project project)
        {
        }

        private void AfterRemoveTenderEventExecute(Tender tender)
        {
            var project = Projects.SingleOrDefault(x => x.Tenders.ContainsById(tender));
            if (project == null) return;
            var lookup = project.Tenders.GetById(tender);
            project.Tenders.Remove(lookup);
            lookup.Refresh();
        }

        private void AfterRemoveOfferEventExecute(Offer offer)
        {
            //проект, содержащий удаленное ТКП
            var project = Projects.SingleOrDefault(x => x.Offers.Select(t => t.Id).Contains(offer.Id));
            if (project == null) return;

            //удаляем ТКП из проекта
            var lookup = project.Offers.Single(x => x.Id == offer.Id);
            project.Offers.Remove(lookup);
            //удаляем из списка ТКП
            if (Offers.Contains(lookup)) Offers.Remove(lookup);
        }

        #endregion

    }
}
