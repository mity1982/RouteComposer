using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RouteComposer.DTO;

namespace RouteComposerTests
{
    [TestClass]
    public class RouteComposerTests
    {
        [TestMethod]
        public void ValidRoute()
        {
            var rc = new global::RouteComposer.Services.RouteComposer(); 

            //Исходные данные: ((‘Москва’, ‘Тюмень’), (‘Тюмень’, ‘Сочи’), (‘Ростов - на - Дону’, ‘Москва’))
            //Выходные данные: (‘Ростов - на - Дону’, ‘Москва’, ‘Тюмень’, ‘Сочи’)

            rc.AddDirection(new Direction(){From = "Москва", To = "Тюмень"});
            rc.AddDirection(new Direction() { From = "Тюмень", To = "Сочи" });
            rc.AddDirection(new Direction() { From = "Ростов - на - Дону", To = "Москва" });

            var routeNode = rc.GetRoute().First;
            Assert.AreEqual("Ростов - на - Дону", routeNode.Value);
            Assert.AreEqual("Москва", routeNode.Next.Value);
            Assert.AreEqual("Тюмень", routeNode.Next.Next.Value);
            Assert.AreEqual("Сочи", routeNode.Next.Next.Next.Value);
        }

        [TestMethod]
        public void ValidRouteComplex()
        {
            var rc = new global::RouteComposer.Services.RouteComposer();

            //Исходные данные: ((‘Москва’, ‘Тюмень’), (‘Тюмень’, ‘Сочи’), (‘Ростов - на - Дону’, ‘Москва’))
            //Выходные данные: (‘Ростов - на - Дону’, ‘Москва’, ‘Тюмень’, ‘Сочи’)

            rc.AddDirection(new Direction() { From = "Москва", To = "Тюмень" });
            rc.AddDirection(new Direction() { From = "Nsk", To = "Msk" });
            rc.AddDirection(new Direction() { From = "Тюмень", To = "Сочи" });
            rc.AddDirection(new Direction() { From = "Ростов - на - Дону", To = "Москва" });
            rc.AddDirection(new Direction() { From = "Сочи", To = "Nsk" });

            var routeNode = rc.GetRoute().First;
            Assert.AreEqual("Ростов - на - Дону", routeNode.Value);
            Assert.AreEqual("Москва", routeNode.Next.Value);
            Assert.AreEqual("Тюмень", routeNode.Next.Next.Value);
            Assert.AreEqual("Сочи", routeNode.Next.Next.Next.Value);
            Assert.AreEqual("Nsk", routeNode.Next.Next.Next.Next.Value);
            Assert.AreEqual("Msk", routeNode.Next.Next.Next.Next.Next.Value);
        }
    }
}
