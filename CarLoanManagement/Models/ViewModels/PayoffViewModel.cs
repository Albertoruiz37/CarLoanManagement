using System.ComponentModel.DataAnnotations;

namespace CarLoanManagement.Models.ViewModels
{
    public class PayoffViewModel
    {
        public int CarId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Your Name")]
        public string Name { get; set; } = string.Empty;
    }
}