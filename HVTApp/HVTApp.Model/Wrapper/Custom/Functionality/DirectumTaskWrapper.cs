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
        /// Задача является главной в контексте
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
        /// Список предыдущих задач
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
        /// Список всех последующих задач
        /// </summary>
        public List<DirectumTaskWrapper> NextTasks { get; } = new List<DirectumTaskWrapper>();

        /// <summary>
        /// Параллельные задачи
        /// </summary>
        public List<DirectumTaskWrapper> ParallelTasks { get; } = new List<DirectumTaskWrapper>();

        /// <summary>
        /// Дочерние задачи
        /// </summary>
        public List<DirectumTaskWrapper> ChildTasks { get; } = new List<DirectumTaskWrapper>();

        /// <summary>
        /// Сообщения, упорядоченные по моменту создания
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

        public string Header => $"Задача \"{Group.Title}\"";

        /// <summary>
        /// Вернуть файлы-вложения
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DirectumTaskGroupFileWrapper> GetFiles()
        {
            //из текущей
            foreach (var file in this.Group.Files)
            {
                yield return file;
            }

            //из родительской
            if (this.ParentTask != null)
            {
                foreach (var file in this.ParentTask.GetFiles())
                {
                    yield return file;
                }
            }

            //из дочерних
            foreach (var childTask in ChildTasks)
            {
                foreach (var file in childTask.GetFiles())
                {
                    yield return file;
                }
            }

            //из параллельных
            foreach (var parallelTask in ParallelTasks)
            {
                foreach (var file in parallelTask.GetFiles())
                {
                    yield return file;
                }
            }

            //из предыдущих
            foreach (var previousTask in PreviousTasks)
            {
                foreach (var file in previousTask.GetFiles())
                {
                    yield return file;
                }
            }

            //из последующих
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