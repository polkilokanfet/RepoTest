namespace HVTApp.Model.Wrapper
{
    public partial class PriceEngineeringTaskFileAnswerWrapper : IFilePathContainer, IIsActual
    {
        /// <summary>
        /// ���� � ����������� � ��������� �����
        /// </summary>
        public string Path { get; set; }
    }
}