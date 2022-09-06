using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ders_kayit_sistemi.Models
{
    public class OgrOz
    {
        
        public int Id { get; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int BolumId { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
       
    }
}
