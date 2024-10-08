namespace HVTApp.DataAccess
{
    public partial class TaskInvoiceForPaymentConfiguration
    {
        public TaskInvoiceForPaymentConfiguration()
        {
            HasMany(task => task.Items).WithRequired().HasForeignKey(item => item.TaskId).WillCascadeOnDelete(true);
            HasOptional(task => task.PlanMaker).WithMany().WillCascadeOnDelete(false);
            HasOptional(task => task.BackManager).WithMany().WillCascadeOnDelete(false);
            Property(task => task.Comment).IsOptional().HasMaxLength(128);
        }
    }
}