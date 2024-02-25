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
    public class jobApplicationController : Controller
    {
        // GET: jobApplication
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static jobApplicationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44316/api/");
        }

        // GET: jobApplication/List
        public ActionResult List()
        {
            string url = "jobApplicationData/ListJobApplications";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<jobApplicationDto> jobApplications = response.Content.ReadAsAsync<IEnumerable<jobApplicationDto>>().Result;


            return View(jobApplications);
        }
        // GET: jobApplication/Details/5

        public ActionResult Details(int id)
        {
            DetailsJobApplication ViewModel = new DetailsJobApplication();


            string url = "jobApplicationData/FindJobApplication/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            jobApplicationDto SelectedApplication = response.Content.ReadAsAsync<jobApplicationDto>().Result;

            ViewModel.SelectedApplication = SelectedApplication;

            //show associated keepers with this animal
            url = "UserData/ListUsersForApplication/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<UserDto> RegisteredUsers = response.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;

           ViewModel.RegisteredUsers = RegisteredUsers;

            url = "CompanyData/ListCompanyForJobApplication/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<CompaniesDto> AssociateCompanies = response.Content.ReadAsAsync<IEnumerable<CompaniesDto>>().Result;

            ViewModel.AssociateCompanies = AssociateCompanies;


            return View(ViewModel);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: jobApplication/New
        public ActionResult New()
        {
            //information about all species in the system.
            //GET api/speciesdata/listspecies

            string url = "CompanyData/ListCompanies";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<CompaniesDto> CompaniesOptions = response.Content.ReadAsAsync<IEnumerable<CompaniesDto>>().Result;

            return View(CompaniesOptions);
        }

        // POST: jobApplication/Create
        [HttpPost]
        public ActionResult Create(jobApplication jobApplication)
        {
            Debug.WriteLine("the json payload is :");
            Debug.WriteLine(jobApplication.JobTitle);
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/animaldata/addanimal 
            string url = "jobApplicationData/AddJobApplication";


            string jsonpayload = jss.Serialize(jobApplication);
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

        // GET: Animal/Edit/5
        public ActionResult Edit(int id)
        {
            UpdatejobApplication ViewModel = new UpdatejobApplication();

            //the existing animal information
            string url = "jobApplicationData/FindJobApplication/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            jobApplicationDto SelectedApplication = response.Content.ReadAsAsync<jobApplicationDto>().Result;
            ViewModel.SelectedApplication = SelectedApplication;

            // all species to choose from when updating this animal
            //the existing animal information
            url = "CompanyData/ListCompanies";
            response = client.GetAsync(url).Result;
            IEnumerable<CompaniesDto> CompaniesOptions = response.Content.ReadAsAsync<IEnumerable<CompaniesDto>>().Result;

            ViewModel.CompaniesOptions = CompaniesOptions;

            return View(ViewModel);
        }

        // POST: Animal/Update/5
        [HttpPost]
        public ActionResult Update(int id, jobApplication jobApplication)
        {

            string url = "jobApplicationData/UpdateJobApplication/" + id;
            string jsonpayload = jss.Serialize(jobApplication);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            //if (response.IsSuccessStatusCode)
            //{
                return RedirectToAction("List");
            //}
            //else
            //{
               // return RedirectToAction("Error");
            //}
        }
        // GET: Animal/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "jobApplicationData/FindJobApplication/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            jobApplicationDto SelectedApplication = response.Content.ReadAsAsync<jobApplicationDto>().Result;
            return View(SelectedApplication);
        }

        // POST: Animal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "jobApplicationData/DeletejobApplication/" + id;
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