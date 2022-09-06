using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ders_kayit_sistemi.Models
{
    public class UstKurulus
    {
        public int Id { get; }
        public int YoneticiId { get; set; }
        public string Ad { get; set; }
    }
}
