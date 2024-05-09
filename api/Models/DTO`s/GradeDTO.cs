using System.ComponentModel.DataAnnotations;

namespace api.Model.DTO
{
    public class GradeDTO
    {
        public int Id { get; set; }

        [Required]
        [Range(-1, 10)]
        public int Value { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int SubjectId { get; set; }
    }
}