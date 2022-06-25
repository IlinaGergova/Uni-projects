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
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string JQueryVer = "1.7.1";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-" + JQueryVer + ".min.js",
                DebugPath = "~/Scripts/jquery-" + JQueryVer + ".js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-" + JQueryVer + ".js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });

            if (!IsPostBack)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
                connection.Open();
                string select = "SELECT id,name,image,calories,servings FROM [RecipesDB]";
                SqlCommand cmd = new SqlCommand(select, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LinkButton viewRecipeLink = new LinkButton();
                        viewRecipeLink.ID = "recipeLink";
                        viewRecipeLink.Attributes.Add("runat", "server");
                        RecipesHolder.Controls.Add(new Literal { Text = "<div class='rec'>" });
                        if (reader["image"].ToString() == "NULL")
                        {
                            RecipesHolder.Controls.Add(new Literal { Text = "<img src='" + "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6c/No_image_3x4.svg/1280px-No_image_3x4.svg.png" + "'>" });
                        }
                        else
                        {
                            RecipesHolder.Controls.Add(new Literal { Text = "<img src='" + reader["image"] + "'>" });
                        }
                        
                        RecipesHolder.Controls.Add(new Literal { Text = "<h3>" });
                        viewRecipeLink.Text = reader["name"].ToString();
                        viewRecipeLink.Attributes.Add("href", "Recipe.aspx?recipe=" + reader["id"].ToString());
                        RecipesHolder.Controls.Add(viewRecipeLink);
                        RecipesHolder.Controls.Add(new Literal { Text = "</h3>" });
                        RecipesHolder.Controls.Add(new Literal { Text = "<p class='details'>" + "Calories: " + reader["calories"] + " Servings: " + reader["servings"] + "</p>" });
                        RecipesHolder.Controls.Add(new Literal { Text = "</div>" });
                    }
                }


                reader.Close();

                //int temp = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //if (temp == 1)
                //{
                //    Response.Write("Student Already Exist");
                //}

                connection.Close();
            }
        }

        protected void addRecipe(object sender, EventArgs e)
        {
            Response.Redirect("AddRecipe.aspx", false);
        }

        protected void showList(object sender, EventArgs e)
        {
            Form.Controls.Remove(showSidebar);
            Sidebar.Visible = true;
           
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string select = "SELECT product FROM [Products]";
            SqlCommand selectCmd = new SqlCommand(select, connection);
            SqlDataReader reader = selectCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CheckBox item = new CheckBox();
                    item.Attributes.Add("id", reader["product"].ToString());
                    item.Attributes.Add("name", reader["product"].ToString());

                    Label label = new Label();
                    label.Attributes.Add("for", reader["product"].ToString());
                    label.Text = reader["product"].ToString();
                    label.Attributes.Add("class", "product");

                    items.Controls.Add(new Literal { Text = "<div class='item'>" });
                    items.Controls.Add(item);
                    items.Controls.Add(label);
                    items.Controls.Add(new Literal { Text = "</div>" });
                }
            }
            connection.Close();
        }

        protected void hideList(object sender, EventArgs e)
        {
            Form.Controls.Remove(Sidebar);  
        }

        protected void addToList(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            showList(sender, e);

            CheckBox item = new CheckBox();
            item.Attributes.Add("id", inputField.Text);
            item.Attributes.Add("name", inputField.Text);
            
            Label label = new Label();
            label.Attributes.Add("for", inputField.Text);
            label.Attributes.Add("class", "product");
            label.Text = inputField.Text;
            
            if (item.Checked)
            {
                label.Style["text-decoration"] = "line-through";
            }

            items.Controls.Add(new Literal { Text = "<div class='item'>" });
            items.Controls.Add(item);
            items.Controls.Add(label);
            items.Controls.Add(new Literal { Text = "</div>" });

            string insert = "INSERT INTO [Products] (product) values (@product)";
            connection.Open();
            SqlCommand insertCmd = new SqlCommand(insert, connection);
            insertCmd.Parameters.AddWithValue("@product", inputField.Text);
            insertCmd.ExecuteNonQuery();
            connection.Close();
            inputField.Text = "";
        }
    }
}