using RealEstateWebApp.UI.Resources;
using System.ComponentModel.DataAnnotations;

namespace RealEstateWebApp.UI.ViewModels
{
    public class CommentViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resource))]
        [RegularExpression("^[0-9]*$", ErrorMessageResourceName = "IncorrectField", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(12, MinimumLength = 10, ErrorMessageResourceName = "IncorrectLength", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Comment", ResourceType = typeof(Resource))]

        public string Comment { get; set; }
    }

}
