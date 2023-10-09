using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as a DataTransferObject for inserting a new person
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person name can't be blank")]
        public string? PersonName { get; set; }
        [Required(ErrorMessage = "Person email can't be blank")]
        [EmailAddress(ErrorMessage ="Email value should be a valid email")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOptions? Gender { get; set; }
        public Guid CountryId { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PersonName = this.PersonName,
                Email = this.Email,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender.ToString(),
                CountryId = this.CountryId,
                Address = this.Address,
                ReceiveNewsLetters = this.ReceiveNewsLetters
            };
        }
    }
}
