using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class DirectumTaskWrapper
    {
        private bool _showPreviousTask = true;
        private bool _showNextTask = true;
        private bool _isMain = false;

        public bool IsMain
        {
            get { return _isMain; }
            set
            {
                _isMain = value;
                OnPropertyChanged();
            }
        }

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
                return result;
            } }

        public List<DirectumTaskWrapper> NextTasks { get; } = new List<DirectumTaskWrapper>();

        public List<DirectumTaskWrapper> ParallelTasks { get; } = new List<DirectumTaskWrapper>();
        public List<DirectumTaskWrapper> ChildTasks { get; } = new List<DirectumTaskWrapper>();

        public IEnumerable<DirectumTaskMessageWrapper> MessagesByMoment => Messages?.OrderBy(x => x.Moment);

        public bool ShowPreviousTask
        {
            get { return _showPreviousTask; }
            set
            {
                _showPreviousTask = value;
                OnPropertyChanged();
            }
        }

        public bool ShowNextTask
        {
            get { return _showNextTask; }
            set
            {
                _showNextTask = value;
                OnPropertyChanged();
            }
        }

        public string Header => $"Задача \"{Group.Title}\"";
    }
}