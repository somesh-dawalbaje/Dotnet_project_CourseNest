namespace CourseNest.Models.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int DurationMonths { get; set; }

        public string Fees { get; set; }

        public string Description { get; set; }
    }
}
