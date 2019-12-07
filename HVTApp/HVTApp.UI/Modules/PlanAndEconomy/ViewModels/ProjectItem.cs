using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class ProjectItem
    {
        public Project Project { get; }
        public DateTime OrderInTakeDate { get; }
        public ProjectItem(Project project, DateTime orderInTakeDate)
        {
            Project = project;
            OrderInTakeDate = orderInTakeDate;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ProjectItem);
        }

        protected bool Equals(ProjectItem other)
        {
            return Equals(Project, other.Project) && OrderInTakeDate.Equals(other.OrderInTakeDate);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Project != null ? Project.GetHashCode() : 0) * 397) ^ OrderInTakeDate.GetHashCode();
            }
        }
    }
}