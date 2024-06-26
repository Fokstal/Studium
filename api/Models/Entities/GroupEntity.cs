namespace api.Models
{
    public class GroupEntity : ModelEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Guid CuratorId { get; set; }
        public string AuditoryName { get; set; } = null!;

        public List<StudentEntity> StudentEntityList { get; set; } = [];
        public List<SubjectEntity> SubjectEntityList { get; set; } = [];
    }
}