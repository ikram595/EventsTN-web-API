using System.ComponentModel.DataAnnotations;

namespace EventsTN.Models
{
    public class Event
    {
        public int EventId { get; set; }
        [Required(ErrorMessage = "champ obligatoire")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Champ doit être de longueur comprise entre 3 et 100 caractères")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "champ obligatoire")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Champ doit être de longueur comprise entre 10 et 500 caractères")]
        public string Description { get; set; } = null!;
        public int Limit { get; set; }
        public string ImgUrl { get; set; } = null!;
        [Required(ErrorMessage = "champ obligatoire")]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Format de date non valide. Utilisez le format jj/mm/aaaa.")]
        public string StartDate { get; set; } = null!;
        [Required(ErrorMessage = "champ obligatoire")]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Format de date non valide. Utilisez le format jj/mm/aaaa.")]
        public string EndDate { get; set; } = null!;
        [Required(ErrorMessage = "champ obligatoire")]
        [RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "Format d'heure non valide. Utilisez le format HH:mm.")]
        public string StartTime { get; set; }
        [RegularExpression(@"^\d{2}:\d{2}$", ErrorMessage = "Format d'heure non valide. Utilisez le format HH:mm.")]
        public string EndTime { get; set; }
        [Required(ErrorMessage = "champ obligatoire")]
        public string Location { get; set; } = null!;
        [Required(ErrorMessage = "champ obligatoire")]
        [EnumDataType(typeof(EventType), ErrorMessage = "Type d'événement non valide")]
        public string Type { get; set; } = null!;
        [Required(ErrorMessage = "champ obligatoire")]
        [EnumDataType(typeof(EventStatus), ErrorMessage = "Statut d'événement non valide")]
        public string Status { get; set; } = null!;
        [Required(ErrorMessage = "champ obligatoire")]
        [EnumDataType(typeof(EventCategory), ErrorMessage = "Catégorie d'événement non valide")]
        public string Category { get; set; } = null!;
        [Required(ErrorMessage = "champ obligatoire")]
        [EnumDataType(typeof(EventProperties), ErrorMessage = "Propriétés d'événement non valides")]
        public string Properties { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
    public enum EventType
    {
        InPerson,
        Online
    }
    public enum EventStatus
    {
        Published,
        Drafted,
        Cancelled
    }

    public enum EventCategory
    {
        Education,
        SocialImpact,
        Tech,
        Entertainment,
        Other
    }
    [Flags]
    public enum EventProperties
    {
        Training = 1,
        Hackathon = 2,
        Workshop = 4,
        Conference = 8,
        Meetup = 16,
        Seminar = 32,
        Talk = 64
    }
}
