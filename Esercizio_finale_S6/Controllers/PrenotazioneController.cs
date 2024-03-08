using Esercizio_finale_S6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;


namespace Esercizio_finale_S6.Controllers
{
    public class PrenotazioneController : Controller
    {
        public ActionResult CreaPrenotazione(string CodiceFiscale)
        {
            List<SelectListItem> camereDisponibili = Camera.GetCamereDisponibili();

            Prenotazione model = new Prenotazione();
            model.DataPrenotazione = DateTime.Today;
            model.DataInizioSoggiorno = DateTime.Today;
            model.DataFineSoggiorno = DateTime.Today;

            ViewBag.ListaCamere = camereDisponibili; 

            List<SelectListItem> listaCodiciFiscali = Cliente.GetCodiciFiscali(); 

            ViewBag.ListaCodiciFiscali = listaCodiciFiscali;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreaPrenotazione(Prenotazione model)
        {
            if (ModelState.IsValid)
            {
                model.InserisciPrenotazione();
                return RedirectToAction("ListaPrenotazione");
            }
            return View(model);
        }

        public ActionResult DettagliPrenotazione(int IdPrenotazione)
        {
            Prenotazione prenotazione = Prenotazione.GetPrenotazioneById(IdPrenotazione);

            if (prenotazione != null)
            {
                return View(prenotazione);
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult ListaPrenotazione()
        {
            var prenotazione = new Prenotazione().ListaPrenotazioni();
            return View(prenotazione);
        }

        public ActionResult EliminaPrenotazione(int IdPrenotazione)
        {
            Prenotazione prenotazione = Prenotazione.GetPrenotazioneById(IdPrenotazione);

            if (prenotazione != null)
            {
                prenotazione.EliminaPrenotazione();
            }
            return RedirectToAction("ListaPrenotazione");
        }
        public ActionResult ModificaPrenotazione(int IdPrenotazione)
        {
            Prenotazione prenotazione = Prenotazione.GetPrenotazioneById(IdPrenotazione);

            if (prenotazione != null)
            {
                ViewBag.ListaCamere = Camera.GetCamereDisponibili();
                ViewBag.ListaCodiciFiscali = Cliente.GetCodiciFiscali();

                return View(prenotazione);
            }

            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult ModificaPrenotazione(Prenotazione model)
        {
            if (ModelState.IsValid)
            {
                model.ModificaPrenotazione(); 
                return RedirectToAction("DettagliPrenotazione", new { IdPrenotazione = model.IdPrenotazione });
            }
            return View(model);
        }
    }

}