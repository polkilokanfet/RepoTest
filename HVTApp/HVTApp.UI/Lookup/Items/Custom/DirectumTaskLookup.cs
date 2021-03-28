namespace HVTApp.UI.Lookup
{
    public partial class DirectumTaskLookup
    {
        public string Direction { get; set; }

        /// <summary>
        /// Актуально для исполнения
        /// </summary>
        public bool IsActualToPerform =>
            this.Group.IsStoped == false &&
            this.Entity.FinishPerformer.HasValue == false;
    }
}