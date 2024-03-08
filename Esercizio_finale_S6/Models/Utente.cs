using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Esercizio_finale_S6.Models
{
    public class Utente
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}