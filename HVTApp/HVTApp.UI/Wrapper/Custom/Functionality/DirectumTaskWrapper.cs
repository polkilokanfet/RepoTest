using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class DirectumTaskWrapper
    {
        private bool _showPreviousTask = true;

        /// <summary>
        /// не собирает все задачи! костыль!
        /// </summary>
        public List<DirectumTaskWrapper> PreviousTasks => PreviousTask != null 
            ? new List<DirectumTaskWrapper> {PreviousTask} 
            : new List<DirectumTaskWrapper>();

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
    }
}