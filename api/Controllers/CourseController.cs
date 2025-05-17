using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Course;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [Route("api/courses")]
  [ApiController]
  public class CourseController : ControllerBase
  {
    private readonly ApplicationDBContext _context;
    private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");
    public CourseController(ApplicationDBContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var courses = await _context.Courses.ToListAsync();
      var courseDtos = courses.Select(courses => courses.ToDto());
      return Ok(courseDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getById([FromRoute] int id)
    {
      var _course = await _context.Courses.FirstOrDefaultAsync(u => u.Id == id);
      if (_course == null)
      {
        return NotFound();
      }
      return Ok(_course.ToDto());
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateCourseRequestDto courseDto)
    {
      if (courseDto.File == null || courseDto.File.Length == 0)
        return BadRequest("No file uploaded.");

      var courseModel = courseDto.ToCourseFromCreateDto();
      await _context.Courses.AddAsync(courseModel);
      await _context.SaveChangesAsync();

      var fileName = courseModel.Id.ToString() + Path.GetExtension(courseDto.File.FileName);
      var filePath = Path.Combine(_imagePath, fileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await courseDto.File.CopyToAsync(stream);
      }

      courseModel.ImageUrl = fileName;
      _context.Courses.Update(courseModel);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(getById), new { id = courseModel.Id }, courseModel.ToDto());
    }

[HttpPut]
[Route("{id}")]
public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateCourseRequestDto courseDto)
{
    var courseModel = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
    if (courseModel == null)
    {
        return NotFound();
    }

    // Actualiza los campos del curso
    courseModel.Name = courseDto.Name;
    courseModel.Description = courseDto.Description;
    courseModel.Schedule = courseDto.Schedule;
    courseModel.Professor = courseDto.Professor;

    // Si se subió una nueva imagen, reemplaza la anterior
    if (courseDto.File != null && courseDto.File.Length > 0)
{
    // Validar extensión
    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
    var fileExtension = Path.GetExtension(courseDto.File.FileName).ToLower();
    
    if (!allowedExtensions.Contains(fileExtension))
    {
        return BadRequest("Formato de imagen no válido");
    }

    // Usar Guid para evitar caché
    var fileName = $"{Guid.NewGuid()}{fileExtension}";
    var filePath = Path.Combine(_imagePath, fileName);

    // Eliminar imagen anterior si existe
    if (!string.IsNullOrEmpty(courseModel.ImageUrl))
    {
        var oldFilePath = Path.Combine(_imagePath, courseModel.ImageUrl);
        if (System.IO.File.Exists(oldFilePath))
        {
            System.IO.File.Delete(oldFilePath);
        }
    }

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await courseDto.File.CopyToAsync(stream);
    }

    courseModel.ImageUrl = fileName;
}

    _context.Courses.Update(courseModel);
    await _context.SaveChangesAsync();

    return Ok(courseModel.ToDto());
}


    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
      var courseModel = await _context.Courses.FirstOrDefaultAsync(_course => _course.Id == id);
      if (courseModel == null)
      {
        return NotFound();
      }
      _context.Courses.Remove(courseModel);

      await _context.SaveChangesAsync();

      return NoContent();
    }
  }
}
