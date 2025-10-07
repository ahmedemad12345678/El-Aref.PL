﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace El_Aref.PL.DTO
{
    public class CreateEmployeeTDO
    {
        internal int? DepartmentId;

        [Required(ErrorMessage = "Name is required !!")]
        public string Name { get; set; }
        [Range(22, 60, ErrorMessage = "Age must be between 22 and 60 ")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email not valid !!")]
        public string Email { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,9}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-street-city-country")]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }


        public bool IsDeleted { get; set; }
        [DisplayName("Huring Date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("create At")]
        public DateTime CreateAt { get; set; }
        public string? ImageName { get; set; }

        public IFormFile? Image { get; set; }
    }
}
