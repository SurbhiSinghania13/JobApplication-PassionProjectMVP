using jobApplicationTracking.Migrations;
using jobApplicationTracking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;

namespace jobApplicationTracking.Controllers
{
    public class jobApplicationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        /// <summary>
        /// Returns all jobApplication in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all jobApplication in the database, including their associated comapnies.
        /// </returns>
        /// <example>
        /// GET: api/jobApplicationData/ListJobApplications
        /// </example>
        [HttpGet]
        [Route("api/jobApplicationData/ListJobApplications")]
        [ResponseType(typeof(jobApplicationDto))]
        public IHttpActionResult ListJobApplications()
        {
            List<jobApplication> jobApplications = db.jobApplications.ToList();
            List<jobApplicationDto> jobApplicationDtos = new List<jobApplicationDto>();

            jobApplications.ForEach(j => jobApplicationDtos.Add(new jobApplicationDto()
            {
                JobApplicationID = j.JobApplicationID,
                JobTitle = j.JobTitle,
                CompanyName = j.CompanyName,
                JobLocation = j.JobLocation,
                CompanyID = j.companies.CompanyID,
                Industry = j.companies.Industry
            }));

            return Ok(jobApplicationDtos);
             
        }

        /// <summary>
        /// Gathers information about all jobApplication related to a particular company ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all jobApplication in the database, including their associated company matched with a particular company ID
        /// </returns>
        /// <param name="id">Company ID.</param>
        /// <example>
        /// GET: api/jobApplicationData/ListApplicationForCompany/3
        /// </example>
        [HttpGet]
        [Route("api/jobApplicationData/ListApplicationForCompany/{id}")]
        [ResponseType(typeof(jobApplicationDto))]
        public IHttpActionResult ListApplicationForCompany(int id)
        {
            List<jobApplication> jobApplications = db.jobApplications.Where(a => a.CompanyID == id).ToList();
            List<jobApplicationDto> jobApplicationDtos = new List<jobApplicationDto>();

            jobApplications.ForEach(j => jobApplicationDtos.Add(new jobApplicationDto()
            {
                JobApplicationID = j.JobApplicationID,
                JobTitle = j.JobTitle,
                CompanyName = j.CompanyName,
                JobLocation = j.JobLocation,
                CompanyID = j.companies.CompanyID,
                Industry = j.companies.Industry
            }));

            return Ok(jobApplicationDtos);
        }

        /// <summary>
        /// Gathers information about jobApplications related to a particular user
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all jobApplication in the database, including their associated company that match to a particular user id
        /// </returns>
        /// <param name="id">user ID.</param>
        /// <example>
        /// GET: api/jobApplicationData/ListJobApplicationOfUser/1
        /// </example>
        [HttpGet]
        [Route("api/jobApplicationData/ListJobApplicationOfUser/{id}")]
        [ResponseType(typeof(jobApplicationDto))]
        public IHttpActionResult ListJobApplicationOfUser(int id)
        {
            List<jobApplication> jobApplications = db.jobApplications.Where(
                j => j.users.Any(
                    u => u.UserId == id
                )).ToList();
            List<jobApplicationDto> jobApplicationDtos = new List<jobApplicationDto>();

            jobApplications.ForEach(j => jobApplicationDtos.Add(new jobApplicationDto()
            {
                JobApplicationID = j.JobApplicationID,
                JobTitle = j.JobTitle,
                CompanyName = j.CompanyName,
                JobLocation = j.JobLocation,
                CompanyID = j.companies.CompanyID,
                Industry = j.companies.Industry
            }));

            return Ok(jobApplicationDtos);
        }
        /// <summary>
        /// Returns all jobApplications in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An jobApplication in the system matching up to the jobApplication ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the jobApplication</param>
        /// <example>
        /// GET: api/jobApplicationData/FindJobApplication/5
        /// </example>
        [ResponseType(typeof(jobApplicationDto))]
        [Route("api/jobApplicationData/FindJobApplication/{id}")]
        [HttpGet]
        public IHttpActionResult FindJobApplication(int id)
        {
            jobApplication jobApplication = db.jobApplications.Find(id);
            jobApplicationDto jobApplicationDto = new jobApplicationDto()
            {
                JobApplicationID = jobApplication.JobApplicationID,
                JobTitle = jobApplication.JobTitle,
                CompanyName = jobApplication.CompanyName,
                JobLocation = jobApplication.JobLocation,
                CompanyID = jobApplication.companies.CompanyID,
                Industry = jobApplication.companies.Industry
            };
            if (jobApplication == null)
            {
                return NotFound();
            }

            return Ok(jobApplicationDto);
        }
        /// <summary>
        /// Updates a particular jobApplication in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the jobApplication ID primary key</param>
        /// <param name="jobApplication2">JSON FORM DATA of an jobApplication</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/jobApplicationData/UpdateJobApplication/5
        /// FORM DATA: jobApplication2 JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [Route("api/jobApplicationData/UpdateJobApplication/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateJobApplication(int id, jobApplication jobApplication2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobApplication2.JobApplicationID)
            {

                return BadRequest();
            }

            db.Entry(jobApplication2).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            if (!jobApplicationExists(id))
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
        /// Adds an jobApplication to the system
        /// </summary>
        /// <param name="jobApplication2">JSON FORM DATA of an jobApplication</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: jobApplication ID, jobApplication Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/jobApplicationData/AddjobApplication
        /// FORM DATA: jobApplication2 JSON Object
        /// </example>
        [ResponseType(typeof(jobApplication))]
        [Route("api/jobApplicationData/AddJobApplication")]
        [HttpPost]
        public IHttpActionResult AddJobApplication(jobApplication jobApplication2)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.jobApplications.Add(jobApplication2);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobApplication2.JobApplicationID }, jobApplication2);
        }
        /// <summary>
        /// Deletes an jobApplication from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the jobApplication</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/jobApplicationData/DeletejobApplication/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(jobApplication))]
        [Route("api/jobApplicationData/DeletejobApplication/{id}")]
        [HttpPost]
        public IHttpActionResult DeletejobApplication(int id)
        {
            jobApplication jobApplication2 = db.jobApplications.Find(id);
            if (jobApplication2 == null)
            {
                return NotFound();
            }

            db.jobApplications.Remove(jobApplication2);
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

        private bool jobApplicationExists(int id)
        {
            return db.jobApplications.Count(e => e.JobApplicationID == id) > 0;
        }


    }
}
