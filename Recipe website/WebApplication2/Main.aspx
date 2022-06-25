<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebApplication2.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recipes</title>
    <style>
        body{
            background-color:#e9c46a;
        }
        h1{
            color:#264653;
            font-size:50px;
            text-align: center;
        }
        a{
            text-decoration:none;
            color:#2a9d8f;
        }
        a:hover{
            color:#f4a261;
        }
        section{
            padding:40px 30px 40px 30px;
            border-radius:2rem;
            width:70%;
            margin:auto;
            margin-top:100px;
            display:grid;
            grid-template-columns: 15rem 15rem 15rem 15rem;
            grid-row-gap: 30px;
            justify-content:center;
            background-color:#264653;
        }
        .rec{
            display:inline-block;
            border-radius:20px;
            width:200px;
            height:250px;
            background-color:white;
            justify-self: center;
        }
        .rec:hover{
            transform: translateY(-0.25em);
            box-shadow:0em 0.5em 0.5em -0.5em #e9c46a;
        }
        .rec h3{
            color:#2a9d8f;
            padding-left:20px;
        }
        .details{
            padding-left:20px;
            color:#264653;
        }
        img{
            width:100%;
            height:50%;
            border-radius:20px 20px 0 0;
        }
        #add{
            width:100%;
            height:50%;
            border-radius:20px 20px 0 0;
            font-size:30px;
            color:#e76f51;
            background-color:#f4a261;
            border:none;
        }
        #add:hover{
             color:#f4a261;
            background-color:#e76f51;
            cursor:pointer;
        }
        #showSidebar{
            float:right;
            text-align:center;
            background-color:#e76f51;
            color:white;
            padding:10px 0 10px 0;
            border:none;
            border-radius:3rem;
            width:150px;
            height:50px;
            font-size:1.3rem;
        }
        #showSidebar:hover{
            transform: scale(1.1);
            cursor:pointer;
            background-color:#f4a261;
            box-shadow:0em 0.5em 0.5em -0.5em #219ebc;
        }
        #intro{
            text-align: center;
            background-color:#2a9d8f;
            color:white;
            padding:10px;
            width:60%;
            margin:auto;
            border-radius:20px;
            font-size:larger;
        }
       #heading{
           display:flex;
           justify-content:center;
           column-gap:32rem;
           margin-left:38rem;
       }
       .sidebar{
           background-color:white;
           height:100%;
           width:30%;
           padding:20px;
           float:right;
           z-index:1;
           margin-top:-9rem;
       }
       .sidebarNav{
           display:flex;
           justify-content:space-between;
       }
       #closeBtn{
           float:right;
            text-align:center;
            background:none;
            color:#e76f51;
            padding:10px 0 10px 0;
            border:none;
            font-size:1.3rem;
       }
       #closeBtn:hover{
           cursor:pointer;
       }
       h2{
           color:#e76f51;
       }
       .inputNav{
           display:flex;
           justify-content:space-between;
       }
       input[type=checkbox]:checked + span.product {
            text-decoration: line-through;
       }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <div id="heading">
            <h1>Recipes</h1>
            <asp:Button ID="showSidebar" runat="server" Text="To buy list" OnClick="showList"></asp:Button>      
        </div>

        <asp:PlaceHolder ID="Sidebar" runat="server" Visible="false">
            <div class='sidebar'>
                <div class='sidebarNav'>
                    <h2>My list</h2>
                    <asp:Button ID="closeBtn" runat="server" Text="x" OnClick="hideList" />
                </div>
                
                <asp:PlaceHolder ID="items" runat="server" ></asp:PlaceHolder>
     
                <hr />
                <div class='inputNav'>
                    <asp:TextBox ID="inputField" runat="server"></asp:TextBox>
                    <asp:Button ID="addProduct" runat="server" Text="add" OnClick="addToList" />
                </div>
            </div>
        </asp:PlaceHolder>

        <p id="intro">Collect all your favourite recipes and save all products that you need in your To buy list!</p>
       <section>
            <asp:PlaceHolder ID="RecipesHolder" runat="server"></asp:PlaceHolder>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RecConnectionString %>" SelectCommand="SELECT * FROM [RecipesDB]"></asp:SqlDataSource>
            <div id="new-rec" class="rec">
                <asp:Button ID="add" runat="server" Text="+" OnClick="addRecipe" />
                <h3>Add new recipe</h3>
            </div>
       </section>
    </form>
</body>
</html>
