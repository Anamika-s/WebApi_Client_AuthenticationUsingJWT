using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using WebApplication16.Models;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace WebApplication16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesADOController : ControllerBase
    {
        IConfiguration _config;
        public EmployeesADOController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Employees
        [HttpGet]
        [Route("GetUsers")]
        public List<UsersModel> GetUsers()
        {
            return LoadUsers().ToList();
        }

        [HttpGet]
        [Route("GetUserById")]
        public UsersModel GetUserById(int id)
        {
            return LoadUsers().Where(x => x.Id == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("InsertUser")]
        public IActionResult InsertUser(UsersModel user)
        {

            SqlConnection connection = new SqlConnection(_config.GetConnectionString("UserConnectionString"));
            SqlCommand cmd = new SqlCommand("Insert into users(id, name, emailid, mobile,address,isactive) values (@id, @name,@emailid,@mobile,@address,@isactive)", connection);
            //string Query = "Insert into users(id, name, emailid, mobile,address,isactive) values (user.id + ','" + user.Name + ",'" + user.EmailId + ",'" + user.Mobile + ",'" + user.Address + ",' + user.IsActive)";
            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@address", user.Address);
            cmd.Parameters.AddWithValue("@mobile", user.Mobile);
            cmd.Parameters.AddWithValue("@emailid", user.EmailId);
            cmd.Parameters.AddWithValue("@isactive", user.IsActive);



            // SqlCommand cmd = new SqlCommand(Query, connection);
            
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(user);

        }


 
        [HttpPut("{id}")]
        [Route("EditUser")]
        public IActionResult PutUser(int id, UsersModel user)
        {

            SqlConnection connection = new SqlConnection(_config.GetConnectionString("UserConnectionString"));
            string Quey = "Update users set name ='" + user.Name + "', address = '" + user.Address + "' where id  =  "+ id ;


            SqlCommand cmd = new SqlCommand(Quey, connection); 
           
            // SqlCommand cmd = new SqlCommand(Query, connection);

            //string qry = "Select * from users WHERE [Name]='" + pLoginName + "' AND [Pwd]='" + pPassword + "'";

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return Ok(user);

        }


        [HttpDelete("{id}")]
        [Route("DeleteUser")]
        public IActionResult Delete(int id)
        {

            SqlConnection connection = new SqlConnection(_config.GetConnectionString("UserConnectionString"));
            SqlCommand cmd = new SqlCommand("'Delete from users where id= '+ id ",connection);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return Ok();

        }

        private List<UsersModel> LoadUsers()
        {
            List<UsersModel> users = new List<UsersModel>();
            SqlConnection connection = new SqlConnection(_config.GetConnectionString("UserConnectionString"));
            SqlCommand cmd = new SqlCommand("Select * from users", connection);
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            ada.Fill(dataTable);
            for(int i=0;i<dataTable.Rows.Count;i++)
            {
                UsersModel user = new UsersModel();
                user.Id = (int)dataTable.Rows[i]["Id"];
                user.Name = dataTable.Rows[i]["Name"].ToString();
                user.EmailId = dataTable.Rows[i]["EmailId"].ToString();
                user.Address = dataTable.Rows[i]["Address"].ToString();
                user.IsActive = (bool)dataTable.Rows[i]["isActive"];
                users.Add(user);
            }
            return users;
        }

        //// GET: api/Employees/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(employee);
        //    }
        //}

        //// PUT: api/Employees/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(int id, Employee employee)
        //{
        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Employees
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    _context.Employees.Add(employee);   
        //    await _context.SaveChangesAsync();

        //    return Ok(employee);
        //}

        //// DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return employee;
        //}

        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
    }
}
