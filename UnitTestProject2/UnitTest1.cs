using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplicationBalance2.Models;
using WebApplicationBalance2.Controllers;
using System.Collections.Generic;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        //пример 1
        [TestMethod]
        public void TestMethod1()
        {
            List<InfoFlows> infoFlows = new List<InfoFlows>();
            infoFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "4", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "5", SourceId = "3",  DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false});
            infoFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false});

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = infoFlows, Delta_error = 0.05, Limitations=null});

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Баланс сошелся!", result.Message);
        }

        //привер 2: с 8-мым потоком
        [TestMethod]
        public void TestMethod2()
        {
            List<InfoFlows> infoFlows = new List<InfoFlows>();
            infoFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "4", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });
            infoFlows.Add(new InfoFlows { Id = "8", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 6.667, Tolerance = 0.667, UseTecnologicRanges = false });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = infoFlows, Delta_error = 0.05, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Баланс сошелся!", result.Message);
        }

        //пример 3: с ограничением
        [TestMethod]
        public void TestMethod3()
        {
            List<InfoFlows> aboutFlows = new List<InfoFlows>();
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "4", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "8", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 6.667, Tolerance = 0.667, UseTecnologicRanges = false });

            List<Limit> limitations = new List<Limit>();
            List<A> a = new List<A>();
            a.Add(new A { FlowId = "1", Coefficient = 1 });
            a.Add(new A { FlowId = "2", Coefficient = -10 });
            limitations.Add(new Limit { A = a, B = 0, Sign = "=" });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = aboutFlows, Delta_error = 0.05, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Баланс сошелся!", result.Message);
        }

        //не сходится
        [TestMethod]
        public void TestMethod4()
        {
            List<InfoFlows> aboutFlows = new List<InfoFlows>();
            List<TecnologicRange> tecnologicRanges = new List<TecnologicRange>();
            tecnologicRanges.Add(new TecnologicRange { MinValue = 15, MaxValue = 20 });
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = true, TecnologicRanges = tecnologicRanges });
            aboutFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "4", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "8", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 6.667, Tolerance = 0.667, UseTecnologicRanges = false });

            List<Limit> limitations = new List<Limit>();
            List<A> a = new List<A>();
            a.Add(new A { FlowId = "1", Coefficient = 1 });
            a.Add(new A { FlowId = "2", Coefficient = -10 });
            limitations.Add(new Limit { A = a, B = 0, Sign = "=" });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = aboutFlows, Delta_error = 0.05, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual(false, result.IsBalancing);
        }

        // повторяется название потока
        [TestMethod]
        public void TestMethod5()
        {
            List<InfoFlows> aboutFlows = new List<InfoFlows>();
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "8", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 6.667, Tolerance = 0.667, UseTecnologicRanges = false });

            List<Limit> limitations = new List<Limit>();
            List<A> a = new List<A>();
            a.Add(new A { FlowId = "1", Coefficient = 1 });
            a.Add(new A { FlowId = "2", Coefficient = -10 });
            limitations.Add(new Limit { A = a, B = 0, Sign = "=" });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = aboutFlows, Delta_error = 0.05, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Поток с id = '1' уже существует в списке!", result.Message);
        }

        //нет названия у потока
        [TestMethod]
        public void TestMethod6()
        {
            List<InfoFlows> aboutFlows = new List<InfoFlows>();
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "8", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 6.667, Tolerance = 0.667, UseTecnologicRanges = false });

            List<Limit> limitations = new List<Limit>();
            List<A> a = new List<A>();
            a.Add(new A { FlowId = "1", Coefficient = 1 });
            a.Add(new A { FlowId = "2", Coefficient = -10 });
            limitations.Add(new Limit { A = a, B = 0, Sign = "=" });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = aboutFlows, Delta_error = 0.05, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Поток не может иметь в качестве названия пустую строку", result.Message);
        }

     

        //пустой запрос
        [TestMethod]
        public void TestMethod7()
        {
            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = null, Delta_error = 0.05, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Не найдено ни одного объекта!", result.Message);
        }
        

        //пример 1 с уменьшением ошибки. Баланс не сойдется
        [TestMethod]
        public void TestMethod8()
        {
            List<InfoFlows> aboutFlows = new List<InfoFlows>();
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "4", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = aboutFlows, Delta_error = 0.00005, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("баланс не сходится в узле 3", result.Message);
        }

        //привер 2: с 8-мым потоком с уменьшением ошибки. Баланс не сойдется
        [TestMethod]
        public void TestMethod9()
        {
            List<InfoFlows> aboutFlows = new List<InfoFlows>();
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "4", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "8", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 6.667, Tolerance = 0.667, UseTecnologicRanges = false });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = aboutFlows, Delta_error = 0.00005, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Баланс сошелся!", result.Message);
        }

        //пример 3: с ограничением с уменьшением ошибки. Баланс не сойдется
        [TestMethod]
        public void TestMethod10()
        {
            List<InfoFlows> aboutFlows = new List<InfoFlows>();
            aboutFlows.Add(new InfoFlows { Id = "1", SourceId = "1", DestinationId = null, IsExcluded = false, Value = 10.005, Tolerance = 0.2, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "2", SourceId = "", DestinationId = "1", IsExcluded = false, Value = 3.033, Tolerance = 0.121, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "3", SourceId = "2", DestinationId = "1", IsExcluded = false, Value = 6.831, Tolerance = 0.683, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "4", SourceId = "", DestinationId = "2", IsExcluded = false, Value = 1.985, Tolerance = 0.04, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "5", SourceId = "3", DestinationId = "2", IsExcluded = false, Value = 5.093, Tolerance = 0.102, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "6", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 4.057, Tolerance = 0.081, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "7", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 0.991, Tolerance = 0.02, UseTecnologicRanges = false });
            aboutFlows.Add(new InfoFlows { Id = "8", SourceId = null, DestinationId = "3", IsExcluded = false, Value = 6.667, Tolerance = 0.667, UseTecnologicRanges = false });

            List<Limit> limitations = new List<Limit>();
            List<A> a = new List<A>();
            a.Add(new A { FlowId = "1", Coefficient = 1 });
            a.Add(new A { FlowId = "2", Coefficient = -10 });
            limitations.Add(new Limit { A = a, B = 0, Sign = "=" });

            BalanceInput balanceInput = new BalanceInput();
            balanceInput.Flows = new List<Flow>();
            balanceInput.Flows.Add(new Flow { Flows = aboutFlows, Delta_error = 0.00005, Limitations = null });

            var controller = new BalanceController();
            var result = controller.Post(balanceInput) as BalanceOutput;
            Assert.AreEqual("Баланс сошелся!", result.Message);
        }

    }
}
