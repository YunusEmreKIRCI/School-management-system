

using System.Collections.Generic;

namespace ders_kayit_sistemi.Models
{
    public class DersListesi
    {
        public List<DersKayit> list = new List<DersKayit>();
        public int toplamKredi()
        {
            int total = 0;
            foreach (var item in list)
                if (item.BasariDurumu == false && item.KayitDurumu == true)
                    total += item.Kredi;
            return total;
        }
    }
}
