namespace api.Models
{
    public class GroupEntity : ModelEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? CuratorId { get; set; }
        public string AuditoryName { get; set; } = null!;

        public List<StudentEntity> StudentList { get; set; } = [];
        public List<SubjectEntity> SubjectList { get; set; } = [];
    }
}