using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class DirectumTaskWrapper : IComparable<DirectumTaskWrapper>
    {
        private bool _showPreviousTask = true;
        private bool _showNextTask = true;
        private bool _isMain = false;

        /// <summary>
        /// ������ �������� ������� � ���������
        /// </summary>
        public bool IsMain
        {
            get => _isMain;
            set
            {
                _isMain = value;
                RaisePropertyChanged();
            }
        }

        public bool HasChildTasks => ChildTasks.Any();

        /// <summary>
        /// ������ ���������� �����
        /// </summary>
        public List<DirectumTaskWrapper> PreviousTasks
        {
            get
            {
                var tasks = new List<DirectumTaskWrapper>();
                var task = this;
                while (task.PreviousTask != null)
                {
                    task = task.PreviousTask;
                    tasks.Add(task);
                }
                tasks.Sort();
                return tasks;
            }
        }

        /// <summary>
        /// ������ ���� ����������� �����
        /// </summary>
        public List<DirectumTaskWrapper> NextTasks { get; } = new List<DirectumTaskWrapper>();

        /// <summary>
        /// ������������ ������
        /// </summary>
        public List<DirectumTaskWrapper> ParallelTasks { get; } = new List<DirectumTaskWrapper>();

        /// <summary>
        /// �������� ������
        /// </summary>
        public List<DirectumTaskWrapper> ChildTasks { get; } = new List<DirectumTaskWrapper>();

        /// <summary>
        /// ���������, ������������� �� ������� ��������
        /// </summary>
        public IEnumerable<DirectumTaskMessageWrapper> MessagesByMoment => Messages?.OrderBy(message => message.Moment);

        public bool ShowPreviousTask
        {
            get => _showPreviousTask;
            set
            {
                _showPreviousTask = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowNextTask
        {
            get => _showNextTask;
            set
            {
                _showNextTask = value;
                RaisePropertyChanged();
            }
        }

        public string Header => $"������ \"{Group.Title}\"";

        /// <summary>
        /// ������� �����-��������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DirectumTaskGroupFileWrapper> GetFiles()
        {
            //�� �������
            foreach (var file in this.Group.Files)
            {
                yield return file;
            }

            //�� ������������
            if (this.ParentTask != null)
            {
                foreach (var file in this.ParentTask.GetFiles())
                {
                    yield return file;
                }
            }

            //�� ��������
            foreach (var childTask in ChildTasks)
            {
                foreach (var file in childTask.GetFiles())
                {
                    yield return file;
                }
            }

            //�� ������������
            foreach (var parallelTask in ParallelTasks)
            {
                foreach (var file in parallelTask.GetFiles())
                {
                    yield return file;
                }
            }

            //�� ����������
            foreach (var previousTask in PreviousTasks)
            {
                foreach (var file in previousTask.GetFiles())
                {
                    yield return file;
                }
            }

            //�� �����������
            foreach (var nextTask in NextTasks)
            {
                foreach (var file in nextTask.GetFiles())
                {
                    yield return file;
                }
            }
        }

        public int CompareTo(DirectumTaskWrapper other)
        {
            return this.Model.CompareTo(other.Model);
        }
    }
}