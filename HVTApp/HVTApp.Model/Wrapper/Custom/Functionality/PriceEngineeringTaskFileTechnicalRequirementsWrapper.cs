namespace HVTApp.Model.Wrapper
{
    public partial class PriceEngineeringTaskFileTechnicalRequirementsWrapper : IFilePathContainer, IIsActual
    {
        /// <summary>
        /// ���� � ����������� � ��������� �����
        /// </summary>
        public string Path { get; set; }
    }
}