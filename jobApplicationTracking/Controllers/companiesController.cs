using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using jobApplicationTracking.Models;
using jobApplicationTracking.Models.ViewModels;
using System.Web.Script.Serialization;
using jobApplicationTracking.Migrations;

namespace jobApplicationTracking.Controllers
{
    public class companiesController : Controller
    {
        // GET: Company
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static companiesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44316/api/");
        }

        // GET: Species/List
        public ActionResult List()
        {
            //objective: communicate with our Species data api to retrieve a list of Speciess
            //curl https://localhost:44324/api/Speciesdata/listSpeciess


            string url = "CompanyData/ListCompanies";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<CompaniesDto> Companies = response.Content.ReadAsAsync<IEnumerable<CompaniesDto>>().Result;
            //Debug.WriteLine("Number of Speciess received : ");
            //Debug.WriteLine(Speciess.Count());


            return View(Companies);
        }

        // GET: Species/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with our Species data api to retrieve one Species
            //curl https://localhost:44324/api/Speciesdata/findspecies/{id}

            DetailsCompanies ViewModel = new DetailsCompanies();

            string url = "CompanyData/FindCompanies/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CompaniesDto SelectedCompanies = response.Content.ReadAsAsync<CompaniesDto>().Result;

            ViewModel.SelectedCompanies = SelectedCompanies;

            //showcase information about animals related to this species
            //send a request to gather information about animals related to a particular species ID
            url = "jobApplicationData/ListApplicationForCompany/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<jobApplicationDto> OpenJobs = response.Content.ReadAsAsync<IEnumerable<jobApplicationDto>>().Result;

            ViewModel.OpenJobs = OpenJobs;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }
        public ActionResult New()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(Models.companies companies)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Species.SpeciesName);
            //objective: add a new Species into our system using the API
            //curl -H "Content-Type:application/json" -d @Species.json https://localhost:44324/api/Speciesdata/addSpecies 
            string url = "CompanyData/AddCompany";


            string jsonpayload = jss.Serialize(companies);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            //if (response.IsSuccessStatusCode)
           // {
                return RedirectToAction("List");
            //}
            //else
            //{
              //  return RedirectToAction("Error");
            //}


        }
        //Get: Animal/UnAssociate/{id}?KeeperID={keeperID}
        [HttpGet]
        public ActionResult UnAssociate(int id, int JobApplicationID)
        {
            Debug.WriteLine("Attempting to unassociate animal :" + id + " with keeper: " + JobApplicationID);

            //call our api to associate animal with keeper
            string url = "CompanyData/UnAssociateCompanyWithJob/" + id + "/" + JobApplicationID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }
        // GET: Species/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "CompanyData/FindCompanies/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CompaniesDto selectedCompany = response.Content.ReadAsAsync<CompaniesDto>().Result;
            return View(selectedCompany);
        }

        // POST: Species/Update/5
        [HttpPost]
        public ActionResult Update(int id, Models.companies companies)
        {

            string url = "CompanyData/UpdateCompanies/" + id;
            string jsonpayload = jss.Serialize(companies);
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
              //return RedirectToAction("Error");
            //}
        }

        // GET: Species/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "CompanyData/FindCompanies/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CompaniesDto selectedCompany = response.Content.ReadAsAsync<CompaniesDto>().Result;
            return View(selectedCompany);
        }

        // POST: Species/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "CompanyData/DeleteCompany/" + id;
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