using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string ProducerName { get; set; }
        public string Bio { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
