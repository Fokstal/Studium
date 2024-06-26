namespace api.Models
{
    public class PersonEntity : ModelEntity
    {
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; } = new();
        public int Sex { get; set; }
        public string AvatarFileName { get; set; } = null!;

        public PassportEntity? PassportEntity { get; set; }
        public StudentEntity? StudentEntity { get; set; }
    }
}