using System.ComponentModel.DataAnnotations;

namespace todoapp.models
{
    public class Card
    {
        [Key]
        public string Id { get; set; }
        public string cardName { get; set; }
        public string cardDescription { get; set; }
        public string cardImportance { get; set; }
        
        public string userId { get; set; }
    }
}
 