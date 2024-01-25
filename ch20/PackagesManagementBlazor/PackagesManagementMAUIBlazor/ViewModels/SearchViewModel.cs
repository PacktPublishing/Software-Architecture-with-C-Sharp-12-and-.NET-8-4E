using System.ComponentModel.DataAnnotations;

namespace PackagesManagementMAUIBlazor.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string? Location { get; set; }
    }
}
