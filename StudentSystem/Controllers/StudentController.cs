using StudentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace StudentSystem.Controllers
{
    public class StudentController : ApiController
    {
        public IList<Student> listOfStudents = new List<Student>();

        public IHttpActionResult Get()
        {
            XDocument doc = XDocument.Load("C://Students.xml");
            foreach (XElement person in doc.Descendants("Student"))
            {
                Student newStudent = new Student();
                foreach (XElement item in person.Nodes())
                {
                    if (item.Name.ToString().Equals("Id"))
                        newStudent.Id = Convert.ToInt16(item.Value);
                    else if (item.Name.ToString().Equals("Name"))
                        newStudent.Name = item.Value;
                    else
                        newStudent.Age = Convert.ToInt16(item.Value);
                }
                listOfStudents.Add(newStudent);
            }

            return Ok(listOfStudents);
        }

        // GET: Item
        //[HttpGet]
        //[Route("api/Item/GetItem/{id}")]
        public IHttpActionResult GetDataById(int id)
        {
            XDocument doc = XDocument.Load("C://Students.xml");
            //var query = from c in doc.Descendants("Band") select c;
            foreach (XElement docPerson in doc.Descendants("Id"))
            {
                if (Convert.ToInt16(docPerson.Value) == id)
                {
                    Student thisStudent = new Student();
                    foreach (XElement node in docPerson.Parent.Nodes())
                    {
                        if (node.Name.ToString().Equals("Id"))
                            thisStudent.Id = Convert.ToInt16(node.Value);
                        else if (node.Name.ToString().Equals("Name"))
                            thisStudent.Name = node.Value;
                        else
                            thisStudent.Age = Convert.ToInt16(node.Value);
                    }
                    return Ok(thisStudent);

                }
            }
            return BadRequest("Student does not exist!");

            //Student selectedStudent = listOfStudents.Where(m => m.Id == id).SingleOrDefault();
            //if (selectedStudent != null)
            //    return Ok(selectedStudent);
            //else
            //    return BadRequest("No Student Selected");
        }


        // Delete: Item
        //[HttpDelete]
        //[Route("api/Item/DeleteItem/{id}")]
        public IHttpActionResult DeleteData([FromUri]int id)
        {
            XDocument doc = XDocument.Load("C://Students.xml");

            foreach (XElement docStudent in doc.Descendants("Id"))
            {
                if (Convert.ToInt16(docStudent.Value) == id)
                {
                    docStudent.Parent.Remove();
                    doc.Save("C://Students.xml");
                    return Ok("Student successfully removed");

                }
            }
            return BadRequest("Student does not exist!");

            //Student selectedMovie = listOfStudents.Where(m => m.Id == id).SingleOrDefault();
            //if (selectedMovie != null)
            //{
            //    listOfStudents.Remove(selectedMovie);
            //    return Ok(listOfStudents);
            //    //listOfMovies.Add();
            //}
            //else
            //    return BadRequest("No Student Selected");
        }

        // Update: Item
        //[Route("api/Item/UpdateItem")]
        //[HttpPut]
        public IHttpActionResult UpdateData([FromBody]Student stu)
        {
            XDocument doc = XDocument.Load("C://Students.xml");
            //var query = from c in doc.Descendants("Band") select c;
            foreach (XElement docPerson in doc.Descendants("Id"))
            {
                if (Convert.ToInt16(docPerson.Value) == stu.Id)
                {
                    foreach (XElement node in docPerson.Parent.Nodes())
                    {
                        if (node.Name.ToString().Equals("Id"))
                            node.SetValue(stu.Id);
                        else if (node.Name.ToString().Equals("Name"))
                            node.SetValue(stu.Name);
                        else
                            node.SetValue(stu.Age);
                    }
                    doc.Save("C://Students.xml");
                    return Ok("Student successfully updated");

                }
            }
            return BadRequest("Student does not exist!");

            //Student selectedStudent = listOfStudents.Where(m => m.Id == stu.Id).SingleOrDefault();
            //if (selectedStudent != null)
            //{
            //    selectedStudent.Name = stu.Name;
            //    selectedStudent.Age = stu.Age;
            //    //listOfMovies.Remove(selectedMovie);
            //    //listOfMovies.Add();
            //}
            //return Ok(listOfStudents);
        }

        
        // Insert: Item
        //[Route("api/Item/InsertItem")]
        //[HttpPost]
        public IHttpActionResult InsertData([FromBody]Student stu)
        {
            XDocument doc = XDocument.Load("C://Students.xml");
            doc.Root.Add(new XElement("Student",
                    new XElement("Id", stu.Id),
                    new XElement("Name", stu.Name),
                    new XElement("Age", stu.Age)));
            doc.Save("C://Students.xml");

            return Ok(listOfStudents);

            //listOfStudents.Add(stu);
            //return Ok(listOfStudents);
        }
    }
}
