using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHealth.Data.Entities
{
    /// <summary>
    /// Данные верификации телефона
    /// </summary>
    [Table("PhoneVerificationData")]
    public class PhoneVerificationData : EntityBase<PhoneVerificationData>
    {
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(6)]
        public string OTP { get; set; }

        public bool Сonfirmed { get; set; }
    }
}
