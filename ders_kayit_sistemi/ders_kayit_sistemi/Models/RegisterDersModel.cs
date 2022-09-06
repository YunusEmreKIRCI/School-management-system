using System.Collections.Generic;

namespace ders_kayit_sistemi.Models
{
    public class RegisterDersModel
    {
        public List<BolumModel> Bolumler { get; set; }
        public DersModel Ders{ get; set; }
    }
}
