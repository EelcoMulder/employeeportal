namespace EmployeePortal.SkillManagement.Domain.Model
{
    public enum SkillLevel
    {
        Basic,
        Average,
        Good,
        Excellent
    }

    public class Skill
    {
        public string Name { get; set; }
        public SkillLevel Level { get; set; }
        public int Id { get; set; }
    }
}