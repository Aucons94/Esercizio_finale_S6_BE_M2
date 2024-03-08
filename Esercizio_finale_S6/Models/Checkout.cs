using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esercizio_finale_S6.Models
{
    public class Checkout
    {
        public string NomeTitolare { get; set; }
        public int NumeroStanza { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public decimal TariffaApplicata { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public List<ServizioAggiuntivo> ServiziAggiuntivi { get; set; }
        public decimal ImportoDaSaldare { get; set; }
    }
}