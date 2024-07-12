using System.ComponentModel.DataAnnotations.Schema;

namespace MVAPI.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Annotation
        public byte Id { get; set; }
        [MaxLength(length:100)]
        public string Name { get; set; }

    }
}
