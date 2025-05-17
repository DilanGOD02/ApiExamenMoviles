namespace api.Models{
  public class Course
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Schedule { get; set; }
    public string Professor { get; set; }
  }
}
