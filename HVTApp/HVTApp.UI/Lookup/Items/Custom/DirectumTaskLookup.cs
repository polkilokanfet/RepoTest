namespace HVTApp.UI.Lookup
{
    public partial class DirectumTaskLookup
    {
        public string Status
        {
            get
            {
                if (Entity.Group.IsStoped)
                    return "�����������";

                if (Entity.FinishAuthor.HasValue)
                    return "�������";

                if (Entity.FinishPerformer.HasValue)
                    return "���������";

                return "� ������";
            }
        }
    }
}