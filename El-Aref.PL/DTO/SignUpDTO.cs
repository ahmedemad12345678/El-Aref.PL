using System.ComponentModel.DataAnnotations;

namespace El_Aref.PL.DTO
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "UserName Is Required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FristName Is Required !!")]
        public string FristName { get; set; }

        [Required(ErrorMessage = "LastName  Is Required !!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required !!")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password) ,ErrorMessage = "Confirm Password does not match the Password !!")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree{ get; set; }
    }
}
