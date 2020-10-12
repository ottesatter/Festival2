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
                            plaats = reader["plaats"].ToString(), 
                            beschrijving = reader["beschrijving"].ToString(),
                            start_dt = DateTime.Parse(reader["start_dt"].ToString()),
                            eind_dt = DateTime.Parse(reader["eind_dt"].ToString()),
                            plaatje = reader["plaatje"].ToString(),
                            prijs = reader["prijs"].ToString(),
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
      return View();
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
        return View(GetFestivals());
    }
        
    [Route("festival/{id}")]
    public IActionResult Festival(string id)
    {
      var model = GetFestivals(id);

        return View(model);
    }

        private Festival GetFestivals(string id)
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            id = Convert.ToInt32(reader["id"]),
                            naam = reader["naam"].ToString(),
                            plaats = reader["plaats"].ToString(),
                            beschrijving = reader["beschrijving"].ToString(),
                            start_dt = DateTime.Parse(reader["start_dt"].ToString()),
                            eind_dt = DateTime.Parse(reader["eind_dt"].ToString()),
                            plaatje = reader["plaatje"].ToString(),
                            prijs = reader["prijs"].ToString(),
                        };
                        festivals.Add(p);
                    }
                }
            }

            return festivals[0];
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

