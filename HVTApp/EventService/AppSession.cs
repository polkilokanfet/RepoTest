using System;
using System.ServiceModel;
using HVTApp.Infrastructure;

namespace EventService
{
    class AppSession
    {
        public Guid AppSessionId { get; }
        public Guid UserId { get; }
        public Role UserRole { get; }
        public OperationContext OperationContext { get; }

        /// <summary>
        /// ������ ������������� ����������
        /// </summary>
        /// <param name="appSessionId">�� ������ ����������</param>
        /// <param name="userId">�� ������������ ����������</param>
        /// <param name="userRole">���� ������������ ����������</param>
        /// <param name="operationContext"></param>
        public AppSession(Guid appSessionId, Guid userId, Role userRole, OperationContext operationContext)
        {
            AppSessionId = appSessionId;
            UserId = userId;
            UserRole = userRole;
            OperationContext = operationContext;
        }

        public override string ToString()
        {
            return $"appSessionId: {AppSessionId}, userId: {UserId}, role: {UserRole}";
        }
    }
}