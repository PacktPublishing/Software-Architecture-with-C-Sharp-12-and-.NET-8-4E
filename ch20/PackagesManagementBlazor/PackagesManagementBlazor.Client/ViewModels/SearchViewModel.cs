using System.ComponentModel.DataAnnotations;

namespace PackagesManagementBlazor.Client.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string? Location { get; set; } 
    }
}
