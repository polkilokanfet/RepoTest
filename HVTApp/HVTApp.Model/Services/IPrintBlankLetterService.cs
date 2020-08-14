using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IPrintBlankLetterService
    {
        void PrintBlankLetter (Document letter, string path);
    }
}