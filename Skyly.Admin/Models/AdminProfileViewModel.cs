using System.ComponentModel.DataAnnotations;
using System;

namespace Skyly.Admin.Models
{
    public class AdminProfileViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string FullName { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور غير متطابقة")]
        public string ConfirmPassword { get; set; }
    }

}
