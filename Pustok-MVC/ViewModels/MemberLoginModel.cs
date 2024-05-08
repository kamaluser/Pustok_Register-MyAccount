using System.ComponentModel.DataAnnotations;

namespace Pustok_MVC.ViewModels
{
    public class MemberLoginModel
    {
        [MinLength(5)]
        [MaxLength(25)]
        public string UserName { get; set; }
        [MinLength(8)]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
