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
    public partial class AddRecipe : System.Web.UI.Page
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

            CaloriesNum.Attributes.Add("min", "0");
            ServingsNum.Attributes.Add("min", "0");
        }

        protected void goBack(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx", false);
        }

        protected void submitData(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
                connection.Open();

                string insert = "INSERT INTO [RecipesDB] (name,image,servings,calories,ingredients, description) values (@recipename,@imageurl,@calories,@servings,@ingredients,@description)";


                SqlCommand cmd = new SqlCommand(insert, connection);
                cmd.Parameters.AddWithValue("@recipename", RecipeName.Text);

                if (Uri.IsWellFormedUriString(ImageURL.Text, UriKind.Absolute))
                {
                    cmd.Parameters.AddWithValue("@imageurl", ImageURL.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@imageurl", "NULL");
                }

                cmd.Parameters.AddWithValue("@calories", Int32.Parse(CaloriesNum.Text));
                cmd.Parameters.AddWithValue("@servings", Int32.Parse(ServingsNum.Text));
                cmd.Parameters.AddWithValue("@ingredients", IngredientsList.Text);
                cmd.Parameters.AddWithValue("@description", Steps.Text);
                cmd.ExecuteNonQuery();
                connection.Close();

                RecipeName.Text = "";
                ImageURL.Text = "";
                CaloriesNum.Text = "";
                ServingsNum.Text = "";
                IngredientsList.Text = "";
                Steps.Text = "";
            }
            //Response.Redirect("Main.aspx", false);
        }
    }
}