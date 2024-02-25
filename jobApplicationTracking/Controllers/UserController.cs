using jobApplicationTracking.Models;
using jobApplicationTracking.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace jobApplicationTracking.Controllers
{
    public class UserController : Controller
    {
        
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static UserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44316/api/");
        }

        // GET: User/List
        public ActionResult List()
        {


            string url = "UserData/ListUsers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<UserDto> Users = response.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;

            return View(Users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            DetailsUser ViewModel = new DetailsUser();


            string url = "UserData/FindUser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            UserDto SelectedUser = response.Content.ReadAsAsync<UserDto>().Result;

            ViewModel.SelectedUser = SelectedUser;

            url = "jobApplicationData/ListJobApplicationOfUser/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<jobApplicationDto> AppliedJobs = response.Content.ReadAsAsync<IEnumerable<jobApplicationDto>>().Result;

            ViewModel.AppliedJobs = AppliedJobs;

            url = "jobApplicationData/ListApplicationsNotRegByUser/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<jobApplicationDto> AvailableJobs = response.Content.ReadAsAsync<IEnumerable<jobApplicationDto>>().Result;

            ViewModel.AvailableJobs = AvailableJobs;

            return View(ViewModel);
        }

        // GET: User/New
        public ActionResult New()
        {
            return View();
        }


        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User User)
        {
            Debug.WriteLine("the json payload is :");
            Debug.WriteLine(User.UserName);

            string url = "UserData/AddUser";

            string jsonpayload = jss.Serialize(User);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(response);
            //if (response.IsSuccessStatusCode)
            //{
            return RedirectToAction("List");
            // }
            //else
            //{
            //   return RedirectToAction("Error");
            // }


        }

        public ActionResult Error()
        {

            return View();
        }
 
        //POST: User/Associate/{UserId}/{jobApplicationId}
        [HttpPost]
        public ActionResult Associate(int id, int JobApplicationID)
        {
            Debug.WriteLine("Attempting to associate animal :" + id + " with keeper " + JobApplicationID);

            string url = "UserData/AssociateUserWithJob/" + id + "/" + JobApplicationID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }


        // GET: User/UnAssociate/{id}?JobApplicationID={JobApplicationID}
        [HttpGet]
        public ActionResult UnAssociate(int id, int JobApplicationID)
        {
            Debug.WriteLine("Attempting to unassociate animal :" + id + " with keeper: " + JobApplicationID);

            string url = "UserData/UnAssociateUserWithJob/" + id + "/" + JobApplicationID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        // GET: User/Edit/2
        public ActionResult Edit(int id)
        {
            string url = "UserData/FindUser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            UserDto selectedUser = response.Content.ReadAsAsync<UserDto>().Result;
            return View(selectedUser);
        }

        // POST: User//Update/5
        [HttpPost]
        public ActionResult Update(int id, User User)
        {

            string url = "UserData/UpdateUser/" + id;
            string jsonpayload = jss.Serialize(User);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            //if (response.IsSuccessStatusCode)
           // {
                return RedirectToAction("List");
           // }
            //else
            //{
              //  return RedirectToAction("Error");
          //  }
        }

        // GET: User/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "UserData/FindUser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            UserDto selectedUser = response.Content.ReadAsAsync<UserDto>().Result;
            return View(selectedUser);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "UserData/DeleteUser/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

           // if (response.IsSuccessStatusCode)
            //{
                return RedirectToAction("List");
            //}
            //else
            //{
              //  return RedirectToAction("Error");
            //}
        }
    }
}