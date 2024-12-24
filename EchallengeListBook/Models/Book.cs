using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EchallengeListBook.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id autoincrementer 
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Le titre doit contenir au moins 2 caractères.")]
        public string Title { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "L'auteur doit contenir au moins 3 caractères.")]
        public string Author { get; set; }

        [Range(1000, 9999, ErrorMessage = "L'année de publication doit être valide.")]
        public int Year { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,13}$", ErrorMessage = "L'ISBN doit contenir entre 10 et 13 chiffres.")]
        public string ISBN { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le nombre d'exemplaires doit être positif.")]
        public int CopiesAvailable { get; set; }
    }
}
