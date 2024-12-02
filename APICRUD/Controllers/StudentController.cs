using APICRUD.Entities_;
using APICRUD.Infratructure.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }

        [HttpGet(nameof(Getdetails))]
        public async Task<IActionResult> Getdetails()
        {

            var i = await _student.GetStudents();
            return Ok(i);
        }

        [HttpPost(nameof(Add))]
        public async Task<IActionResult> Add(Student student)
        {
            var i = await _student.Save(student);
            return Ok(i);
        }
        [HttpDelete ("Remove/{id}")]
        
        public async Task<IActionResult> Remove(int id)
        {
            var i = await _student.Delete(id);
            return Ok(i);
        }

        [HttpGet(nameof(Edit))]
        public async Task<IActionResult> Edit(int id)
        {
            var i = await _student.Edit(id);
            return Ok(i);
        }
    }
}
