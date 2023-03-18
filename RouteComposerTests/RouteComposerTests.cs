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

            //�������� ������: ((��������, ��������), (��������, �����), (������� - �� - ����, ��������))
            //�������� ������: (������� - �� - ����, ��������, ��������, �����)

            rc.AddDirection(new Direction(){From = "������", To = "������"});
            rc.AddDirection(new Direction() { From = "������", To = "����" });
            rc.AddDirection(new Direction() { From = "������ - �� - ����", To = "������" });

            var routeNode = rc.GetRoute().First;
            Assert.AreEqual("������ - �� - ����", routeNode.Value);
            Assert.AreEqual("������", routeNode.Next.Value);
            Assert.AreEqual("������", routeNode.Next.Next.Value);
            Assert.AreEqual("����", routeNode.Next.Next.Next.Value);
        }

        [TestMethod]
        public void ValidRouteComplex()
        {
            var rc = new global::RouteComposer.Services.RouteComposer();

            //�������� ������: ((��������, ��������), (��������, �����), (������� - �� - ����, ��������))
            //�������� ������: (������� - �� - ����, ��������, ��������, �����)

            rc.AddDirection(new Direction() { From = "������", To = "������" });
            rc.AddDirection(new Direction() { From = "Nsk", To = "Msk" });
            rc.AddDirection(new Direction() { From = "������", To = "����" });
            rc.AddDirection(new Direction() { From = "������ - �� - ����", To = "������" });
            rc.AddDirection(new Direction() { From = "����", To = "Nsk" });

            var routeNode = rc.GetRoute().First;
            Assert.AreEqual("������ - �� - ����", routeNode.Value);
            Assert.AreEqual("������", routeNode.Next.Value);
            Assert.AreEqual("������", routeNode.Next.Next.Value);
            Assert.AreEqual("����", routeNode.Next.Next.Next.Value);
            Assert.AreEqual("Nsk", routeNode.Next.Next.Next.Next.Value);
            Assert.AreEqual("Msk", routeNode.Next.Next.Next.Next.Next.Value);
        }
    }
}
