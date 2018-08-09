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

        public Response Login(UserAccountDto userAccountDto)
        {
            Response response;
            try
            {
                int maxAttempt = int.Parse(Utility.GetConfig(EmsResousrce.ConfigMaxLoginAttempt));
                var userAccountRepo = _unitOfWork.Repository<UserAccount>();
                var userAccount = userAccountRepo.Query().Filter(i => i.UserName == userAccountDto.UserName).Get().FirstOrDefault();
                if (userAccount != null)
                {
                    if (userAccount.MaxLoginAttempt < maxAttempt)
                    {
                        var passwordHash = Hashbrowns.Hash(userAccountDto.Password, userAccount.Salt);
                        if (userAccount.Password == passwordHash)
                        {
                            response = new Response(ResponseType.Success, string.Empty, new LoginInfoDto
                            {
                                EmailAddress = userAccount.Users.First().EmailAddress,
                                UserId = userAccount.Users.First().Id,
                                UserName = userAccount.UserName
                            });
                        }
                        else
                        {
                            userAccount.MaxLoginAttempt++;
                            response = new Response(ResponseType.Error, EmsResousrce.ErrMsgInvalidPassword, new LoginInfoDto { UserId = userAccount.Users.First().Id });
                        }
                    }
                    else
                    {
                        response = new Response(ResponseType.Error, EmsResousrce.ErrMsgInvalidLoginAttempt);
                    }
                }
                else
                {
                    response = new Response(ResponseType.Error, EmsResousrce.ErrMsgNoUserFound);
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
                        UserId = response.Data?.UserId
                    });
                }
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                response = new Response(ResponseType.Critical, e.GetBaseException().Message);
            }

            return response;
        }
    }
}
