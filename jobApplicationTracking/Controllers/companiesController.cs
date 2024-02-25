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
    
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static companiesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44316/api/");
        }

        // GET: companies/List
        public ActionResult List()
        {


            string url = "CompanyData/ListCompanies";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<CompaniesDto> Companies = response.Content.ReadAsAsync<IEnumerable<CompaniesDto>>().Result;


            return View(Companies);
        }

        // GET: companies/Details/5
        public ActionResult Details(int id)
        {

            DetailsCompanies ViewModel = new DetailsCompanies();

            string url = "CompanyData/FindCompanies/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            CompaniesDto SelectedCompanies = response.Content.ReadAsAsync<CompaniesDto>().Result;

            ViewModel.SelectedCompanies = SelectedCompanies;

            
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

        // GET: companies/New
        public ActionResult New()
        {
            return View();
        }

        // POST: companies/Create
        [HttpPost]
        public ActionResult Create(Models.companies companies)
        {
            Debug.WriteLine("the json payload is :");
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

        // GET: companies/UnAssociate/{id}?JobApplicationID={JobApplicationID}
        [HttpGet]
        public ActionResult UnAssociate(int id, int JobApplicationID)
        {
            Debug.WriteLine("Attempting to unassociate animal :" + id + " with keeper: " + JobApplicationID);

            string url = "CompanyData/UnAssociateCompanyWithJob/" + id + "/" + JobApplicationID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }
        // GET: companies/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "CompanyData/FindCompanies/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CompaniesDto selectedCompany = response.Content.ReadAsAsync<CompaniesDto>().Result;
            return View(selectedCompany);
        }

        // POST: companies/Update/5
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

        // GET: companies/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "CompanyData/FindCompanies/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            CompaniesDto selectedCompany = response.Content.ReadAsAsync<CompaniesDto>().Result;
            return View(selectedCompany);
        }

        // POST: companies/Delete/5
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