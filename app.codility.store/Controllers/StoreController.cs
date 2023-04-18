using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.codility.store.Models;
using app.codility.store.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace app.codility.store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        [HttpGet("stores")]
        public ActionResult<IEnumerable<Store>> GetStores(int? storeId, bool includeCustomer = false)
        {
            string countryCode = Request.Headers["x-test-country-code"];
            if (string.IsNullOrEmpty(countryCode))
            {
                return Unauthorized();
            }

            var storeList = _storeRepository.GetStores(s => !storeId.HasValue || s.Id == storeId.Value, includeCustomer);

            if (countryCode.Split(",").Length > 1)
            {
                return Unauthorized();
            }

            string storeCountryCode = storeList.FirstOrDefault()?.CountryCode;
            if (string.IsNullOrEmpty(storeCountryCode) || !storeCountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Forbid();
            }

            return Ok(storeList);
        }

        [HttpPost("customers")]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer customer)
        {
            string countryCode = Request.Headers["x-test-country-code"];
            if (string.IsNullOrEmpty(countryCode))
            {
                return Unauthorized();
            }

            string storeCountryCode = _storeRepository.GetStores(x=>x.Id == customer.StoreId, false).FirstOrDefault()?.CountryCode;
            if (string.IsNullOrEmpty(storeCountryCode) || !storeCountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Forbid();
            }

            if (customer == null || string.IsNullOrEmpty(customer.Name))
            {
                return BadRequest();
            }

            var createdCustomer = _storeRepository.AddCustomer(customer);
            return CreatedAtAction(nameof(CreateCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpGet("stores/{storeId}/customers")]
        public ActionResult<IEnumerable<Customer>> GetCustomers(int storeId)
        {
            string countryCode = Request.Headers["x-test-country-code"];
            if (string.IsNullOrEmpty(countryCode))
            {
                return Unauthorized();
            }

            string storeCountryCode = _storeRepository.GetStores(x=>x.Id ==storeId).FirstOrDefault()?.CountryCode;
            if (string.IsNullOrEmpty(storeCountryCode) || !storeCountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Forbid();
            }

            var customerList = _storeRepository.GetCustomers(storeId);
            return Ok(customerList);
        }
    }


}

