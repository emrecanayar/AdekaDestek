using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AdekaDestek.Core.Entities.Abstract;

namespace AdekaDestek.Core.Entities.Concrete
{
    public class USER : IEntity
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string NAME { get; set; }
        [Required]
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        [Required]
        public string PASSWORD { get; set; }
        [Required]
        public int USER_DEPARTMENT_ID { get; set; }
        [Required]
        public int USER_SECTION_ID { get; set; }
        public int? USER_GROUP_ID { get; set; }
        [Required]
        public int LOCATION_ID { get; set; }
        public int? MANAGER_ID { get; set; }
        public int? DEVICE_ID { get; set; }
        [Required]
        public DateTime INSERT_DATETIME { get; set; }
        [Required]
        public string INSERT_USER { get; set; }
        [Required]
        public DateTime UPDATE_DATETIME { get; set; }
        [Required]
        public string UPDATE_USER { get; set; }
        public DateTime? LAST_LOGIN_DATETIME { get; set; }
        public DateTime? LAST_WRONG_LOGIN_TRY { get; set; }
        public string SAP_KULLANICI_ADI { get; set; }
        public int? SAP_PERSONEL_NO { get; set; }
        public string INFINI_KULLANICI_ADI { get; set; }
        [Required]
        public bool EmailAtilabilirMi { get; set; }
        [Required]
        public bool OnayaTabiMi { get; set; }
        [Required]
        public bool YONETICI_CC { get; set; }
        [Required]
        public bool ISE_DEVAM_MI { get; set; }
      
    }
}
