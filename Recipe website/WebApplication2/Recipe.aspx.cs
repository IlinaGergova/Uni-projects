using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApplication2
{
    public partial class Recipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
                connection.Open();
                string select = "SELECT * FROM [RecipesDB] WHERE id =" + Request.QueryString["recipe"].ToString();
                SqlCommand cmd = new SqlCommand(select, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        TitleHolder.Controls.Add(new Literal { Text = "<h1 id=title>" });
                        TitleHolder.Controls.Add(new Literal { Text = reader["name"].ToString() });
                        TitleHolder.Controls.Add(new Literal { Text = "</h1>" });
                        
                        
                        if(reader["image"].ToString() == "NULL")
                        {
                            foodImage.ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/No_image_3x4.svg/1280px-No_image_3x4.svg.png";
                        }
                        else
                        {
                            foodImage.ImageUrl = reader["image"].ToString();
                        }
                        RecipeHolder.Controls.Add(new Literal { Text = "<div>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "<h2> Ingredients: </h2>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "<div id=ingredients>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "<ul>" });
                        string text = reader["ingredients"].ToString().Replace("\r\n", "#");
                        string[] ingredients = text.Split('\u0023'); 
                        foreach(string i in ingredients)
                        {
                        if (!i.Equals(""))
                        {
                            Button addBtn = new Button();
                            addBtn.ID = i;
                            addBtn.Text = "+";
                            addBtn.Click += addToList;
                            addBtn.Attributes.Add("class", "addBtn");
                            RecipeHolder.Controls.Add(new Literal { Text = "<li>" });
                            RecipeHolder.Controls.Add(addBtn);
                            RecipeHolder.Controls.Add(new Literal { Text = i + "</li>" });
                        }
                        
                        }
                        
                        RecipeHolder.Controls.Add(new Literal { Text = "</ul>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "</div>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "</div>" });

                        RecipeHolder.Controls.Add(new Literal { Text = "<div id=details>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "Calories: " + reader["calories"].ToString() });
                        RecipeHolder.Controls.Add(new Literal { Text = "<br>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "Servings: " + reader["servings"].ToString() });
                        RecipeHolder.Controls.Add(new Literal { Text = "</div>" });

                        RecipeHolder.Controls.Add(new Literal { Text = "<div>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "<h2> Steps: </h2>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "<div id=description>" });
                        RecipeHolder.Controls.Add(new Literal { Text = reader["description"].ToString().Replace("\r\n", "<br/>") });
                        RecipeHolder.Controls.Add(new Literal { Text = "</div>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "</div>" });
                    }
                }
                connection.Close();
                reader.Close();
            
        }

        protected void addToList(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
           
            string insert = "INSERT INTO [Products] (product, count) values (@product, @count)";
            connection.Open();
            SqlCommand insertCmd = new SqlCommand(insert, connection);
            insertCmd.Parameters.AddWithValue("@product", btn.ID);
            insertCmd.Parameters.AddWithValue("@count", 1);
            try
            {
                insertCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // 2627 is the error number for data duplicates
                {
                    countPlus(btn.ID);
                }
                else
                {
                    Response.Write(ex.Message);
                }
                connection.Close();
                return;
            }


            connection.Close();
            
        }

        protected void countPlus(string name)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string select = "SELECT count FROM [Products] WHERE product = '" + name + "'";
            SqlCommand cmd = new SqlCommand(select, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    count = Int32.Parse(reader["count"].ToString());
                }
            }
            connection.Close();

            count = count + 1;

            connection.Open();
            string delete = "UPDATE [Products] SET count=" + count + " WHERE product = '" + name + "'";
            cmd = new SqlCommand(delete, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx", false);
        }

        protected void deleteRec(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string delete = "DELETE FROM [RecipesDB] WHERE id =" + Request.QueryString["recipe"].ToString();
            SqlCommand cmd = new SqlCommand(delete, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            Response.Redirect("Main.aspx", false);
        }
    }
}