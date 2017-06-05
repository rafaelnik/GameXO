using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStat.Controllers;
using System.Web.Mvc;

namespace GameXOTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void ClearStat_EmptyFileExpected()
        {
            HomeController controller = new HomeController();

            ActionResult result = controller.Index() as ActionResult;

            Assert.IsNotNull(result);
        }
    }
}
