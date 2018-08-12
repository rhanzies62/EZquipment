using Ems.Domain;
using Ems.Domain.Common;
using Ems.Domain.DTO;
using Ems.Domain.Services;
using Ems.Service.Cryptography;
using Ems.Services.Config;
using Ems.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Services
{
    public class AuthenticateService : BaseService, IAuthenticateService
    {
        public AuthenticateService(IUnitOfWork _unitOfWork) : base(_unitOfWork)
        {

        }

        public Response<LoginInfoDto> Login(UserAccountDto userAccountDto)
        {
            Response<LoginInfoDto> response;
            try
            {
                int maxAttempt = int.Parse(Utility.GetConfig(EmsResousrce.ConfigMaxLoginAttempt));
                var userRepo = _unitOfWork.Repository<User>();
                var user = userRepo.Query().Filter(i => i.UserAccount.UserName == userAccountDto.UserName).Get().FirstOrDefault();
                if (user != null)
                {
                    if (user.UserAccount.MaxLoginAttempt < maxAttempt)
                    {
                        var passwordHash = Hashbrowns.Hash(userAccountDto.Password, user.UserAccount.Salt);
                        if (user.UserAccount.Password == passwordHash)
                        {
                            response = new Response<LoginInfoDto>(ResponseType.Success, string.Empty, new LoginInfoDto
                            {
                                EmailAddress = user.EmailAddress,
                                UserId = user.Id,
                                UserName = user.UserAccount.UserName,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Roles = user.UserRoles.Select(i => new RoleDto
                                {
                                    RoleId = i.RoleId,
                                    RoleName = i.Role.Name
                                })
                            });
                        }
                        else
                        {
                            user.UserAccount.MaxLoginAttempt++;
                            response = new Response<LoginInfoDto>(ResponseType.Error, EmsResousrce.ErrMsgInvalidPassword, new LoginInfoDto { UserId = user.Id });
                        }
                    }
                    else
                    {
                        response = new Response<LoginInfoDto>(ResponseType.Error, EmsResousrce.ErrMsgInvalidLoginAttempt, new LoginInfoDto { UserId = user.Id });
                    }
                }
                else
                {
                    response = new Response<LoginInfoDto>(ResponseType.Critical, EmsResousrce.ErrMsgNoUserFound, new LoginInfoDto());
                }

                if (response.StatusType == ResponseType.Error || response.StatusType == ResponseType.Success)
                {
                    _unitOfWork.Repository<LogInLog>().Insert(new LogInLog
                    {
                        CreatedBy = EmsResousrce.DefaultCreatedBy,
                        CreatedDate = DateTime.UtcNow,
                        ErrorMessage = response.Message,
                        IPAddress = userAccountDto.IPAddress,
                        IsSuccess = response.IsSuccess,
                        UserId = response.Data.UserId
                    });
                }
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response<LoginInfoDto>(ResponseType.Critical, e.GetBaseException().Message);
            }

            return response;
        }
    }
}
