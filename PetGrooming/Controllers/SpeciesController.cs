using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {
        private PetGroomingContext db = new PetGroomingContext();
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List()
        {
            //what data do we need?
            List<Species> myspecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(myspecies);
        }

        //Add method:
            //This is for the URL of the page Add: /Species/Add
            public ActionResult Add()
            {
                return View();
            }
            //HttpPost for the method Add
            [HttpPost]
            public ActionResult Add(string SpeciesName)
            {
                //A message which shows that we are gathering data for Species
                Debug.WriteLine("Gathering Species Name of " + SpeciesName);
                //To Create a query
                string query = "INSERT INTO Species VALUES (@SpeciesName)";
                //To Run the Query
                SqlParameter sqlparam = new SqlParameter("@SpeciesName", SpeciesName);
                db.Database.ExecuteSqlCommand(query, sqlparam);
                //Redirect the page to the list of Species
                return RedirectToAction("List");
            }
        //end of Add method

        //Show Method
        public ActionResult Show(int id)
        {
            //write the query
            string query = "SELECT * FROM species WHERE speciesid = @id";
            //sql parameter
            SqlParameter sqlparam = new SqlParameter("@id", id);
            Species selectedSpecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();
            return View(selectedSpecies);
        }
        //end of Show method



        // Update
        // [HttpPost] Update
        // (optional) delete
        // [HttpPost] Delete
    }
}