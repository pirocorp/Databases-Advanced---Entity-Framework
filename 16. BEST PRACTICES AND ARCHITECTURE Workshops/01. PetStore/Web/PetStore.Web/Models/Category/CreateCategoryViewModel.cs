namespace PetStore.Web.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCategoryViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
