using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp
{
    public class HvtAppLogger2 : IHvtAppLogger
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly User _user;

        public HvtAppLogger2(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _user = _unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id);
        }

        public void LogError(string message, Exception exception = null, string fileName = "")
        {
            LogUnit logUnit = new LogUnit
            {
                Moment = DateTime.Now,
                Head = message,
                Author = _user
            };
            if (exception != null)
            {
                logUnit.Head = exception.GetType().Name;
                logUnit.Message = exception.PrintAllExceptions();
                while (exception != null)
                {
                    logUnit.Head = exception.ToString();
                    exception = exception.InnerException;
                }
            }

            _unitOfWork.Repository<LogUnit>().Add(logUnit);
            _unitOfWork.SaveChanges();
        }
    }
}