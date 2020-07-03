using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Modules.Sales.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Market
{
    public partial class Market2ViewModel : ViewModelBase
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
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                ((DelegateCommand)RemoveNoteCommand).RaiseCanExecuteChanged();
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


        public ICommand AddNoteCommand { get; private set; }
        public ICommand RemoveNoteCommand { get; private set; }
        public ICommand SaveNotesCommand { get; private set; }


        public void InitNotes()
        {
            _notesUnitOfWork = Container.Resolve<IUnitOfWork>();
            var projectNotes = _notesUnitOfWork.Repository<Project>().Find(x => true).Select(x => new ProjectNotesWrapper(x));
            _projectNotes = new ValidatableChangeTrackingCollection<ProjectNotesWrapper>(projectNotes);

            AddNoteCommand = new DelegateCommand(
                () =>
                {
                    var note = new NoteWrapper(new Note {Date = DateTime.Now});
                    ProjectNotesWrapper.Notes.Add(note);
                    OnPropertyChanged(nameof(Notes));
                },
                () => ProjectNotesWrapper != null);

            RemoveNoteCommand = new DelegateCommand(
                () =>
                {
                    ProjectNotesWrapper.Notes.Remove(SelectedNote);
                    OnPropertyChanged(nameof(Notes));
                    SelectedNote = null;
                },
                () => SelectedNote != null);

            SaveNotesCommand = new DelegateCommand(
                () =>
                {
                    _projectNotes.AcceptChanges();
                    _notesUnitOfWork.SaveChanges();
                },
                () => _projectNotes.All(x => x.Notes.IsValid) && _projectNotes.Any(x => x.Notes.IsChanged));

            _projectNotes.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand)SaveNotesCommand).RaiseCanExecuteChanged();
            };
        }


    }
}
