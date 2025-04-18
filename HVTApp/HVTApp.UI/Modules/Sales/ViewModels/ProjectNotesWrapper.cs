using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProjectNotesWrapper : WrapperBase<Project>
    {
        public NotesViewModel NotesViewModel { get; }
        public ProjectNotesWrapper(Project model, NotesViewModel notesViewModel) : base(model)
        {
            NotesViewModel = notesViewModel;
        }

        public IValidatableChangeTrackingCollection<NoteViewModel> Notes { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Notes == null) throw new ArgumentException("Notes cannot be null");
            Notes = new ValidatableChangeTrackingCollection<NoteViewModel>(Model.Notes.Select(e => new NoteViewModel(e, this)));
            RegisterCollection(Notes, Model.Notes);
        }
    }

    public class NoteViewModel : WrapperBase<Note>
    {
        private readonly ProjectNotesWrapper _projectNotesWrapper;

        public DelegateLogCommand RemoveNoteCommand { get; }

        public NoteViewModel(Note model, ProjectNotesWrapper projectNotesWrapper) : base(model)
        {
            _projectNotesWrapper = projectNotesWrapper;
            RemoveNoteCommand = new DelegateLogCommand(RemoveNoteCommand_ExecuteMethod);
        }

        public NoteViewModel(string text, ProjectNotesWrapper projectNotesWrapper) : base(new Note { Text = text, Date = DateTime.Now })
        {
            _projectNotesWrapper = projectNotesWrapper;
            RemoveNoteCommand = new DelegateLogCommand(RemoveNoteCommand_ExecuteMethod);
        }

        private void RemoveNoteCommand_ExecuteMethod()
        {
            _projectNotesWrapper.NotesViewModel.RemoveNote(this, _projectNotesWrapper);
        }
    }
}