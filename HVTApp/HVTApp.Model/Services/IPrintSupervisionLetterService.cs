﻿using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IPrintSupervisionLetterService
    {
        void PrintSupervisionLetter(IEnumerable<Supervision> supervisions, Document letter, string path = "");
    }
}