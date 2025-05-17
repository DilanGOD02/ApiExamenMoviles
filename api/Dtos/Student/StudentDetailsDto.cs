namespace api.Dtos.Student
{
    public class StudentDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int courseId { get; set; }

        // Datos del curso
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseSchedule { get; set; }
        public string CourseProfessor { get; set; }
        public string CourseImageUrl { get; set; }
    }
}