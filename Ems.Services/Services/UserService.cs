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

namespace Ems.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Response<UserDto> Get(int id)
        {
            Response<UserDto> response;
            try
            {
                var userRepo = _unitOfWork.Repository<User>();
                var user = userRepo.Query()
                       .Filter(i => i.Id == id)
                       .Get();
                if (user.Any())
                {
                    response = new Response<UserDto>(ResponseType.Success, string.Empty, Mapper.Map<User,UserDto>(user.FirstOrDefault()));
                }else
                {
                    response = new Response<UserDto>(ResponseType.Error, EmsResousrce.ErrMsgNoUserFound);
                }
            }catch(Exception ex)
            {
                response = new Response<UserDto>(ResponseType.Error, ex.GetBaseException().Message);
            }

            return response;
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
                if(user != null)
                {
                    user.FirstName = model.FirstName;
                    user.MiddleName = model.MiddleName;
                    user.LastName = model.LastName;
                    user.EmailAddress = model.EmailAddress;
                    user.UpdatedDate = DateTime.UtcNow;
                    user.UpdatedBy = createdBy;
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
                    if (model.Address != null)
                    {
                        if (user.Address == null)
                        {
                            user.Address = new Address()
                            {
                                CreatedBy = createdBy,
                                CreatedDate = DateTime.UtcNow
                            };
                        }
                        user.Address.StreetAddress = model.Address.StreetAddress;
                        user.Address.StreetAddress2 = model.Address.StreetAddress2;
                        user.Address.City = model.Address.City;
                        user.Address.PostalCode = model.Address.PostalCode;
                        user.Address.UpdatedBy = createdBy;
                        user.Address.UpdatedDate = DateTime.UtcNow;
                    }
                    _unitOfWork.Save();
                    response = new Response<bool>(ResponseType.Success,string.Empty,true);
                }else
                {
                    response = new Response<bool>(ResponseType.Error, EmsResousrce.ErrMsgUserNotFound);
                }

            }
            catch (Exception ex)
            {
                response = new Response<bool>(ResponseType.Error, ex.GetBaseException().Message);
            }

            return response;
        }
    }
}
