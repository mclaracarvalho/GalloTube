using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GalloFlix.Models;

[Table("Video")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "O Título é obrigatório")]
    [StringLength(100, ErrorMessage = "O Título deve possuir no máximo 100 caracteres")]
    public string Title { get; set; }

    [Display(Name = "Descrição")]
    [Required(ErrorMessage = "A Descrição é obrigatória")]
    [StringLength(100, ErrorMessage = "A Descrição deve possuir no máximo 100 caracteres")]

    [Display(Name = "Duração (em minutos)")]
    [Required(ErrorMessage = "A Duração é obrigatória")]
    public Int16 Duration { get; set; }

    [Display(Name = "Classificação Etária")]
    [Required(ErrorMessage = "A Classificação Etária é obrigatória")]
    public byte AgeRating { get; set; }

    [StringLength(200)]
    [Display(Name = "Foto")]
    public string Image { get; set; }

    [NotMapped]
    [Display(Name = "Duração")]
    public string HourDuration { get {
        return TimeSpan.FromMinutes(Duration) .ToString(@"%h'h 'mm'min'");
    }}

    public ICollection<MovieComment> Comments { get; set; }
    public ICollection<MovieGenre> Genres { get; set; }
    public ICollection<MovieRating> Ratings { get; set; }

}