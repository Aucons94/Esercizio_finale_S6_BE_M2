using Esercizio_finale_S6.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Esercizio_finale_S6.Controllers
{
    [Authorize]
    public class QueriesController : Controller
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Albergo"].ConnectionString;
        }

        public ActionResult QueriesPage()
        {
            return View();
        }
        public static async Task<Prenotazione> GetPrenotazioneByCodiceFiscale(string codiceFiscale)
        {
            using (SqlConnection sqlConnection = new SqlConnection((new QueriesController()).GetConnectionString()))
            {
                await sqlConnection.OpenAsync();

                string query = "SELECT * FROM Prenotazioni WHERE CodiceFiscale = @CodiceFiscale";

                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            return new Prenotazione
                            {
                                IdPrenotazione = Convert.ToInt32(reader["IdPrenotazione"]),
                                CodiceFiscale = reader["CodiceFiscale"].ToString(),
                                NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                                DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                                Anno = Convert.ToInt32(reader["Anno"]),
                                DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]),
                                DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]),
                                CaparraConfirmatoria = Convert.ToDecimal(reader["CaparraConfirmatoria"]),
                                TariffaApplicata = Convert.ToDecimal(reader["TariffaApplicata"]),
                                MezzaPensione = Convert.ToBoolean(reader["MezzaPensione"]),
                                PensioneCompleta = Convert.ToBoolean(reader["PensioneCompleta"]),
                                PernottamentoPrimaColazione = Convert.ToBoolean(reader["PernottamentoPrimaColazione"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}