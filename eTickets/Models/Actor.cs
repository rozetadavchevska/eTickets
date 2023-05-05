using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace eTickets.Models
{
    public class Actor:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        public string? ProfilePictureUrl { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]
        public string ActorName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public List<ActorMovie>? ActorsMovies { get; set; }
    }
}
