namespace HVTApp.Model.Wrapper
{
    public partial class PriceEngineeringTaskFileAnswerWrapper : IFilePathContainer, IIsActual
    {
        /// <summary>
        /// Путь к копируемому в хранилище файлу
        /// </summary>
        public string Path { get; set; }
    }
}