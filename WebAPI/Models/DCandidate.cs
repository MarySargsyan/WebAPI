using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class DCandidate
    {
        public Guid id { get; set; }

        public string fullName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }

        public int age { get; set; }
        public string bloogGroop { get; set; }
        public string address { get; set; }

    }
}
