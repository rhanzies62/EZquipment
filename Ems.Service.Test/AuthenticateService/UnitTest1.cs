using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ems.Services.Config;
using Ems.Data;
using Ems.Domain;
using Ems.Services;

namespace Ems.Service.Test.AuthenticateService
{
    [TestClass]
    public class AuthenticateServiceTest
    {
        [TestMethod]
        public void LoginUser_Correct_Success()
        {
            AutoMapperConfig.Init();
            var authenticateService = new Ems.Services.AuthenticateService(new UnitOfWork(new EmsDevEntities()));
            var response = authenticateService.Login(new Domain.DTO.UserAccountDto
            {
                UserName = "franciscebu",
                Password = "123456",
                IPAddress = "192.168.0.1"
            });
            Assert.AreEqual(true, response.IsSuccess);
        }

        [TestMethod]
        public void LoginUser_Incorrect_Success()
        {
            var authenticateService = new Ems.Services.AuthenticateService(new UnitOfWork(new EmsDevEntities()));
            var response = authenticateService.Login(new Domain.DTO.UserAccountDto
            {
                UserName = "franciscebu",
                Password = "1234561",
                IPAddress = "192.168.0.1"
            });
            Assert.AreEqual(false, response.IsSuccess);
            Assert.AreEqual(EmsResousrce.ErrMsgInvalidPassword, response.Message);
        }
    }
}
