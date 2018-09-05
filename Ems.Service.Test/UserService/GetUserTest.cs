using Ems.Data;
using Ems.Domain;
using Ems.Services.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Test.UserService
{
    [TestClass]
    public class GetUserTest
    {
        [TestMethod]
        public void GetUser_Existing()
        {
            AutoMapperConfig.Init();
            var userService = new Ems.Services.UserService(new UnitOfWork(new EmsDevEntities()));
            var response = userService.Get(17);
            Assert.AreEqual(true, response.IsSuccess);
        }

        [TestMethod]
        public void GetUser_NoneExisting()
        {
            var userService = new Ems.Services.UserService(new UnitOfWork(new EmsDevEntities()));
            var response = userService.Get(9999);
            Assert.AreEqual(false, response.IsSuccess);

        }
    }
}
