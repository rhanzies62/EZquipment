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
    public class UpdateTest
    {
        [TestMethod]
        public void UpdateUserBasicInfo_Success()
        {
            var userService = new Ems.Services.UserService(new UnitOfWork(new EmsDevEntities()));
            var response = userService.Get(6);
            Assert.AreEqual(true, response.IsSuccess);
            if (response.IsSuccess)
            {
                var userDto = response.Data;
                userDto.FirstName = "Francis";
                userDto.LastName = "Cebu Jr";

                if (userDto.Address == null)
                    userDto.Address = new Domain.DTO.AddressDto();

                userDto.Address.StreetAddress = "Blk 16 lot 18 Yakal Street Zabarte Road Camarin";
                userDto.Address.City = "Caloocan City";
                userDto.Address.PostalCode = "1422";

                if (userDto.Phone == null)
                    userDto.Phone = new Domain.DTO.PhoneDto();

                userDto.Phone.Number = "09500782401";

                var updateResponse = userService.Update(userDto, "Admin");
                Assert.AreEqual(true, updateResponse.IsSuccess);
            }
        }

        [TestMethod]
        public void UpdateUserBasicInfo_Fail()
        {
            var userService = new Ems.Services.UserService(new UnitOfWork(new EmsDevEntities()));
            var response = userService.Get(17);
            Assert.AreEqual(true, response.IsSuccess);
            if (response.IsSuccess)
            {
                var userDto = response.Data;
                userDto.FirstName = null;
                var updateResponse = userService.Update(userDto, "Admin");
                Assert.AreEqual(false, updateResponse.IsSuccess);
            }
        }

        [TestMethod]
        public void UpdateUser_NoneExistingUser_Fail()
        {
            var userService = new Ems.Services.UserService(new UnitOfWork(new EmsDevEntities()));
            var updateResponse = userService.Update(new Domain.DTO.UserDto { Id = 1 }, "Admin");
            Assert.AreEqual(false, updateResponse.IsSuccess);
        }
    }
}
