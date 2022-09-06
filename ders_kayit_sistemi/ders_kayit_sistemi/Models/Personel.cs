using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ders_kayit_sistemi.Models
{
    public class Personel
    {
        //public Personel(string ad, string soyad, int bolumid, string password, string email)
        //{
        //    this.Ad = ad;
        //    this.Soyad = soyad;
        //    this.Sifre = password;
        //}
        public int Id { get; }
        public int? BolumId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        

    }
}
