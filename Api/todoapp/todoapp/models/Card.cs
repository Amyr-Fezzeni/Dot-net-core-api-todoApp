using System.ComponentModel.DataAnnotations;

namespace todoapp.models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string cardName { get; set; }
        public string cardDescription { get; set; }
        public string cardImportance { get; set; }
        
    }
}
 