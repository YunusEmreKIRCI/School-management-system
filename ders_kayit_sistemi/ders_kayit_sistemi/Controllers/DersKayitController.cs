using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using ders_kayit_sistemi.Models;

namespace ders_kayit_sistemi.Controllers
{
    public class DersKayitController : Controller
    {

        private readonly IConfiguration configuration;

        public DersKayitController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("id") == null)
                return RedirectToAction("Index", "Login");
            List<Models.DersKayit> list = new List<Models.DersKayit>();
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comm = new SqlCommand(@"SELECT
                                                d.id,
                                            	d.ad, 
                                            	d.kredi,
                                                dk.dersKayitId,
                                                dk.basariDurumu,
                                                d.donem,
                                            	CASE 
                                            		WHEN dk.dersKayitId is NULL then 0 
                                            		else 1 
                                            	end kayitoldu
                                            FROM dersler d 
                                            INNER JOIN bolum b ON b.id = d.bolumId
                                            INNER JOIN ogrenci o ON o.bolumId = b.id
                                            LEFT JOIN dersKayit dk ON dk.ogrenciId = o.id AND dk.dersId = d.id
                                            WHERE 
                                            	o.id= @id 
                                            ORDER BY d.donem", connection);
            comm.Parameters.AddWithValue("@id", HttpContext.Session.GetString("id"));
            adapter.SelectCommand = comm;
            connection.Open();
            adapter.Fill(dt);
            connection.Close();
            bool a;

            foreach (System.Data.DataRow row in dt.Rows)
            {
                a = Convert.ToBoolean(row["kayitoldu"]);
                if (a)
                {

                    list.Add(new Models.DersKayit
                    {
                        DersId = Convert.ToInt32(row["id"]),
                        Id = Convert.ToInt32(row["dersKayitId"]),
                        //DersId = Convert.ToInt32(row["bolumId"]),
                        //OgrenciId = Convert.ToInt32(row["ogrenciId"]),
                        BasariDurumu = Convert.ToBoolean(row["basariDurumu"]),
                        KayitDurumu = Convert.ToBoolean(row["kayitoldu"]),
                        Ad = row["ad"].ToString(),
                        Donem = Convert.ToInt32(row["donem"]),
                        Kredi = Convert.ToInt32(row["kredi"]),
                        //AkademisyenId = Convert.ToInt32(row["akademisyenId"]),
                    });
                }
                else
                {
                    list.Add(new Models.DersKayit
                    {
                        DersId = Convert.ToInt32(row["id"]),
                        //Id = Convert.ToInt32(row["id"]),
                        //DersId = Convert.ToInt32(row["bolumId"]),
                        //OgrenciId = Convert.ToInt32(row["ogrenciId"]),
                        //BasariDurumu = Convert.ToBoolean(row["basariDurumu"]),
                        KayitDurumu = Convert.ToBoolean(row["kayitoldu"]),
                        Ad = row["ad"].ToString(),
                        Donem = Convert.ToInt32(row["donem"]),
                        Kredi = Convert.ToInt32(row["kredi"]),
                        //AkademisyenId = Convert.ToInt32(row["akademisyenId"]),
                    });
                }


            }

            DersListesi dersListesi = new DersListesi();
            dersListesi.list = list;

            return View(dersListesi);

        }
        [HttpPost]
        public ActionResult DersKayit(DersKayit kayit)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("id"));
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            System.Data.DataTable dt = new System.Data.DataTable();
            if (kayit.KayitDurumu)
            {
                SqlCommand command = new SqlCommand("DELETE FROM dersKayit WHERE dersKayitId= @id", connection);
                command.Parameters.AddWithValue("@id", kayit.Id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                if (kayit.Total + kayit.Kredi > 40)
                {
                    // hata mesajı gönder
                    return RedirectToAction("Index");
                }
                SqlCommand command = new SqlCommand("INSERT INTO dersKayit(dersId,ogrenciId,basariDurumu) VALUES(@dersId,@ogrenciId,0)", connection);
                command.Parameters.AddWithValue("@ogrenciId", id);
                command.Parameters.AddWithValue("@dersId", kayit.DersId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }



            return RedirectToAction("Index");
        }
    }
}
