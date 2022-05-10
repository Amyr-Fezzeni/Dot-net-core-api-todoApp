using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace todoapp.models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public String userName { get; set; }
        public String password { get; set; }

        public List<Card> cards { get; set; }

       
    }
}
