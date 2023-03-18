using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RouteComposer.Controllers;
using RouteComposer.DTO;

namespace RouteComposerTests
{
    [TestClass]
    public class RoutesControllerConsoleTests
    {
        [TestMethod]
        public void ValidRoute()
        {

            var result = new LinkedList<string>(new List<string>(){"A","B","C"});

            RoutesControllerConsole rc = new RoutesControllerConsole(() =>
            {
                var rcMock = new Mock<RouteComposer.Interfaces.IRouteComposer>();

                rcMock.Setup(x => x.AddDirection(It.IsAny<Direction>()));
                rcMock.Setup(x => x.GetRoute()).Returns(result);

                return rcMock.Object;
            });

            rc.OnNewInput("2");

            rc.OnNewInput("A B");
            var resultTest = rc.OnNewInput("B C");

            Assert.AreEqual(string.Format(RouteComposer.Properties.Resources.ResultLine, string.Join(" ",new List<string>(){"A","B","C"})), resultTest);

        }


    }
}
