using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    internal class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }
        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, 
                                               Title, 
                                               Url 
                                            FROM Blog";

                   List<Blog> blogs = new List<Blog>();


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        {
                            Blog blog = new Blog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                            };

                            blogs.Add(blog);
                        }

                    reader.Close();
                    
                    
                    return blogs;
                    
                }
            }
        }

        public Blog Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT b.Id as BlogId, 
                                               b.Title, 
                                               b.Url, 
                                               t.Id AS TagId, 
                                               t.Name
                                        FROM Blog b
                                            LEFT JOIN BlogTag bt on bt.BlogId=b.Id
                                            LEFT JOIN Tag t on t.Id=bt.TagId
                                        WHERE b.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    Blog blog = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (blog == null)
                        {
                            blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Url = reader.GetString(reader.GetOrdinal("Url")),
                            };
                        }
                     

                    }
                    reader.Close();

                    return blog;
                }
            }
        }
        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, Url)
                                        VALUES (@Title, @Url)";
                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@Url", blog.Url);
                    
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Update(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog SET Title = @title, Url = @url WHERE id = @id";
                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);
                    cmd.Parameters.AddWithValue("@id", blog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Blog WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}