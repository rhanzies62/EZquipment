﻿using AutoMapper;
using Ems.Domain;
using Ems.Domain.Common;
using Ems.Domain.DTO;
//using Ems.Domain.Entities;
using Ems.Domain.Services;
using Ems.ExternalServices.Interface;
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
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly IEmailService _emailService;
        public CompanyService(IUnitOfWork unitOfWork, IEmailService emailService) : base(unitOfWork)
        {
            _emailService = emailService;
        }

        public Response CreateCompanyWithUser(CompanyDto model, string createdBy)
        {
            Response response;
            try
            {
                var companyRepo = _unitOfWork.Repository<Company>();
                var userRepo = _unitOfWork.Repository<User>();
                string fullName = string.Empty,
                       validationToken = Cryptography.CreateSalt();

                Company company = Mapper.Map<Company>(model);
                company.CreatedBy = createdBy;
                company.CreatedDate = DateTime.UtcNow;
                company.CompanyCode = $"COMP-{companyRepo.Get().Count()}";

                if (companyRepo.Query().Filter(i => i.Name == company.Name).Get().Any())
                {
                    response = new Response(ResponseType.Error, EmsResousrce.ErrMsgCompanyNameAlreadyExist);
                    return response;
                }

                string email = model.Users?.FirstOrDefault().EmailAddress;
                if (userRepo.Query().Filter(i => i.EmailAddress == email).Get().Any())
                {
                    response = new Response(ResponseType.Error, EmsResousrce.ErrMsgEmailAddressAlreadyExist);
                    return response;
                }

                string username = model.Users?.FirstOrDefault().UserAccount?.UserName;
                if (userRepo.Query().Filter(i => i.UserAccount.UserName == username).Get().Any())
                {
                    response = new Response(ResponseType.Error, EmsResousrce.ErrMsgUserNameAlreadyExist);
                    return response;
                }

                if (company.Address != null)
                {
                    company.Address.CreatedBy = createdBy;
                    company.Address.CreatedDate = DateTime.UtcNow;
                }

                foreach (var user in company.Users)
                {
                    fullName = $"{user.FirstName} {user.LastName}";
                    user.CreatedBy = createdBy;
                    user.CreatedDate = DateTime.UtcNow;
                    user.ValidationToken = validationToken;
                    if (user.Address != null)
                    {
                        user.Address.CreatedBy = createdBy;
                        user.Address.CreatedDate = DateTime.UtcNow;
                    }
                    if (user.UserAccount != null)
                    {
                        user.UserAccount.CreatedBy = createdBy;
                        user.UserAccount.CreatedDate = DateTime.UtcNow;
                        user.UserAccount.Salt = Cryptography.CreateSalt();
                        user.UserAccount.Password = Hashbrowns.Hash(user.UserAccount.Password, user.UserAccount.Salt);
                    }
                }
                companyRepo.Insert(company);
                _unitOfWork.Save();

                string validationLink = $"{Utility.GetConfig(EmsResousrce.ConfigBaseUrl)}validate?t={validationToken}";
                _emailService.SendEmailActivation(fullName, validationLink, email);
                response = new Response(ResponseType.Success);
            }
            catch (Exception e)
            {
                response = new Response(ResponseType.Error, e.GetBaseException().Message);
            }
            return response;
        }

        public Response ResendEmailActivation(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public Response ValidateEmail(string validationToken)
        {
            Response response;
            try
            {
                if (string.IsNullOrWhiteSpace(validationToken))
                {
                    response = new Response(ResponseType.Error, EmsResousrce.ErrMsgInvalidValidationToken);
                    return response;
                }

                var userRepo = _unitOfWork.Repository<User>();
                var user = userRepo.Query().Filter(i => i.ValidationToken == validationToken).Get().FirstOrDefault();
                if (user != null)
                {
                    user.IsEmailValidated = true;
                    user.EmailValidatedDate = DateTime.UtcNow;
                    user.UserAccount.IsActive = true;
                    userRepo.Update(user);
                    _unitOfWork.Save();
                    response = new Response(ResponseType.Success);
                }
                else
                {
                    response = new Response(ResponseType.Error, EmsResousrce.ErrMsgInvalidValidationToken);
                }

            }
            catch (Exception e)
            {
                response = new Response(ResponseType.Error, e.GetBaseException().Message);
            }
            return response;
        }
    }
}
