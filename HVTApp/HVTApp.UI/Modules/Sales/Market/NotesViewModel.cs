using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.ViewModels;

namespace HVTApp.UI.Modules.Sales.Market
{
    public class NotesViewModel : NotifyDataErrorInfoBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<ProjectNotesWrapper> _projectNotesWrappers;
        private string _noteText;
        private ProjectNotesWrapper _projectNotesWrapper;

        private ProjectNotesWrapper ProjectNotesWrapper
        {
            get => _projectNotesWrapper;
            set => SetProperty(ref _projectNotesWrapper, value, () => RaisePropertyChanged(nameof(Notes)));
        }

        public IEnumerable<NoteViewModel> Notes
        {
            get
            {
                return ProjectNotesWrapper == null 
                    ? default(IEnumerable<NoteViewModel>) 
                    : ProjectNotesWrapper.Notes.OrderByDescending(note => note.Model.Date);
            }
        }

        public string NoteText
        {
            get => _noteText;
            set => SetProperty(ref _noteText, value, this.Validate);
        }

        public DelegateLogCommand AddNoteCommand { get; }

        public NotesViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectNotesWrappers = ((IProjectRepository)unitOfWork.Repository<Project>()).GetAllForUserWithNotes().Select(project => new ProjectNotesWrapper(project, this)).ToList();

            AddNoteCommand = new DelegateLogCommand(
                () =>
                {
                    if (string.IsNullOrWhiteSpace(NoteText) || ProjectNotesWrapper == null) return;
                    ProjectNotesWrapper.Notes.Add(new NoteViewModel(NoteText, ProjectNotesWrapper));
                    ProjectNotesWrapper.AcceptChanges();
                    _unitOfWork.SaveChanges();
                    RaisePropertyChanged(nameof(Notes));
                    NoteText = string.Empty;
                }, 
                () => 
                    ProjectNotesWrapper != null && 
                    this.Errors.HasErrors == false);
        }

        public void SelectProject(Guid? projectId)
        {
            if (projectId == null)
            {
                ProjectNotesWrapper = null;
            }
            else
            {
                var projectNotesWrapper = _projectNotesWrappers.SingleOrDefault(x => x.Model.Id == projectId);
                if (projectNotesWrapper == null)
                {
                    projectNotesWrapper = new ProjectNotesWrapper(_unitOfWork.Repository<Project>().GetById(projectId.Value), this);
                    _projectNotesWrappers.Add(projectNotesWrapper);
                }

                ProjectNotesWrapper = projectNotesWrapper;
            }
        }

        protected void Validate()
        {
            Errors.Reboot();

            var validationResults = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(NoteText) == false &&
                NoteText.Length > 150)
            {
                validationResults.Add(new ValidationResult("Заметка не может превышать 150 символов", new[] { nameof(NoteText) }));
            }

            //если валидатор нашел ошибки.
            foreach (var validationResult in validationResults)
            {
                foreach (var propertyName in validationResult.MemberNames)
                {
                    Errors.Add(new DataErrorInfo(propertyName, validationResult.ErrorMessage));
                }
            }

            if (Errors.IsChanged)
            {
                foreach (var propertyName in Errors.ChangedErrors.Select(x => x.PropertyName).Distinct())
                {
                    OnErrorsChanged(propertyName);
                }
            }
        }

        public void RemoveNote(NoteViewModel noteViewModel, ProjectNotesWrapper projectNotesWrapper)
        {
            projectNotesWrapper.Notes.Remove(noteViewModel);
            projectNotesWrapper.AcceptChanges();
            _unitOfWork.Repository<Note>().Delete(noteViewModel.Model);
            _unitOfWork.SaveChanges();
            RaisePropertyChanged(nameof(Notes));
        }
    }
}
