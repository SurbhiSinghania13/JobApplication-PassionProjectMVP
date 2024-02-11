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
    public class UserDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Users in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Users in the database
        /// </returns>
        /// <example>
        /// GET: api/UserData/ListUsers
        /// </example>
        [HttpGet]
        [Route("api/UserData/ListUsers")]
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult ListUsers()
        {
            List<User> users = db.Users1.ToList();
            List<UserDto> UserDtos = new List<UserDto>();

            users.ForEach(u => UserDtos.Add(new UserDto()
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserEmail = u.UserEmail,
                UserPortfolioUrl = u.UserPortfolioUrl

             }));

            return Ok(UserDtos);
        }

        /// <summary>
        /// Returns all Users in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An User in the system matching up to the User ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the User</param>
        /// <example>
        /// GET: api/UserData/FindUser/5
        /// </example>
        [ResponseType(typeof(UserDto))]
        [Route("api/UserData/FindUser/{id}")]
        [HttpGet]
        public IHttpActionResult FindUser(int id)
        {
            User u = db.Users1.Find(id);
            UserDto UserDto = new UserDto()
            {
                UserId = u.UserId,
                UserName = u.UserName,
                UserEmail = u.UserEmail,
                UserPortfolioUrl = u.UserPortfolioUrl
            };
            if (u == null)
            {
                return NotFound();
            }

            return Ok(UserDto);
        }

        /// <summary>
        /// Updates a particular User in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the User ID primary key</param>
        /// <param name="Keeper">JSON FORM DATA of an u</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/UserData/UpdateUser/1
        /// FORM DATA: u JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [Route("api/UserData/UpdateUser/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateUser(int id, User u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != u.UserId)
            {

                return BadRequest();
            }

            db.Entry(u).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        /// Adds an User to the system
        /// </summary>
        /// <param name="u">JSON FORM DATA of an User</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: User ID, User Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/UserData/AddUser
        /// FORM DATA: u JSON Object
        /// </example>
        [ResponseType(typeof(User))]
        [HttpPost]
        public IHttpActionResult AddUser(User u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users1.Add(u);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = u.UserId }, u);
        }

        /// <summary>
        /// Deletes an User from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the User</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/UserData/DeleteUser/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(User))]
        [Route("api/UserData/DeleteUser/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteUser(int id)
        {
            User u = db.Users1.Find(id);
            if (u == null)
            {
                return NotFound();
            }

            db.Users1.Remove(u);
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

        private bool UserExists(int id)
        {
            return db.Users1.Count(e => e.UserId == id) > 0;
        }

    }
}
