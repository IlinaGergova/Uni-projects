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
            //this.loadList(sender, e);
            this.loadData(sender, e);
        }

        
        protected void loadData(object sender, EventArgs e)
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
                    viewRecipeLink.ID = "recipeLink" + reader["name"].ToString();
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
                reader.Close();

                connection.Close();
            }
            this.loadList(sender, e);
        }

        protected void loadList(object sender, EventArgs e)
        {
            items.Controls.Clear();
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
                connection.Open();
                string select = "SELECT * FROM [Products]";
                SqlCommand selectCmd = new SqlCommand(select, connection);
                SqlDataReader reader = selectCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        addButtons(reader["product"].ToString(), reader["count"].ToString());
                        //Button deleteBtn = new Button();
                        //deleteBtn.Text = "x";
                        //deleteBtn.UseSubmitBehavior = false;
                        //deleteBtn.Attributes.Add("class", reader["product"].ToString());
                        //deleteBtn.Click += deleteItem;

                        //Button addMore = new Button();
                        //addMore.Text = "+";
                        //addMore.UseSubmitBehavior = false;
                        //addMore.Attributes.Add("class", reader["product"].ToString());
                        //addMore.Click += countPlus;

                        //Button removeOne = new Button();
                        //removeOne.Text = "-";
                        //removeOne.UseSubmitBehavior = false;
                        //removeOne.Attributes.Add("class", reader["product"].ToString());
                        //removeOne.Click += countMinus;


                        //items.Controls.Add(new Literal { Text = "<li>" });
                        //items.Controls.Add(deleteBtn);
                        //items.Controls.Add(addMore);
                        //items.Controls.Add(removeOne);
                        //items.Controls.Add(new Literal { Text = "<span>" });
                        //string number = reader["count"].ToString();
                        //items.Controls.Add(new Literal { Text = reader["count"].ToString() });
                        //items.Controls.Add(new Literal { Text = "</span>" });
                        //items.Controls.Add(new Literal { Text = reader["product"].ToString() });
                        //items.Controls.Add(new Literal { Text = "</li>" });

                    }
                }
                connection.Close();
            
        }

        protected void addRecipe(object sender, EventArgs e)
        {
            Response.Redirect("AddRecipe.aspx", false);
        }

        protected void showList(object sender, EventArgs e)
        {
            Sidebar.Visible = true;
        }

        protected void hideList(object sender, EventArgs e)
        {
            inputField.Text = "";
            Sidebar.Visible = false;
        }

        protected void addToList(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            showList(sender, e);

            if (inputField.Text != "")
            {

                string insert = "INSERT INTO [Products] (product,count) values (@product,@count)";
                connection.Open();
                SqlCommand insertCmd = new SqlCommand(insert, connection);
                insertCmd.Parameters.AddWithValue("@product", inputField.Text);
                insertCmd.Parameters.AddWithValue("@count", 1);
                try
                {
                    insertCmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if(ex.Number == 2627) // 2627 is the error number for data duplicates
                    {
                        countPlusInput(inputField.Text,sender,e);
                    }
                    else
                    {
                        Response.Write(ex.Message);
                    }
                    inputField.Text = "";
                    connection.Close();
                    return;
                }
                connection.Close();
                //Button newDeleteBtn = new Button();
                //newDeleteBtn.Text = "x";
                //newDeleteBtn.UseSubmitBehavior = false;
                //newDeleteBtn.Attributes.Add("class", inputField.Text);
                //newDeleteBtn.Click += deleteItem;

                //Button newAddMore = new Button();
                //newAddMore.Text = "+";
                //newAddMore.UseSubmitBehavior = false;
                //newAddMore.Attributes.Add("class", inputField.Text);
                //newAddMore.Click += countPlus;

                //Button newRemoveOne = new Button();
                //newRemoveOne.Text = "-";
                //newRemoveOne.UseSubmitBehavior = false;
                //newRemoveOne.Attributes.Add("class", inputField.Text);
                //newRemoveOne.Click += countMinus;

                //items.Controls.Add(new Literal { Text = "<li>" });
                //items.Controls.Add(newDeleteBtn);
                //items.Controls.Add(newAddMore);
                //items.Controls.Add(newRemoveOne);
                //items.Controls.Add(new Literal { Text = "<span> 1 </span>" });
                //items.Controls.Add(new Literal { Text = inputField.Text });
                //items.Controls.Add(new Literal { Text = "</li>" });

                addButtons(inputField.Text, "1");
                inputField.Text = "";
                loadList(sender, e);
            }
        }

        protected void addButtons(string product, string count)
        {
            Button deleteBtn = new Button();
            deleteBtn.Text = "x";
            deleteBtn.UseSubmitBehavior = false;
            deleteBtn.Attributes.Add("class", product);
            deleteBtn.Click += deleteItem;

            Button addMore = new Button();
            addMore.Text = "+";
            addMore.UseSubmitBehavior = false;
            addMore.Attributes.Add("class", product);
            addMore.Click += countPlus;

            Button removeOne = new Button();
            removeOne.Text = "-";
            removeOne.UseSubmitBehavior = false;
            removeOne.Attributes.Add("class", product);
            removeOne.Click += countMinus;


            items.Controls.Add(new Literal { Text = "<li>" });
            items.Controls.Add(deleteBtn);
            items.Controls.Add(addMore);
            items.Controls.Add(removeOne);
            items.Controls.Add(new Literal { Text = "<span>" });
            items.Controls.Add(new Literal { Text = count });
            items.Controls.Add(new Literal { Text = "</span>" });
            items.Controls.Add(new Literal { Text = product });
            items.Controls.Add(new Literal { Text = "</li>" });
        }
        protected void deleteItem(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string delete = "DELETE FROM [Products] WHERE product = '" + btn.Attributes["class"] + "'";
            SqlCommand cmd = new SqlCommand(delete, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            loadList(sender, e);
        }

        protected void countPlus(object sender, EventArgs e)
        {
            //Form.Controls.Remove(Sidebar);
            Button btn = sender as Button;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();

            int count = getCount(sender,e) + 1;
            
            string delete = "UPDATE [Products] SET count="+ count+" WHERE product = '" + btn.Attributes["class"] + "'";
            SqlCommand cmd = new SqlCommand(delete, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            loadList(sender, e);
        }
        protected int getCount(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string select = "SELECT count FROM [Products] WHERE product = '" + btn.Attributes["class"] + "'";
            SqlCommand cmd = new SqlCommand(select, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return count = Int32.Parse(reader["count"].ToString());
                }
            }
            connection.Close();
            return -1;
        }
        protected void countPlusInput(string productName, object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string select = "SELECT count FROM [Products] WHERE product = '" + productName + "'";
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
            string delete = "UPDATE [Products] SET count=" + count + " WHERE product = '" + productName + "'";
            cmd = new SqlCommand(delete, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            loadList(sender, e);
        }
        protected void countMinus(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecConnectionString"].ConnectionString);
            connection.Open();
            string select = "SELECT count FROM [Products] WHERE product = '" + btn.Attributes["class"] + "'";
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

            count = count - 1;

            connection.Open();
            string update = "";
            if(count <= 0)
            {
                update = "DELETE FROM [Products] WHERE product = '" + btn.Attributes["class"] + "'";
            }
            else
            {
                update = "UPDATE [Products] SET count=" + count + " WHERE product = '" + btn.Attributes["class"] + "'";
            }
            
            cmd = new SqlCommand(update, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            loadList(sender, e);
        }
    }
    }