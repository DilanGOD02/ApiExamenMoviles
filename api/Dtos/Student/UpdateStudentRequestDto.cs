using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Student
{
    public class UpdateStudentRequestDto
    {
        public string Name {get; set; }
        public string Email {get; set; }
        public string Phone {get; set; }
        public int courseId {get; set; }
    }
}
