namespace HVTApp.UI.Lookup
{
    public partial class DirectumTaskLookup
    {
        public string Status
        {
            get
            {
                if (Entity.Group.IsStoped)
                    return "Остановлено";

                if (Entity.FinishAuthor.HasValue)
                    return "Принято";

                if (Entity.FinishPerformer.HasValue)
                    return "Исполнено";

                return "В работе";
            }
        }
    }
}