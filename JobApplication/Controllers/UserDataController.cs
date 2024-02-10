using JobApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace JobApplication.Controllers
{
    public class UserDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all animals in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all animals in the database, including their associated species.
        /// </returns>
        /// <example>
        /// GET: api/AnimalData/ListAnimals
        /// </example>
        [HttpGet]
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult ListUsers()
        {
            List<User> Users = db.Users.ToList();
            List<UserDto> UserDtos = new List<UserDto>();

            Users.ForEach(a => UserDtos.Add(new UserDto()
            {
                UserId = a.UserId,
                UserName = a.UserName,
                UserEmail = a.UserEmail,
                UserPortfolioUrl = a.UserPortfolioUrl,
                AppliedJobs = a.AppliedJobs,
                SpeciesID = a.Species.SpeciesID,
                SpeciesName = a.Species.SpeciesName
            }));

            return Ok(UserDto);
        }
    }
}
