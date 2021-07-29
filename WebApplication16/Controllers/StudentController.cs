using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication16.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/<StudentController>
     
        static List<Student> students = null;
        public StudentController()
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

        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return students;
        }



        // GET: api/Student/5
        [HttpGet]
        [Route("{id:int}")]
        public Student Get(int id)
        {
            return students.FirstOrDefault(x => x.Id == id);
        }
        // POST: api/Student
        [HttpPost]
        public void Post(Student student)
        {
            students.Add(student);
        }



        // PUT: api/Student/5
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, Student student)
        {
            (from p in students
             where p.Id == id
             select p).ToList()
             .ForEach(x =>
             {
                 x.Name = student.Name;

                 x.BatchCode = student.BatchCode;
                 x.Marks = student.Marks;
             }
             );



        }

        // DELETE: api/Student/5
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            Student student = students.FirstOrDefault(x => x.Id == id);
            students.Remove(student);
        }
    }
}
