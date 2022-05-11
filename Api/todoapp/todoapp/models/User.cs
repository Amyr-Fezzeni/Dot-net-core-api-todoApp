using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace todoapp.models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public bool isLogedIn { get; set; }
        
        

       
    }
}
