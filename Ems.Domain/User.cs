//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ems.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class User : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.LogInLogs = new HashSet<LogInLog>();
            this.UserRoles = new HashSet<UserRole>();
        }
    
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> AddressId { get; set; }
        public int UserAccountId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int CompanyId { get; set; }
        public Nullable<int> PhoneId { get; set; }
        public string ValidationToken { get; set; }
        public bool IsEmailValidated { get; set; }
        public Nullable<System.DateTime> EmailValidatedDate { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogInLog> LogInLogs { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual Phone Phone { get; set; }
    }
}
