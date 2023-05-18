using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        public string? ProfilePictureUrl { get; set; }
        [Display(Name = "Full Name")]
        public string ProducerName { get; set; }
        [Display(Name = "Biography")]
        public string Bio { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
