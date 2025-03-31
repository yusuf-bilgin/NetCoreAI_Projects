using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetCoreAI.Project01_ApiDemo.Context;
using NetCoreAI.Project01_ApiDemo.Entities;

namespace NetCoreAI.Project01_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _context;

        public CustomersController(ApiContext context)
        {
            _context = context;
        }

        // Her bir api metodu bir sekilde cagirilacak
        [HttpGet] //Apide istek atilirken istegin turu mutlaka belirtilmeli
        public IActionResult CustomerList()
        {
            var values = _context.Customers.ToList();
            return Ok(values); // Mesaj kutusu ok metoduyla geliyor
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Müşteri Ekleme Başarılı"); // Response body
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            _context.Customers.Remove(value);
            _context.SaveChanges();
            return Ok("Müşteri Başarıyla Silindi");
        }

        [HttpGet("GetCustomer")]
        public IActionResult GetCustomer(int id)
        {
            var value = _context.Customers.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return Ok("Müşteri Güncelleme Başarılı"); // Response body
        }
    }
}
