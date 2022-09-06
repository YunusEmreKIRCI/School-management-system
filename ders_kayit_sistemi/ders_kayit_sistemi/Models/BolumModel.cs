using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace ders_kayit_sistemi.Models
{
    public class BolumModel
    {
  
        public int Id { get; set; }
        public int UstKurulId { get; set; }
        public int BolumBaskaniId { get; set; }
        public string Ad { get;  set; }

      
    }
}
