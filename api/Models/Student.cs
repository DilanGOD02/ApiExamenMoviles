namespace api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int courseId { get; set; }

        // Propiedad de navegación
        public Course Course { get; set; }
    }
}