using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdekaDestek.Core.Entities.Abstract;

namespace AdekaDestek.Entities.Dtos
{
    public class UserUpdateDto : DtoGetBase
    {
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("E-posta")]
        public string Email { get; set; }
        [DisplayName("Cep Telefonu")]
        public string PhoneNumber { get; set; }
        [DisplayName("Adı")]
        public string FirstName { get; set; }
        [DisplayName("Soyadı")]
        public string LastName { get; set; }
        [DisplayName("İnfini Kullanıcı Adı")]
        public string InfiniUserName { get; set; }
        [DisplayName("Sap Kullanıcı Adı")]
        public string SapUserName { get; set; }
        [DisplayName("Sap Personel No")]
        public string SapEmployeeNo { get; set; }

    }
}
