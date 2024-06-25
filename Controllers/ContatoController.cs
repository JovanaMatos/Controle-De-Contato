using ControloDeContato.Models;
using Microsoft.AspNetCore.Mvc;

using MySqlConnector;
using System;
using System.Collections.Generic;



namespace ControloDeContato.Controllers
{
    public class ContatoController : Controller
    {


        private readonly string _connStr = "server=localhost;user=root;database=db_users;port=3306;password=";


        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar()
        {
            return View();
        }

        public IActionResult ApagarConfirmacao()
        {
            return View();
        }

        public IActionResult Index()
        {
            List<ContatoModel> listaUsers = new List<ContatoModel>();

            using (MySqlConnection conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM users;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            ContatoModel contato = new ContatoModel
                            {
                                Id = rdr.GetInt32(0),
                                Nome = rdr.GetString(1),
                                Email = rdr.GetString(2),
                                Telemovel = rdr.GetString(3)
                            };
                            listaUsers.Add(contato);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return View(listaUsers);
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            using (MySqlConnection conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO users (name, email, telemovel) VALUES (@Nome, @Email, @Telemovel)";
                    Console.WriteLine(sql);

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nome", contato.Nome);
                    cmd.Parameters.AddWithValue("@Email", contato.Email);
                    cmd.Parameters.AddWithValue("@Telemovel", contato.Telemovel);
                    Console.WriteLine(contato.Email);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Editar(int id)

        {
            ContatoModel usuario = null;

            using (MySqlConnection conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM users WHERE id = @Id;";
                    Console.WriteLine(sql);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            usuario = new ContatoModel
                            {
                                Id = rdr.GetInt32("id"),
                                Nome = rdr.GetString("name"),
                                Email = rdr.GetString("email"),
                                Telemovel = rdr.GetString("telemovel")
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(ContatoModel cadastro)
        {
            using (MySqlConnection conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE users SET name = @Nome, email = @Email, telemovel = @Telemovel WHERE id = @Id;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Nome", cadastro.Nome);
                    cmd.Parameters.AddWithValue("@Email", cadastro.Email);
                    cmd.Parameters.AddWithValue("@Telemovel", cadastro.Telemovel);
                    cmd.Parameters.AddWithValue("@Id", cadastro.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return RedirectToAction("Index");
        }




        
       [HttpGet]
        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoModel usuario = null;

            using (MySqlConnection conn = new MySqlConnection(_connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM users WHERE id = @Id;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            usuario = new ContatoModel
                            {
                                Id = (int)rdr["id"],
                                Nome = (string)rdr["name"],
                                Email = (string)rdr["email"],
                                Telemovel = (string)rdr["telemovel"]
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            if (usuario == null)
            {
                return NotFound();
            }


            return View(usuario);
        }


        [HttpPost]
        public IActionResult ApagarConfirmacao(ContatoModel cadastro)

        {

            using (MySqlConnection conn = new MySqlConnection(_connStr))
            {
                try
                {
                   
                    conn.Open();
                    string sql = "DELETE FROM users WHERE id = @Id;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", cadastro.Id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return RedirectToAction("Index");
        }
     }
}






