using DemoG01.Entities;
using DemoG01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoG01.Controllers
{
    [Route("api/[controller]")] // Domain/Department => URL + Verb
    [ApiController]
    public class DepartmentController : ControllerBase // Res according Status Code
    {
        CompanyContext db;
        public DepartmentController(CompanyContext _db)
        {
            db = _db;   
        }

        [HttpGet]  
        public IActionResult GetAll() 
        {
            List<Department> debtList = db.Departments.ToList();
            return Ok(debtList);
        }

        [HttpGet("{id:int}", Name = "GetOne")]
        public IActionResult Details(int id)
        {
            Department debt = db.Departments.Find(id);
            return Ok(debt);
        }

        [HttpGet("{name:alpha}")] //Domain/Employee/{name}
        public IActionResult GetByName(string name)
        {
            Department debt = db.Departments.FirstOrDefault(d=>d.Name == name);
            return Ok(debt);
        }

        //public ActionResult<List<Department>> GetAll()
        //{
        //    List<Department> debtList = db.Departments.ToList();
        //    return debtList;
        //}

        ////////////////////////////////////////////////////////////////////////////
        ///

        [HttpPost]
        public IActionResult AddDept(Department dept)
        {
            if (ModelState.IsValid) 
            { 
                db.Departments.Add(dept);
                db.SaveChanges();
                //return Ok(db.Departments.ToList());
                string url = Url.Link("GetOne", new { id = dept.Id });
                //return Created("http://localhost:5187/api/Department/" + dept.Id, dept);
                return Created(url, new {department=dept, message="Department Added Successfully"});
            
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Department dept, int id) //In case Premitive DataType => [Route Data /or Query String] | and In case Complex DataType => Body
        {
            if (ModelState.IsValid)
            {
                //db.Departments.Update(dept);
                Department OldDept = db.Departments.Find(id);
                if (OldDept != null)
                {
                    OldDept.Name = dept.Name;
                    OldDept.MangerName = dept.MangerName;
                    db.SaveChanges();
                    return NoContent();
                }

               
                //return StatusCode(204, new{ Message = "Department Updated Successfully", dept=dept.Id});
            }
            return BadRequest(ModelState);  
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id) 
        {
            Department dept = db.Departments.Find(id);
            if (dept != null)
            {
                db.Departments.Remove(dept);
                db.SaveChanges();
                return Ok(new
                {
                    message = $"Development with id = {dept.Id} deleted !!",
                    dept = dept,
                    depts = db.Departments.ToList()
                });
            }
            return NotFound();
        }

        //public IActionResult PostDept()
        //{

        //}
    }
}
