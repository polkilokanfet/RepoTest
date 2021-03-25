using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class DirectumTaskWrapper
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
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ������ ���������� �����
        /// </summary>
        public List<DirectumTaskWrapper> PreviousTasks
        {
            get
            {
                var result = new List<DirectumTaskWrapper>();
                var task = this;
                while (task.PreviousTask != null)
                {
                    task = task.PreviousTask;
                    result.Add(task);
                }
                return result
                    .OrderBy(directumTaskWrapper => directumTaskWrapper.Model.FinishPlan)
                    .ThenBy(directumTaskWrapper => directumTaskWrapper.Model.StartResult)
                    .ToList();
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
                OnPropertyChanged();
            }
        }

        public bool ShowNextTask
        {
            get => _showNextTask;
            set
            {
                _showNextTask = value;
                OnPropertyChanged();
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
    }
}