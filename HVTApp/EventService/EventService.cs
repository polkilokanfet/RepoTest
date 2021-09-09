using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;

namespace EventService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public partial class EventService : IEventService
    {
        /// <summary>
        /// Список приложений, подключенных в настоящий момент к сервису
        /// </summary>
        private readonly List<AppSession> _appSessions = new List<AppSession>();

        public event Action<string> PrintMessageEvent;

        /// <summary>
        /// Подключение к сервису
        /// </summary>
        /// <param name="appSessionId">Id сессии приложения</param>
        /// <param name="userId">Id юзера</param>
        /// <returns></returns>
        public bool Connect(Guid appSessionId, Guid userId)
        {
            //если приложение уже подключено к сервису
            if (_appSessions.Select(appSession => appSession.AppSessionId).Contains(appSessionId))
                return false;

            //подключаем новое приложение к сервису
            _appSessions.Add(new AppSession(appSessionId, userId, OperationContext.Current));
            PrintMessageEvent?.Invoke($"Connected appSession {appSessionId}.");
            return true;
        }

        /// <summary>
        /// Отключение от сервиса
        /// </summary>
        /// <param name="appSessionId">Id сессии приложения</param>
        public void Disconnect(Guid appSessionId)
        {
            var appSession = _appSessions.SingleOrDefault(session => session.AppSessionId == appSessionId);
            if (appSession != null)
            {
                _appSessions.Remove(appSession);
                PrintMessageEvent?.Invoke($"Disconnected appSession {appSessionId}.");
            }
        }

        public bool HostIsAlive()
        {
            return true;
        }

        public bool UserIsConnected(Guid userId)
        {
            return _appSessions.Select(appSession => appSession.UserId).Contains(userId);
        }

        public bool CopyProjectAttachments(Guid userId, Guid projectId, string targetDirectory)
        {
            var appSession = _appSessions.FirstOrDefault(session => session.UserId == userId);
            if (appSession != null)
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().CopyProjectAttachmentsCallback(projectId, targetDirectory);
                    this.PrintMessageEvent?.Invoke($"CopyProjectAttachments() done to directory {targetDirectory}. userId={userId}, projectId={projectId}");
                    return true;
                }
                catch (Exception e)
                {
                    this.PrintMessageEvent?.Invoke(e.PrintAllExceptions());
                }
            }

            return false;
        }

        /// <summary>
        /// Закрытие хоста
        /// </summary>
        public void Close()
        {
            foreach (var appSession in _appSessions.ToList())
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnServiceDisposeEvent();
                    PrintMessageEvent?.Invoke($"Succsess on {this.GetType().FullName}.Close() {appSession}.");
                    this.Disconnect(appSession.AppSessionId);
                }
                catch (Exception e)
                {
                    PrintMessageEvent?.Invoke($"Exception on {this.GetType().FullName}.Close() appSession={appSession}. {e.GetType().FullName} \n {e.PrintAllExceptions()}");
                }
            }
        }



        public void SendMessageToChat(Guid authorId, string message)
        {
            foreach (var appSession in _appSessions.Where(x => x.UserId != authorId).ToList())
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSendMessageToChat(authorId, message);
                }
                catch (TimeoutException)
                {
                    _appSessions.Remove(appSession);
                }
                catch (Exception)
                {
                }
            }
        }

        public void SendMessageToUser(Guid authorId, Guid recipientId, string message)
        {
            foreach (var appSession in _appSessions.Where(x => x.UserId == recipientId).ToList())
            {
                try
                {
                    appSession.OperationContext.GetCallbackChannel<IEventServiceCallback>().OnSendMessageToUser(authorId, message);
                }
                catch (TimeoutException)
                {
                    _appSessions.Remove(appSession);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
