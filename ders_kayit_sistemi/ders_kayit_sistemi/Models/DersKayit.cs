using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ders_kayit_sistemi.Models
{
    public class DersKayit
    {
        public int Id { get; set; }
        public int DersId { get; set; }
        public int OgrenciId { get; set; }
        public bool BasariDurumu { get; set; }
        public bool KayitDurumu { get; set; }
        public int Kredi { get; set; }
        public string Ad { get; set; }
        public int Donem { get; set; }
        public int AkademisyenId { get; set; }
        public int Total { get; set; }

    }
}

