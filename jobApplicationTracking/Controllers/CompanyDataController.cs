using jobApplicationTracking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace jobApplicationTracking.Controllers
{
    public class CompanyDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Companies in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Companies in the database
        /// </returns>
        /// <example>
        /// GET: api/CompanyData/ListCompanies
        /// </example>
        [HttpGet]
        [Route("api/CompanyData/ListCompanies")]
        [ResponseType(typeof(CompaniesDto))]
        public IHttpActionResult ListCompanies()
        {
            List<companies> Companies = db.companies.ToList();
            List<CompaniesDto> CompaniesDtos = new List<CompaniesDto>();

            Companies.ForEach(c => CompaniesDtos.Add(new CompaniesDto()
            {
                CompanyID = c.CompanyID,
                CompanyName = c.CompanyName,
                Industry =c.Industry,
                CompanyLocation = c.CompanyLocation,
                CompanyWebsite = c.CompanyWebsite
            }));

            return Ok(CompaniesDtos);
        }
        /// <summary>
        /// Returns all companies in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An companies in the system matching up to the companies ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the companies</param>
        /// <example>
        /// GET: api/CompanyData/FindCompanies/2
        /// </example>
        [ResponseType(typeof(CompaniesDto))]
        [Route("api/CompanyData/FindCompanies/{id}")]
        [HttpGet]
        public IHttpActionResult FindCompany(int id)
        {
            companies c = db.companies.Find(id);
            CompaniesDto CompaniesDto = new CompaniesDto()
            {
                CompanyID = c.CompanyID,
                CompanyName = c.CompanyName,
                Industry = c.Industry,
                CompanyLocation = c.CompanyLocation,
                CompanyWebsite = c.CompanyWebsite
            };
            if (c == null)
            {
                return NotFound();
            }

            return Ok(CompaniesDto);
        }
        /// <summary>
        /// Updates a particular companies in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the companies ID primary key</param>
        /// <param name="companies">JSON FORM DATA of an companies</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/CompanyData/UpdateCompanies/2
        /// FORM DATA: company JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [Route("api/CompanyData/UpdateCompanies/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateCompanies(int id, companies companies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companies.CompanyID)
            {

                return BadRequest();
            }

            db.Entry(companies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompaniesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Adds an company to the system
        /// </summary>
        /// <param name="company">JSON FORM DATA of an company</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: company ID, company Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/CompanyData/AddCompany
        /// FORM DATA: company JSON Object
        /// </example>
        [ResponseType(typeof(companies))]
        [Route("api/CompanyData/AddCompany")]
        [HttpPost]
        public IHttpActionResult AddCompany(companies company)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.companies.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = company.CompanyID }, company);
        }
        /// <summary>
        /// Deletes an Company from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Company</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/CompanyData/DeleteCompany/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(companies))]
        [Route("api/CompanyData/DeleteCompany/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteCompany(int id)
        {
            companies company = db.companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.companies.Remove(company);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool CompaniesExists(int id)
        {
            return db.companies.Count(e => e.CompanyID == id) > 0;
        }
    }
}
