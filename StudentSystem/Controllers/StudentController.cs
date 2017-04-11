using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentSystem.Controllers
{
    public class StudentController : ApiController
    {
        public IList<Student> listOfStudents = new List<Student>();
        public IHttpActionResult Get()
        {
            listOfStudents.Add(new Student() { Id = 1, Name = "One", Age = 15 });
            listOfStudents.Add(new Student() { Id = 2, Name = "Two", Age = 20 });
            listOfStudents.Add(new Student() { Id = 3, Name = "Three", Age = 18 });
            listOfStudents.Add(new Student() { Id = 4, Name = "Four", Age = 17 });
            return Ok(listOfStudents);
        }

        // GET: Item
        //[HttpGet]
        //[Route("api/Item/GetItem/{id}")]
        public IHttpActionResult GetDataById(int id)
        {
            Student selectedStudent = listOfStudents.Where(m => m.Id == id).SingleOrDefault();
            if (selectedStudent != null)
                return Ok(selectedStudent);
            else
                return BadRequest("No Student Selected");
        }


        // Delete: Item
        //[HttpDelete]
        //[Route("api/Item/DeleteItem/{id}")]
        public IHttpActionResult DeleteData([FromUri]int id)
        {
            Student selectedMovie = listOfStudents.Where(m => m.Id == id).SingleOrDefault();
            if (selectedMovie != null)
            {
                listOfStudents.Remove(selectedMovie);
                return Ok(listOfStudents);
                //listOfMovies.Add();
            }
            else
                return BadRequest("No Student Selected");
        }

        // Update: Item
        //[Route("api/Item/UpdateItem")]
        //[HttpPut]
        public IHttpActionResult UpdateData([FromBody]Student stu)
        {
            Student selectedStudent = listOfStudents.Where(m => m.Id == stu.Id).SingleOrDefault();
            if (selectedStudent != null)
            {
                selectedStudent.Name = stu.Name;
                selectedStudent.Age = stu.Age;
                //listOfMovies.Remove(selectedMovie);
                //listOfMovies.Add();
            }
            return Ok(listOfStudents);
        }

        
        // Insert: Item
        //[Route("api/Item/InsertItem")]
        //[HttpPost]
        public IHttpActionResult InsertData([FromBody]Student stu)
        {
            listOfStudents.Add(stu);
            return Ok(listOfStudents);
        }
    }
}
