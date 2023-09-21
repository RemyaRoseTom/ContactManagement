namespace ContactManager.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    [Authorize]
    public partial class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+0?1\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email ID is Required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
