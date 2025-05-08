using api.Dtos.Student;
using api.Models;

namespace api.Mappers
{
  public static class StudentMapper
  {
    public static StudentDto ToDto(this Student studentItem)
    {
      return new StudentDto
      {
        Id = studentItem.Id,
        Name = studentItem.Name,
        Email = studentItem.Email,
        Phone = studentItem.Phone,
        courseId = studentItem.courseId
      };
    }
   
    public static Student ToStudentFromCreateDto(this CreateStudentRequestDto createUserRequest)
    {
      return new Student
      {
        Name = createUserRequest.Name,
        Email = createUserRequest.Email,
        Phone = createUserRequest.Phone,
        courseId = createUserRequest.courseId
        
      };
    }
  }
}