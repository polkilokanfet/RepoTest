using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProjectNotesWrapper : WrapperBase<Project>
    {
        public ProjectNotesWrapper(Project model) : base(model) { }

        public IValidatableChangeTrackingCollection<NoteWrapper> Notes { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Notes == null) throw new ArgumentException("Notes cannot be null");
            Notes = new ValidatableChangeTrackingCollection<NoteWrapper>(Model.Notes.Select(e => new NoteWrapper(e)));
            RegisterCollection(Notes, Model.Notes);
        }
    }
}