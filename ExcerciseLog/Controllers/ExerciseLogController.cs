using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.IO;
using ExcerciseLog.Models;
namespace ExcerciseLog.Controllers

{
    public class ExerciseLogController : Controller
    {
        //
        // GET: /ExerciseLog/

        Database db = new Database();


        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult Log()
        {
            List<NewExercise> files = new List<NewExercise>();
            db.con.Open();

            // Get the current date and time
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd");

            string q = "SELECT l.date, l.rep, l.sett, ne.Excercise_name, ne.Exercise_img,ne.img_type, l.Log_id FROM [Log] l INNER JOIN  NewExercise ne on ne.Exercise_id = l.Exercise_id";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                files.Add(new NewExercise
                {
                    Logid = int.Parse(sdr["Log_id"].ToString()),
                    ExerciseName = sdr["Excercise_name"].ToString(),
                    date = sdr["date"].ToString(),
                    set = int.Parse(sdr["sett"].ToString()),
                    rep = int.Parse(sdr["rep"].ToString()),
                    img_type = sdr["img_type"].ToString(),
                    Exercise_img = (byte[])sdr["Exercise_img"]

                });
            }

            // Close the database connection and data reader
            db.con.Close();
            sdr.Close();

            // Pass the current date and time to the view bag or model for display in the view
            ViewBag.CurrentDateTime = formattedDateTime;

            return View(files);
        }




        // Remove Log

        public ActionResult Remove(int id)
        {
            db.con.Open();
            SqlCommand cmd = new SqlCommand(" delete from Log where Log_id='"+id+"'", db.con);
            cmd.Parameters.AddWithValue("@Log_id", id);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Log");
        }


        //update log

        [HttpGet]
        public ActionResult updatelog(int id)
        {
            db.con.Open();
            string q = "SELECT l.date,l.rep,l.sett,l.Log_id,ne.Excercise_name FROM [Log] l INNER JOIN  NewExercise ne on ne.Exercise_id=l.Exercise_id where l.Log_id='"+id+"'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            NewExercise ne = new NewExercise();
            ne.ExerciseName = sdr["Excercise_name"].ToString();
                    ne.Logid = int.Parse(sdr["Log_id"].ToString());
                    ne.date = sdr["date"].ToString();
                    ne.set = int.Parse(sdr["sett"].ToString());
                    ne.rep = int.Parse(sdr["rep"].ToString());           
            sdr.Close();
            db.con.Close();
            return View(ne);
        }


        [HttpPost]
        public ActionResult updatelog(NewExercise ne)
        {
            db.con.Open();
            string q = "Update [Log] set date=(@date),sett =(@sett),rep=(@rep) where Log_id='" + ne.Logid + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@date", ne.date);
            cmd.Parameters.AddWithValue("@sett", ne.set);
            cmd.Parameters.AddWithValue("@rep", ne.rep);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Log");
        }
      


        //New Exercise
        [HttpGet]
        public ActionResult NewExercises()
        {

            return View();
        }
        [HttpPost]
        public ActionResult NewExercises(NewExercise p, HttpPostedFileBase PostedFile)
        {

            if (ModelState.IsValid)
            {
                if (PostedFile != null && PostedFile.ContentLength > 0)
                {
                    byte[] imageData;

                    using (var ms = new MemoryStream())
                    {
                        PostedFile.InputStream.CopyTo(ms);
                        imageData = ms.ToArray();
                    }

                    db.con.Open();

                    var sqlQuery = "INSERT INTO NewExercise (Excercise_name,img_type, Exercise_img) VALUES (@Excercise_name,@FileName, @ImageData)";

                    using (var command = new SqlCommand(sqlQuery, db.con))
                    {
                        command.Parameters.AddWithValue("@Excercise_name", p.ExerciseName);
                        command.Parameters.AddWithValue("@FileName", PostedFile.FileName);
                        command.Parameters.AddWithValue("@ImageData", imageData);

                        command.ExecuteNonQuery();
                    }
                    
                }

            }
            return View("NewExercises");
        }



      

        public ActionResult Exercises()
        {

            List<NewExercise> files = new List<NewExercise>();
            db.con.Open();
            string q = "select Excercise_name,Exercise_id,Exercise_img,img_type from NewExercise";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                files.Add(new NewExercise
                {
                    Exerciseid = int.Parse(sdr["Exercise_id"].ToString()),

                    ExerciseName = sdr["Excercise_name"].ToString(),

                    img_type = sdr["img_type"].ToString(),
                    Exercise_img = (byte[])sdr["Exercise_img"]

                    
                });
            }
            db.con.Close();
            sdr.Close();
            return View(files);

        }

        // Remove Exercise

        public ActionResult RemoveExercise(int id)
        {
            db.con.Open();
            SqlCommand cmd = new SqlCommand(" delete from NewExercise where Exercise_id='" + id + "'", db.con);
            cmd.Parameters.AddWithValue("@Exercise_id", id);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Exercises");
        }

        //Edit Exercise
        [HttpGet]
        public ActionResult EditExercise(int id)
        {
            db.con.Open();
            string q = "SELECT * from NewExercise  where Exercise_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            NewExercise ne = new NewExercise();
            ne.ExerciseName = sdr["Excercise_name"].ToString();
            sdr.Close();
            db.con.Close();
            return View(ne);
        }


        [HttpPost]
        public ActionResult EditExercise(NewExercise ne,int id)
        {
            db.con.Open();
            string q = "Update [NewExercise] set Excercise_name=(@Excercise_name) where Exercise_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@Excercise_name", ne.ExerciseName);
            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Exercises");
        }

        
        
        
        
        [HttpGet]
        public ActionResult Filllog(int id)
        {
            db.con.Open();
            string q = "select * from NewExercise where Exercise_id='" + id + "'";
            SqlCommand cmd = new SqlCommand(q, db.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            NewExercise ne = new NewExercise();
            ne.ExerciseName = sdr["Excercise_name"].ToString();
            sdr.Close();
            db.con.Close();
            return View(ne);
        }

        [HttpPost]
        public ActionResult Filllog(NewExercise ne,int id)
        {
            db.con.Open();
            string q = "insert into Log (Exercise_id,date,sett,rep) values(@Exercise_id,@date,@sett,@rep)";
            SqlCommand cmd = new SqlCommand(q, db.con);
            cmd.Parameters.AddWithValue("@Exercise_id", id);
            cmd.Parameters.AddWithValue("@date", ne.date);
            cmd.Parameters.AddWithValue("@sett", ne.@set);
            cmd.Parameters.AddWithValue("@rep", ne.rep);

            cmd.ExecuteNonQuery();
            db.con.Close();
            return RedirectToAction("Log");
        }

        



    }
}
