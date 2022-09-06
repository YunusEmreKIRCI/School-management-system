using ders_kayit_sistemi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace ders_kayit_sistemi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {   
            return View();
        }
        public IActionResult AdminKontrol()
        {
            var query = "SELECT * FROM Admin";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            connection.Close();
            if (table.Rows.Count != 0)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Admin", "Register");
            }
        }

        public IActionResult Personel()
        {
            return View();
        }
        public IActionResult Ogr()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AuthenticationPersonel(Personel personel)
        {
            var query = "SELECT * FROM Personel WHERE [e-mail]=@Email AND sifre= @Sifre";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", personel.Email);
            command.Parameters.AddWithValue("@sifre", personel.Sifre);
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
            }
            catch
            {
                return View("Personel", personel);
            }
            connection.Close();
            if (table.Rows.Count != 0)
            {
                HttpContext.Session.SetString("id", table.Rows[0]["id"].ToString());
                HttpContext.Session.SetString("bolumId", table.Rows[0]["bolumId"].ToString());
                HttpContext.Session.SetString("ad", table.Rows[0]["ad"].ToString());
                HttpContext.Session.SetString("soyad", table.Rows[0]["soyad"].ToString());
                HttpContext.Session.SetString("e-mail", table.Rows[0]["e-mail"].ToString());
                return RedirectToAction("Index", "Personel");
            }
            else
            {
                return View("Personel", personel);
            }
        }
        [HttpPost]
        public IActionResult AuthenticationOgr(OgrOz ogroz)
        {
            var query = "SELECT * FROM ogrenci WHERE [e-mail]=@Email AND sifre= @Sifre";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", ogroz.Email);
            command.Parameters.AddWithValue("@sifre", ogroz.Sifre);
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
            }
            catch
            {
                return View("Ogr", ogroz);
            }
            connection.Close();
            if (table.Rows.Count != 0)
            {
                HttpContext.Session.SetString("id", table.Rows[0]["id"].ToString());
                HttpContext.Session.SetString("ad", table.Rows[0]["ad"].ToString());
                HttpContext.Session.SetString("soyad", table.Rows[0]["soyad"].ToString());
                HttpContext.Session.SetString("bolumId", table.Rows[0]["bolumId"].ToString());
                HttpContext.Session.SetString("e-mail", table.Rows[0]["e-mail"].ToString());
                return RedirectToAction("Index", "Ogrenci");
            }
            else
            {
                return View("Ogr", ogroz);
            }
        }
        [HttpPost]
        public IActionResult AuthenticationAdmin(Admin admin)
        {
            var query = "SELECT * FROM admin WHERE id=@id AND sifre= @Sifre";
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
            connection.Open();
            System.Data.DataTable table = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", admin.Id);
            command.Parameters.AddWithValue("@sifre", admin.Sifre);
            adapter.SelectCommand = command;
            try
            {
                adapter.Fill(table);
            }
            catch
            {
                return View("Admin", admin);
            }
            connection.Close();
            if (table.Rows.Count != 0)
            {
                HttpContext.Session.SetString("id", table.Rows[0]["id"].ToString());
                HttpContext.Session.SetString("sifre", table.Rows[0]["sifre"].ToString());
                return RedirectToAction("Index", "admin");
            }
            else
            {
                return View("Admin", admin);
            }
        }
    }
}

