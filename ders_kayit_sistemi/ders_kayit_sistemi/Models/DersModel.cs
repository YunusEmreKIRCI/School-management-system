using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ders_kayit_sistemi.Models
{
    public class DersModel
    {
        public int Id { get; }
        public int BolumId { get; set; }
        public string Ad { get; set; }
        public int Donem { get; set; }
        public int Kredi { get; set; }

    }
}
