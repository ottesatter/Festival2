using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
    public class HomeController : Controller
    {
        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110157;Uid=110157;Pwd=crOLeran;";

        private List<festivals> GetFestivals()
        {
            List<festivals> products = new List<festivals>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        festivals f = new festival
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["naam"].ToString()
                        };
                        festivals.Add(p);
                    }
                }
            }

            return festivals;
        }
        
    public IActionResult Index()
    {
      List<festivals> festival = new List<festivals>();
      // uncomment deze regel om producten uit je database toe te voegen
      festivals = GetFestival();

      return View(festival);
    }

    [Route("informatie")]
    public IActionResult Informatie()
    {
      return View();
    }

    [Route("contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("Overview")]
    public IActionResult Overview()
    {
        return View();
    }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}

