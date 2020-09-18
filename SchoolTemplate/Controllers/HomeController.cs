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
        public IActionResult Index()
        {
            List<Festival> festivals = new List<Festival>();
            // uncomment deze regel om producten uit je database toe te voegen
            festivals = GetFestivals();

            return View(festivals);
        }
        private List<Festival> GetFestivals()
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival f = new Festival
                        {
                            id = Convert.ToInt32(reader["id"]),
                            naam = reader["naam"].ToString(),
                            plaats = reader["plaats"].ToString()                            
                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }
        
   

    [Route("informatie")]
    public IActionResult Informatie()
    {
      return View(GetFestivals());
    }

    [Route("contact")]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("contact")]
    [HttpPost]
    public IActionResult Contact(PersonModel model)
    {
      // geen goed model
      if(!ModelState.IsValid)
        return View(model);

            //wel valide
      SavePerson(model);


      return Redirect("/gelukt");
    }

    private void SavePerson(PersonModel person)
    {
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("Insert INTO klant(voornaam, achternaam, emailadres, geb_datum) VALUES(?voornaam,?achternaam,?email, ?geb_datum)", conn);

        cmd.Parameters.Add("?voornaam", MySqlDbType.VarChar).Value = person.Voornaam;
        cmd.Parameters.Add("?achternaam", MySqlDbType.VarChar).Value = person.Achternaam;
        cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.Email;
        cmd.Parameters.Add("?geb_datum", MySqlDbType.VarChar).Value = person.Geboortedatum;
        cmd.ExecuteNonQuery();
      }
    }


    [Route("Overview")]
    public IActionResult Overview()
    {
        return View();
    }
        
    [Route("detail/{id}")]
    public IActionResult Detail()
    {
        return View();
    }

    [Route("gelukt")]
    public IActionResult Gelukt()
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

