using System;
using DashboardHR.Models.Models;
using Dashboard_WebApp.Controllers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Dashboard_WebApp.Tests
{
    [TestFixture]
    public class ConfigerationControllerTests
    {
        private readonly ConfigurationController _aConfigurationController;
        public ConfigerationControllerTests()
        {
            _aConfigurationController = new ConfigurationController();
        }

        [Test]
        public void ConpanyTest()
        {
            var aInfo = new BusinessInfo
            {
                BuyerCode = "1",
                CompanyCode = "BTP",
                FilterName = "all",
                MerchantCode = "a"
            };
           
            dynamic company = _aConfigurationController.DashboardCompanyJsonData(aInfo);
            HomeController aController = new HomeController();
            dynamic output = aController.GetUnitTestCode();
            int v = Convert.ToInt32(output.Data[0].CompanyId);
            Assert.AreEqual(1, v);
        }
    }
}
