using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Market
{
    public partial class Market2ViewModel
    {
        private IUnitOfWork _notesUnitOfWork;
        private IValidatableChangeTrackingCollection<ProjectNotesWrapper> _projectNotes;
        private NoteWrapper _selectedNote;
        private readonly ObservableCollection<NoteWrapper> _notes = new ObservableCollection<NoteWrapper>();

        public ObservableCollection<NoteWrapper> Notes
        {
            get
            {
                _notes.Clear();
                if (ProjectNotesWrapper != null)
                {
                    _notes.AddRange(ProjectNotesWrapper.Notes.OrderByDescending(x => x.Date));
                }
                return _notes;
            }
        }

        public NoteWrapper SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                RemoveNoteCommand.RaiseCanExecuteChanged();
            }
        }

        public ProjectNotesWrapper ProjectNotesWrapper
        {
            get
            {
                var projectNote = _projectNotes.SingleOrDefault(x => x.Model.Id == SelectedProjectItem?.Project.Id);

                if (projectNote == null && SelectedProjectItem != null)
                {
                    var project = _notesUnitOfWork.Repository<Project>().GetById(SelectedProjectItem.Project.Id);
                    projectNote = new ProjectNotesWrapper(project);
                    _projectNotes.Add(projectNote);
                }

                return projectNote;
            }
        }


        public DelegateLogCommand AddNoteCommand { get; private set; }
        public DelegateLogCommand RemoveNoteCommand { get; private set; }
        public DelegateLogCommand SaveNotesCommand { get; private set; }


        public void InitNotes()
        {
            _notesUnitOfWork = Container.Resolve<IUnitOfWork>();
            var projectNotes = ((IProjectRepository)_notesUnitOfWork.Repository<Project>()).GetAllWithNotes().Select(project => new ProjectNotesWrapper(project));
            _projectNotes = new ValidatableChangeTrackingCollection<ProjectNotesWrapper>(projectNotes);

            AddNoteCommand = new DelegateLogCommand(
                () =>
                {
                    var note = new NoteWrapper(new Note {Date = DateTime.Now});
                    ProjectNotesWrapper.Notes.Add(note);
                    RaisePropertyChanged(nameof(Notes));
                },
                () => ProjectNotesWrapper != null);

            RemoveNoteCommand = new DelegateLogCommand(
                () =>
                {
                    ProjectNotesWrapper.Notes.Remove(SelectedNote);
                    RaisePropertyChanged(nameof(Notes));
                    SelectedNote = null;
                },
                () => SelectedNote != null);

            SaveNotesCommand = new DelegateLogCommand(
                () =>
                {
                    _projectNotes.AcceptChanges();
                    _notesUnitOfWork.SaveChanges();
                },
                () => _projectNotes.All(x => x.Notes.IsValid) && _projectNotes.Any(x => x.Notes.IsChanged));

            _projectNotes.PropertyChanged += (sender, args) =>
            {
                SaveNotesCommand.RaiseCanExecuteChanged();
            };
        }
    }
}
