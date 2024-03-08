using Esercizio_finale_S6.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Esercizio_finale_S6.Controllers
{
    public class CameraController : Controller
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Albergo"].ConnectionString;
        }
        public ActionResult CreaCamera()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreaCamera(Camera model)
        {
            if (ModelState.IsValid)
            {
                model.InserisciCamera();
                return RedirectToAction("ListaCamera");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ListaCamera()
        {
            var camera = new Camera().ListaCamera();
            return View(camera);
        }
        private Camera GetCameraById(int id)
        {
            Camera camera = new Camera();
            return camera.GetCameraById(id);
        }

        [HttpGet]
        public ActionResult ModificaCamera(int IdCamera)
        {
            Camera cameraDaModificare = GetCameraById(IdCamera);

            if (cameraDaModificare == null)
            {
                ViewBag.ErrorMessage = "La camera specificata non è stata trovata.";
                return View("Errore");
            }

            return View(cameraDaModificare);
        }

        [HttpPost]
        public ActionResult ModificaCamera(Camera cameraModificata)
        {
            if (ModelState.IsValid)
            {
                string query = "UPDATE Camere SET " +
                                "NumeroCamera = @NumeroCamera, " +
                                "Descrizione = @Descrizione, " +
                                "Tipo = @Tipo, " +
                                "Stato = @Stato " +
                                "WHERE IdCamera = @IdCamera";

                using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString()))
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@IdCamera", cameraModificata.IdCamera);
                        cmd.Parameters.AddWithValue("@NumeroCamera", cameraModificata.NumeroCamera);
                        cmd.Parameters.AddWithValue("@Descrizione", cameraModificata.Descrizione);
                        cmd.Parameters.AddWithValue("@Tipo", cameraModificata.Tipo);
                        cmd.Parameters.AddWithValue("@Stato", cameraModificata.Stato);

                        cmd.ExecuteNonQuery();
                    }
                }

                return RedirectToAction("ListaCamera");
            }

            return View(cameraModificata);
        }
    }
}