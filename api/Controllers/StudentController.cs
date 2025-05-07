using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Student;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StudentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            var studentDtos = students.Select(student => student.ToDto());
            return Ok(studentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequestDto studentDto)
        {
            var studentModel = studentDto.ToStudentFromCreateDto();
            await _context.Students.AddAsync(studentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = studentModel.Id }, studentModel.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStudentRequestDto studentDto)
        {
            var studentModel = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            studentModel.Name = studentDto.Name;
            studentModel.Email = studentDto.Email;
            studentModel.Phone = studentDto.Phone;
            studentModel.courseId = studentDto.courseId;

            await _context.SaveChangesAsync();

            return Ok(studentModel.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var studentModel = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            _context.Students.Remove(studentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
