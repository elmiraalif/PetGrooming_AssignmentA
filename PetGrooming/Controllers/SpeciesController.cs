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
            List<Species> mySpecies = db.Species.SqlQuery("Select * from species").ToList();

            return View(mySpecies);
        }

        //Add method:
            //This is for the URL of the page Add: /Species/Add
            public ActionResult Add()
            {
                return View();
            }
            //HttpPost for the method Add
            [HttpPost]
            public ActionResult Add(string speciesName)
            {
                //A message which shows that we are gathering data for Species
                Debug.WriteLine("Gathering Species Name of " + speciesName);
                //To Create a query
                string query = "INSERT INTO Species VALUES (@SpeciesName)";
                //To Run the Query
                 SqlParameter sqlparam = new SqlParameter("@SpeciesName", speciesName);
                db.Database.ExecuteSqlCommand(query, sqlparam);
                //Redirect the page to the list of Species
                return RedirectToAction("List");
            }
        //end of Add method



        //Show Method
            public ActionResult Show(int id)
            {
                //write the query
                string query = "SELECT * FROM species WHERE speciesId = @id";
                //sql parameter
                SqlParameter sqlparam = new SqlParameter("@id", id);
                //Select shows a result set, we use FirstOrDefault to get only one result from the result set
                Species selectedSpecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();
                return View(selectedSpecies);
            }
        //end of Show method



        //Update method
            public ActionResult Update(int id)
            {
                string query = "SELECT * FROM species Where speciesId = @id ";
                SqlParameter sqlparam = new SqlParameter("@id", id);
                //First or Default to pick the first result of the result set
                Species selectedSpecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();
                return View(selectedSpecies);
            }

            [HttpPost]
            public ActionResult Update(int id, string speciesName)
            {

                string query = "UPDATE species SET Name = @speciesName WHERE speciesId = @id";
                //we have two properties
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@SpeciesName", speciesName);
                sqlparams[1] = new SqlParameter("@id", id);
                db.Database.ExecuteSqlCommand(query, sqlparams);
                return RedirectToAction("List");
            }
        //end of update method
            

        //Delete Method
            public ActionResult Delete(int id)
            {
                //Write the Query
                string query = "DELETE FROM species WHERE speciesId = @id";
                //sql Parameter
                SqlParameter sqlparam = new SqlParameter("@id", id);
                //Run the query
                db.Database.ExecuteSqlCommand(query, sqlparam);
                //Redirect to the list page
                return RedirectToAction("List");
            }

        //end of Delete Method

       
    }
}