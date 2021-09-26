using System;

namespace HVTApp.Infrastructure
{
    public class UnitOfWorkOperationResult
    {
        /// <summary>
        /// Операция успешно завершена
        /// </summary>
        public bool OperationCompletedSuccessfully { get; }

        /// <summary>
        /// Ошибка сохранения
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Успешное завершение операции
        /// </summary>
        public UnitOfWorkOperationResult()
        {
            OperationCompletedSuccessfully = true;
            Exception = null;
        }

        /// <summary>
        /// Ошибка при выполнении операции
        /// </summary>
        /// <param name="exception">Ошибка</param>
        public UnitOfWorkOperationResult(Exception exception)
        {
            Exception = exception;
            OperationCompletedSuccessfully = false;
        }
    }
}