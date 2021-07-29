using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication16.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Student1Controller : ControllerBase
    {
        static List<Student> students = null;
        public Student1Controller()
        {
            if (students == null)
            {
                students = new List<Student>()
                {
                    new Student(){ Id=1, Name="Aashna", BatchCode="DotNet", Marks=90},

                    new Student(){ Id=2, Name="Priynaka", BatchCode="DotNet", Marks=87},
                    new Student(){ Id=3, Name="Tisha", BatchCode="SAP", Marks=98},
                    new Student(){ Id=4, Name="Naveen", BatchCode="SAP", Marks=90},
                    new Student(){ Id=5, Name="Siddhant", BatchCode="DotNet", Marks=90},
                    new Student(){ Id=6, Name="Vaibhav", BatchCode="DotNet", Marks=90},
                };
            }
        }

        // GET: api/<Student1Controller>
        [HttpGet]
        public IActionResult Get()
        {
            if (students != null)
                return Ok(students);
            else
                return NotFound();
        }
        


        // GET api/<Student1Controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Student student = students.FirstOrDefault(x => x.Id == id);
            if (student != null) return Ok(student);
            else
                return NotFound();
        }

        // POST api/<Student1Controller>
        [HttpPost]
        public IActionResult Post(Student student)
        {
            Student temp = students.FirstOrDefault(x => x.Id == student.Id);
            if (temp == null)
            {
                students.Add(student);
                return Created("Record Created" , student);
            }
            else
                return Ok("Duplicate");

        }

        // PUT api/<Student1Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

       

        // DELETE api/<Student1Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
