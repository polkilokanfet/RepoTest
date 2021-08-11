using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml.Serialization;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using HVTApp.Model;
using HVTApp.UI.Commands;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class AdminViewModel
    {
        private readonly IUnityContainer _container;
        public DelegateLogCommand Command { get; }

        public AdminViewModel(IUnityContainer container)
        {
            _container = container;
            Command = new DelegateLogCommand(
                () =>
                {
                    var moment = DateTime.Now;

                    var unitOfWork = _container.Resolve<IUnitOfWork>();

                    var filesAnswers = unitOfWork.Repository<AnswerFileTce>().GetAll();
                    filesAnswers.ForEach(file => file.Date = moment);

                    var filesTce = unitOfWork.Repository<TechnicalRequrementsFile>().GetAll();
                    filesTce.ForEach(file => file.Date = moment);

                    var tasks = unitOfWork.Repository<TechnicalRequrementsTask>().GetAll();
                    foreach (var task in tasks)
                    {
                        var dateCreate = task.FirstStartMoment ?? moment;
                        var dateStart = task.Start;
                        var dateFinish = task.PriceCalculations
                            .Where(priceCalculation => priceCalculation.TaskOpenMoment.HasValue)
                            .Max(priceCalculation => priceCalculation.TaskOpenMoment);

                        //создание задачи
                        var elementCreate = new TechnicalRequrementsTaskHistoryElement
                        {
                            Moment = dateCreate.AddMinutes(-10),
                            Type = TechnicalRequrementsTaskHistoryElementType.Creation,
                            Comment = "Восстановленная история"
                        };
                        task.HistoryElements.Add(elementCreate);

                        task.Requrements
                            .SelectMany(requrements => requrements.Files)
                            .ForEach(file => file.Date = elementCreate.Moment.AddMinutes(1));

                        //старт задачи
                        if (dateStart.HasValue && dateStart.Value != dateCreate)
                        {
                            var elementStart1 = new TechnicalRequrementsTaskHistoryElement
                            {
                                Moment = dateCreate,
                                Type = TechnicalRequrementsTaskHistoryElementType.Start,
                                Comment = task.Comment
                            };
                            task.HistoryElements.Add(elementStart1);

                            var elementStop = new TechnicalRequrementsTaskHistoryElement
                            {
                                Moment = dateStart.Value.AddMinutes(-1),
                                Type = TechnicalRequrementsTaskHistoryElementType.Stop
                            };
                            task.HistoryElements.Add(elementStop);
                        }

                        var dateStart2 = dateStart ?? dateCreate;
                        var elementStart2 = new TechnicalRequrementsTaskHistoryElement
                        {
                            Moment = dateStart2,
                            Type = TechnicalRequrementsTaskHistoryElementType.Start,
                            Comment = task.Comment
                        };
                        task.HistoryElements.Add(elementStart2);

                        //поручение БМ
                        if (task.BackManager != null)
                        {
                            DateTime dateInstruct = task.HistoryElements
                                .Where(x => x.Type == TechnicalRequrementsTaskHistoryElementType.Start)
                                .Min(x => x.Moment)
                                .AddMinutes(1);

                            var elementInstruct = new TechnicalRequrementsTaskHistoryElement
                            {
                                Moment = dateInstruct,
                                Type = TechnicalRequrementsTaskHistoryElementType.Instruct,
                                Comment = $"Поручено ({task.BackManager.Employee.Person})"
                            };
                            if (!string.IsNullOrWhiteSpace(task.CommentBackOfficeBoss))
                            {
                                elementInstruct.Comment = $"{elementInstruct.Comment} с комментарием: {task.CommentBackOfficeBoss}";
                            }
                            task.HistoryElements.Add(elementInstruct);
                        }

                        //отклонение БМ
                        if (task.RejectByBackManagerMoment.HasValue)
                        {
                            var elementReject = new TechnicalRequrementsTaskHistoryElement
                            {
                                Moment = task.RejectByBackManagerMoment.Value,
                                Type = TechnicalRequrementsTaskHistoryElementType.Reject,
                                Comment = task.RejectComment
                            };
                            task.HistoryElements.Add(elementReject);

                            task.AnswerFiles.ForEach(answerFileTce => answerFileTce.Date = elementReject.Moment);
                        }

                        //финиш задачи
                        if (dateFinish.HasValue)
                        {
                            //создание задачи
                            var elementFinish = new TechnicalRequrementsTaskHistoryElement
                            {
                                Moment = dateFinish.Value,
                                Type = TechnicalRequrementsTaskHistoryElementType.Finish
                            };
                            task.HistoryElements.Add(elementFinish);

                            task.AnswerFiles.ForEach(answerFileTce => answerFileTce.Date = elementFinish.Moment);
                        }
                    }

                    unitOfWork.SaveChanges();

                    _container.Resolve<IMessageService>().ShowOkMessageDialog("fin", "fin");

                    ////_container.Resolve<IEmailService>().SendMail("kosolapov.ag@gmail.com", "SubjTest", "BodyTest");
                    ////var unitOfWork = container.Resolve<IUnitOfWork>();

                    //var dependent = new Dependent {Name = "dep1"};
                    //var mList = new List<Main>();
                    //for (int i = 0; i < 4; i++)
                    //{
                    //    mList.Add(new Main {Name = $"main{i}", Dependent = dependent});
                    //}

                    //var serializer = new XmlSerializer(typeof(List<Main>));
                    //using (var fs = new FileStream(@"G:\main.xml", FileMode.Create))
                    //{
                    //    serializer.Serialize(fs, mList);
                    //}

                    //using (var fs = new FileStream(@"G:\main.xml", FileMode.Open))
                    //{
                    //    var list = (List<Main>)serializer.Deserialize(fs);
                    //    var dep = list.Select(x => x.Dependent).Distinct().ToList();
                    //}
                });
        }
    }

    [Serializable]
    public class Main
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "main";
        public Dependent Dependent { get; set; }
    }

    [Serializable]
    public class Dependent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "dependent";

    }
}