using AutoMapper;
using Ems.Domain;
using Ems.Domain.Common;
using Ems.Domain.DTO;
using Ems.Domain.Services;
using Ems.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Response<bool> Update(UserDto model, string createdBy)
        {
            Response<bool> response;
            try
            {
                var userRepo = _unitOfWork.Repository<User>();
                var user = userRepo.Query()
                                   .Filter(i => i.Id == model.Id)
                                   .Get()
                                   .FirstOrDefault();
                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.EmailAddress = model.EmailAddress;
                if (model.Phone != null)
                {
                    if (user.Phone == null)
                    {
                        user.Phone = new Phone
                        {
                            CreatedBy = createdBy,
                            CreatedDate = DateTime.UtcNow
                        };
                    }
                    user.Phone.Number = model.Phone.Number;
                    user.UpdatedBy = createdBy;
                    user.UpdatedDate = DateTime.UtcNow;
                }

                response = new Response<bool>(ResponseType.Success);
            }
            catch (Exception ex)
            {
                response = new Response<bool>(ResponseType.Error, ex.GetBaseException().Message);
            }

            return response;
        }
    }
}
