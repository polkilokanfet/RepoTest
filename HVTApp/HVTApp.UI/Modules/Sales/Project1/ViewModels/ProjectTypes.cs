using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class ProjectTypes : IEnumerable<ProjectType>
    {
        private readonly ProjectWrapper1 _projectWrapper1;
        private readonly List<ProjectType> _projectTypes;
        private ProjectType _selectedProjectType;

        public ProjectType SelectedProjectType
        {
            get => _selectedProjectType;
            set
            {
                if (Equals(_selectedProjectType, value)) return;
                _selectedProjectType = value;
                this._projectWrapper1.ProjectTypeId = value.Id;
            }
        }

        public ProjectTypes(IUnitOfWork unitOfWork, ProjectWrapper1 projectWrapper1)
        {
            _projectTypes = unitOfWork.Repository<ProjectType>().GetAllAsNoTracking();
            _projectWrapper1 = projectWrapper1;
            if (_projectWrapper1.ProjectTypeId != Guid.Empty)
                _selectedProjectType = _projectTypes.Single(projectType => projectType.Id == _projectWrapper1.ProjectTypeId);
        }

        public IEnumerator<ProjectType> GetEnumerator()
        {
            return _projectTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}