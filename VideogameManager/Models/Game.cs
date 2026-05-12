using System;
using System.ComponentModel.DataAnnotations;

namespace VideogameManager.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Must input a title")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Must input a genre")]
        public string Genre { get; set; } = string.Empty;

        [Range(1970, 2030, ErrorMessage = "Not valid year")]
        public int Year { get; set; }

        [Range(0d, 10d, ErrorMessage = "Score must be between 0 and 10")]
        public double Score { get; set; }

        public string? Description { get; set; } = string.Empty;
        public int DeveloperId { get; set; }

        public Developer? Developer { get; set; }

    }
}
