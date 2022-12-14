using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pensioner_Details.Provider;
using Pensioner_Details.Repository;

namespace Pensioner_Details.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerDetailController));
        private IDetailsProvider detail;

        public PensionerDetailController(IDetailsProvider _Idetail)
        {
            detail = _Idetail;
        }
        ///Getting the details of the pensioner details from csv file by giving Aadhar Number
        ///Summary
        /// <returns> pensioner Values</returns>
         
        // GET: api/PensionerDetail/5
        [HttpGet("{aadhar}")]
        public IActionResult PensionerDetailByAadhar(string aadhar)
        {
            detail = new DetailsProvider();
            PensionerDetail pensioner = detail.GetDetailsByAadhar(aadhar);
            return Ok(pensioner);
        }
        [HttpPost]
        public IActionResult addPensionerDetails(PensionerDetail pension)
        {
            try
            {
                detail= new DetailsProvider();
                PensionerDetail pensioner = detail.AddNewPensioner(pension);
                return Ok("Succesfully Inserted");
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPut]
        public IActionResult updatePensionerDetails(PensionerDetail pension, string aadhar)
        {
            try
            {
                detail = new DetailsProvider();
                PensionerDetail pensioner = detail.UpdatePensioner(pension,aadhar);
                return Ok(pension);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{aadhar}")]
        public IActionResult deletePensioner(string aadhar)
        {
            try
            {
                detail = new DetailsProvider();
                PensionerDetail pensioner = detail.DeletPensioner(aadhar);
                return Ok("Succeffully Removed");
            }
            catch
            {
                return Ok("Not Found");
            }
        }


    }
}

