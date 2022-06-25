using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace WebApplication2
{
    public partial class Recipe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["recipe"]==null)
                //{
                //    Response.Write("Database connection error!");
                //}
                //Response.Write(Request.QueryString["recipe"].ToString());
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
                        RecipeHolder.Controls.Add(new Literal { Text = reader["ingredients"].ToString().Replace("\r\n", "<br />") });

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
                        RecipeHolder.Controls.Add(new Literal { Text = reader["description"].ToString().Replace("\r\n", "<br />") });
                        RecipeHolder.Controls.Add(new Literal { Text = "</div>" });
                        RecipeHolder.Controls.Add(new Literal { Text = "</div>" });
                    }
                }
                connection.Close();
                reader.Close();
            }
            
        }
        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx", false);
        }

        protected void deleteRec(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string select = "DELETE FROM [RecipesDB] WHERE id =" + Request.QueryString["recipe"].ToString();
            SqlCommand cmd = new SqlCommand(select, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            Response.Redirect("Main.aspx", false);
        }
    }
}