using ders_kayit_sistemi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace ders_kayit_sistemi.Controllers
{
    public class Register : Controller
    {
        private readonly string connString;
        private readonly IConfiguration configuration;

        public Register(IConfiguration configuration)
        {
            this.connString = configuration.GetConnectionString("DefaultConnectionString");
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Personel()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comm = new SqlCommand(@"SELECT
                                                b.id,
                                                b.ad
                                            FROM bolum b
                                            ORDER BY b.ad", connection);
            adapter.SelectCommand = comm;
            connection.Open();
            adapter.Fill(dt);
            connection.Close();

            var bolumList = new List<BolumModel>();

            foreach (DataRow row in dt.Rows)
            {
                bolumList.Add(new BolumModel()
                {
                    Ad = row["ad"].ToString(),
                    Id = int.Parse(row["id"].ToString())
                });
            }
            return View(new RegisterPersonelModel()
            {
                Bolumler = bolumList
            });
        }

        public IActionResult Ogr()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comm = new SqlCommand(@"SELECT
                                                b.id,
                                                b.ad
                                            FROM bolum b
                                            ORDER BY b.ad", connection);
            adapter.SelectCommand = comm;
            connection.Open();
            adapter.Fill(dt);
            connection.Close();

            var bolumList = new List<BolumModel>();

            foreach (DataRow row in dt.Rows)
            {
                bolumList.Add(new BolumModel()
                {
                    Ad = row["ad"].ToString(),
                    Id = int.Parse(row["id"].ToString())
                });
            }
            return View(new RegisterOgrModel()
            {
                Bolumler = bolumList
            });
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Ders()
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comm = new SqlCommand(@"SELECT
                                                b.id,
                                                b.ad
                                            FROM bolum b
                                            ORDER BY b.ad", connection);
            adapter.SelectCommand = comm;
            connection.Open();
            adapter.Fill(dt);
            connection.Close();

            var bolumList = new List<BolumModel>();

            foreach (DataRow row in dt.Rows)
            {
                bolumList.Add(new BolumModel()
                {
                    Ad = row["ad"].ToString(),
                    Id = int.Parse(row["id"].ToString())
                });
            }
            return View(new RegisterDersModel()
            {
                Bolumler = bolumList
            });
        }

        public IActionResult UstKurulus()
        {
            return View();
        }

        public IActionResult Bolum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewPersonel(RegisterPersonelModel personelModel)
        {
            if (personelModel.persoel.Ad == "" || personelModel.persoel.Soyad == "" || personelModel.persoel.Sifre == "")
            {
                //personel.ErrorMessage = "Tüm alanları doldurun";
                //return View("Edit", personel);
            }
            if (personelModel.persoel.Ad == null || personelModel.persoel.Soyad == null || personelModel.persoel.Sifre == null)
            {
                //personel.ErrorMessage = "Tüm alanları doldurun";
                //return View("Edit", personel);
            }
            personelModel.persoel.Email = "P" + personelModel.persoel.Ad + "." + personelModel.persoel.Soyad + "@xyz.edu";
            var query = "INSERT INTO Personel(ad,soyad,[e-mail],sifre,bolumId) VALUES(@ad,@soyad,@email,@sifre,@bolumId)";
            SqlConnection connection = new SqlConnection(connString);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ad", personelModel.persoel.Ad);
            command.Parameters.AddWithValue("@soyad", personelModel.persoel.Soyad);
            command.Parameters.AddWithValue("@email", personelModel.persoel.Email);
            command.Parameters.AddWithValue("@sifre", personelModel.persoel.Sifre);
            command.Parameters.AddWithValue("@bolumId", personelModel.persoel.BolumId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Login");


        }
        [HttpPost]
        public IActionResult NewOgr(RegisterOgrModel ogrModel)
        {
            //if (personel.Ad == "" || personel.Soyad == "" || personel.Sifre == "")
            //{
            //    //personel.ErrorMessage = "Tüm alanları doldurun";
            //    //return View("Edit", personel);
            //}
            //if (personel.Ad == null || personel.Soyad == null || personel.Sifre == null)
            //{
            //    //personel.ErrorMessage = "Tüm alanları doldurun";
            //    //return View("Edit", personel);
            //}
            ogrModel.Ogreci.Email = "O" + ogrModel.Ogreci.Ad + "." + ogrModel.Ogreci.Soyad + "@xyz.edu";
            var query = "INSERT INTO ogrenci(ad,soyad,[e-mail],sifre, bolumId) VALUES(@ad, @soyad, @email, @sifre, @bolumId)";
            SqlConnection connection = new SqlConnection(connString);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ad", ogrModel.Ogreci.Ad);
            command.Parameters.AddWithValue("@soyad", ogrModel.Ogreci.Soyad);
            command.Parameters.AddWithValue("@email", ogrModel.Ogreci.Email);
            command.Parameters.AddWithValue("@sifre", ogrModel.Ogreci.Sifre);
            command.Parameters.AddWithValue("@bolumId", ogrModel.Ogreci.BolumId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Login");//personel anasayfasına dönecek
        }
        public IActionResult NewAdmin(Admin admin)
        {
            admin.Id = "A" + admin.Id;
            var query = "INSERT INTO admin(id,sifre) VALUES(@id, @sifre)";
            SqlConnection connection = new SqlConnection(connString);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", admin.Id);
            command.Parameters.AddWithValue("@sifre", admin.Sifre);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Login");
        }
        public IActionResult NewDers(RegisterDersModel dersModel)
        {
            var query = "INSERT INTO dersler(bolumId,ad,donem,kredi) VALUES(@bolumid,@ad,@donem,@kredi)";
            SqlConnection connection = new SqlConnection(connString);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bolumid", dersModel.Ders.BolumId);
            command.Parameters.AddWithValue("@ad", dersModel.Ders.Ad);
            command.Parameters.AddWithValue("@donem", dersModel.Ders.Donem);
            command.Parameters.AddWithValue("@kredi", dersModel.Ders.Kredi);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Personel");
        }

        [HttpPost]
        public IActionResult NewUstKurulus(UstKurulus ustKurulus)
        {
            var query = "INSERT INTO ustKurulus(ad) VALUES(@ad)";
            SqlConnection connection = new SqlConnection(connString);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ad", ustKurulus.Ad);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult NewBolum(BolumModel bolumModel)
        {
            var query = "INSERT INTO bolum(ad) VALUES(@ad)";
            SqlConnection connection = new SqlConnection(connString);
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ad", bolumModel.Ad);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Bolum", "Register");
        }

    }
}

