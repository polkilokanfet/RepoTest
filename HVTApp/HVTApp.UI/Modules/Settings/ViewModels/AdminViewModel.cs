using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml.Serialization;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using HVTApp.Model;
using HVTApp.UI.Commands;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
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
                    //try
                    //{
                    //    _container.Resolve<IEmailService>().SendMail("kosolapov.ag@gmail.com", "SubjTest", "BodyTest");
                    //    _container.Resolve<IEmailService>().SendMail("kosolapov.ep@mail.ru", "SubjTest", "BodyTest");
                    //    _container.Resolve<IMessageService>().ShowOkMessageDialog("Send letter", "Success!");
                    //}
                    //catch (Exception e)
                    //{
                    //    _container.Resolve<IHvtAppLogger>().LogError(e.PrintAllExceptions(), e);
                    //    _container.Resolve<IMessageService>().ShowOkMessageDialog("Error", e.PrintAllExceptions());
                    //}


                    var messageService = _container.Resolve<IMessageService>();
                    var unitOfWork = _container.Resolve<IUnitOfWork>();
                    var blocks = unitOfWork.Repository<ProductBlock>().GetAll();
                    foreach (var block in blocks.ToList())
                    {
                        var productBlocks = blocks.Where(x => x.Equals(block)).ToList();
                        if (productBlocks.Count > 1)
                        {
                            var block1 = productBlocks.First();
                            var remove = productBlocks.ToList();
                            remove.Remove(block1);

                            foreach (var b in remove)
                            {
                                foreach (var priceEngineeringTask in unitOfWork.Repository<PriceEngineeringTask>().Find(x => x.ProductBlockManager.Id == b.Id))
                                {
                                    priceEngineeringTask.ProductBlockManager = block1;
                                }

                                foreach (var priceEngineeringTask in unitOfWork.Repository<PriceEngineeringTask>().Find(x => x.ProductBlockEngineer.Id == b.Id))
                                {
                                    priceEngineeringTask.ProductBlockEngineer = block1;
                                }

                                foreach (var priceEngineeringTaskProductBlockAdded in unitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().Find(x => x.ProductBlock.Id == b.Id))
                                {
                                    priceEngineeringTaskProductBlockAdded.ProductBlock = block1;
                                }

                                foreach (var product in unitOfWork.Repository<Product>().Find(x => x.ProductBlock.Id == b.Id))
                                {
                                    product.ProductBlock = block1;
                                }

                                unitOfWork.RemoveEntity(b);
                            }

                            messageService.ShowOkMessageDialog("", $"Удалён повтор в {remove.Count + 1} блоках ({block})");
                        }

                        //var hashCode = block.GetHashCode();
                        //var h = blocks.Where(x => x.GetHashCode() == hashCode).ToList();
                        //if (h.Count > 1)
                        //{
                        //    messageService.ShowOkMessageDialog("", $"Повтор Hash в блоке ({block})");
                        //}

                        productBlocks.ForEach(x => blocks.Remove(x));
                    }

                    var products = unitOfWork.Repository<Product>().GetAll();
                    foreach (var product in products.ToList())
                    {
                        var productsList = products.Where(x => x.Equals(product)).ToList();
                        if (productsList.Count > 1)
                        {
                            var product1 = productsList.First();
                            var remove = productsList.ToList();
                            remove.Remove(product1);
                            foreach (var p in remove)
                            {
                                foreach (var offerUnit in unitOfWork.Repository<OfferUnit>().Find(x => x.Product.Id == p.Id))
                                {
                                    offerUnit.Product = product1;
                                }

                                foreach (var dependent in unitOfWork.Repository<ProductDependent>().Find(x => x.Product.Id == p.Id))
                                {
                                    dependent.Product = product1;
                                }

                                foreach (var productIncluded in unitOfWork.Repository<ProductIncluded>().Find(x => x.Product.Id == p.Id))
                                {
                                    productIncluded.Product = product1;
                                }

                                foreach (var salesUnit in unitOfWork.Repository<SalesUnit>().Find(x => x.Product.Id == p.Id))
                                {
                                    salesUnit.Product = product1;
                                }

                                unitOfWork.RemoveEntity(p);
                            }

                            messageService.ShowOkMessageDialog("", $"Повтор в продуктах ({product})");
                        }

                        //var hashCode = product.GetHashCode();
                        //var h = products.Where(x => x.GetHashCode() == hashCode).ToList();
                        //if (h.Count > 1)
                        //{
                        //    messageService.ShowOkMessageDialog("", $"Повтор Hash в продукте ({product})");
                        //}

                        productsList.ForEach(x => products.Remove(x));
                    }

                    unitOfWork.SaveChanges();

                    messageService.ShowOkMessageDialog("", "Finish");


                    //unitOfWork.SaveChanges();
                    //unitOfWork.Dispose();

                    //_container.Resolve<IMessageService>().ShowOkMessageDialog("fin", "!!!");



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