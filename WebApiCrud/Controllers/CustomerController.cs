using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCrud.Models;

namespace WebApiCrud.Controllers
{
    public class CustomerController : ApiController
    {
        WebApiEntities context = new WebApiEntities();
        
        //Get - Retrieve Data
       
        public IHttpActionResult GetAllCustomer()
        {
            IList<CustomerViewModel> customer = null;
            using (var x= new WebApiEntities())
            {
                customer = x.tblCustomers.Select(c => new CustomerViewModel()
                {
                    Id = c.Id,
                    Name=c.Name,
                    Email=c.Email,
                    Address=c.Address,
                    Phone=c.Phone,
                    Password=c.Password

                }).ToList<CustomerViewModel>();
            }
            if (customer.Count == 0)
                return NotFound();

            return Ok(customer);
         
        }
        public IHttpActionResult GetCust(int id)
        {
            CustomerViewModel obj = null;
            obj = context.tblCustomers.Where(x => x.Id == id).Select(x => new CustomerViewModel()
            {
                Id = x.Id,
                Name=x.Name,
                Email = x.Email,
                Address = x.Address,
                Phone =x.Phone,
                Password = x.Password


            }).FirstOrDefault();
            if(obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
            //Insert new
        }[HttpPost]
        public IHttpActionResult PostNewCustomer(CustomerViewModel obj)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data. please Check");
            
            using (var x= new WebApiEntities())
            {
                x.tblCustomers.Add(new tblCustomer()
                {
                    Name = obj.Name,
                    Email = obj.Email,
                    Address = obj.Address,
                    Phone = obj.Phone,
                    Password = obj.Password

                });

                x.SaveChanges();
            }
            return Ok();
        }
        //Update
        public IHttpActionResult PutCustomer(CustomerViewModel objupdt)
        {
            if (!ModelState.IsValid)
                return BadRequest("This is Invalid. Please ReCheck ");

            using (var x=new WebApiEntities())
            {
                var checkexistinguser = x.tblCustomers.Where(c => c.Id == objupdt.Id).FirstOrDefault<tblCustomer>();
                if (checkexistinguser != null)
                {
                    checkexistinguser.Name = objupdt.Name;
                    checkexistinguser.Email = objupdt.Email;
                    checkexistinguser.Address = objupdt.Address;
                    checkexistinguser.Phone = objupdt.Phone;
                    checkexistinguser.Password = objupdt.Password;

                    x.SaveChanges();

                }
                else
                {
                    return NotFound();
                }
                return Ok();
            }
        }
        //Delete
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Please Enter Valid Customer Id");

            using (var x = new WebApiEntities())
            {
                var customer = x.tblCustomers.Where(c => c.Id == id).FirstOrDefault();

                x.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }
    }
}
