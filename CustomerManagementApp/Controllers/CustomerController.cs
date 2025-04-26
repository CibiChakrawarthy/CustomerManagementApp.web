using AutoMapper;
using CustomerManagementApp.DTOs;
using CustomerManagementApp.Interfaces;
using CustomerManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _service.GetAllAsync();
            return View(customers);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Console.WriteLine($"Edit called with ID: {id}"); // for Debug

            var customer = id == Guid.Empty
                ? new Customer()
                : await _service.GetByIdAsync(id);

            if (customer == null)
            {
                Console.WriteLine("Customer not found, creating new");
                customer = new Customer();
            }

            var dto = _mapper.Map<CustomerDto>(customer);
            Console.WriteLine($"Mapping to DTO - Name: {dto.Name}"); // for Debug

            return PartialView("_CustomerForm", dto);
        }


        [HttpPost]
        public async Task<IActionResult> Save(CustomerDto dto)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 400; 
                return PartialView("_CustomerForm", dto);
            }

            try
            {
                var customer = _mapper.Map<Customer>(dto);

                if (dto.Id == Guid.Empty)
                {
                    await _service.AddAsync(customer);
                }
                else
                {
                    await _service.UpdateAsync(customer);
                }

                return Json(new { success = true });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error saving customer");
                Response.StatusCode = 400;
                return PartialView("_CustomerForm", dto);
            }
        } 

        
    }
}
