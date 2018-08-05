using AutoMapper;
using Ems.Domain;
using Ems.Domain.Common;
using Ems.Domain.DTO;
//using Ems.Domain.Entities;
using Ems.Domain.Services;
using Ems.Service.Cryptography;
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
        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Response CreateCompanyWithUser(CompanyDto model, string createdBy)
        {
            Response response;
            try
            {
                var companyUnitWork = _unitOfWork.Repository<Company>();
                Company company = Mapper.Map<Company>(model);
                company.CreatedBy = createdBy;
                company.CreatedDate = DateTime.UtcNow;
                company.CompanyCode = $"COMP-{companyUnitWork.Get().Count()}";

                if (company.Address != null)
                {
                    company.Address.CreatedBy = createdBy;
                    company.Address.CreatedDate = DateTime.UtcNow;
                }

                foreach (var companyUser in company.Users)
                {
                    companyUser.CreatedBy = createdBy;
                    companyUser.CreatedDate = DateTime.UtcNow;
                    if (companyUser.Address != null)
                    {
                        companyUser.Address.CreatedBy = createdBy;
                        companyUser.Address.CreatedDate = DateTime.UtcNow;
                    }
                    if (companyUser.UserAccount != null)
                    {
                        companyUser.UserAccount.CreatedBy = createdBy;
                        companyUser.UserAccount.CreatedDate = DateTime.UtcNow;
                        companyUser.UserAccount.Salt = Cryptography.CreateSalt();
                        companyUser.UserAccount.Password = Hashbrowns.Hash(companyUser.UserAccount.Password, companyUser.UserAccount.Salt);
                    }
                }
                companyUnitWork.Insert(company);
                _unitOfWork.Save();
                response = new Response(ResponseType.Success);
            }
            catch (Exception e)
            {
                response = new Response(ResponseType.Error, e.GetBaseException().Message);
            }
            return response;
        }
    }
}
