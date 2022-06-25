<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recipe.aspx.cs" Inherits="WebApplication2.Recipe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
         body{
            background-color:#e9c46a;
         }
        
         #recipeCard{
            background-color:#2a9d8f;
            padding:40px 30px 40px 30px;
            border-radius:2rem;
            width:70%;
            margin:auto;
            display:grid;
            grid-template-columns: 20rem 40rem;
            grid-row-gap: 30px;
            grid-column-gap: 30px;
            justify-content:center;
         }
         #foodImage{
             border-radius:2rem;
             width:20rem;
             align-self:center;
         }
         #ingredients{
             border-radius:2rem;
             background-color:#f4a261;
             padding:20px;
         }
         #description{
             border-radius:2rem;
             background-color:white;
             padding:20px;
         }
         #title{
            color:#264653;
            font-size:50px;
            text-align:center;
         }
         #details{
             color:white;
             font-weight:700;
         }
         h2{
             color:#e9c46a;
             margin-top:0px;
         }
         #back{
             text-align:center;
             padding:10px 0 10px 0;
            border:none;
            border-radius:3rem;
            width:150px;
            height:50px;
            font-size:1.3rem;
            background-color:#2a9d8f;
            color:white;
            justify-self:center;
            align-self:center;
            
        }
        #back:hover{
            transform: scale(1.1);
            cursor:pointer;
            background-color:#264653;
            box-shadow:0em 0.5em 0.5em -0.5em #219ebc;
        }
        #delete{
            text-align:center;
             padding:10px 0 10px 0;
            border:none;
            border-radius:3rem;
            width:150px;
            height:50px;
            font-size:1.3rem;
            background-color:#e76f51;
            color:white;
            margin-left:72rem;
            margin-bottom:2rem;
        }
        #delete:hover{
            transform: scale(1.1);
            cursor:pointer;
            background-color:#f4a261;
            color:#e76f51;
            box-shadow:0em 0.5em 0.5em -0.5em white;
        }
        #nav{
            display:grid;
            grid-template-columns: 10rem 75rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="nav">
             <asp:Button ID="back" runat="server" Text="Back" OnClick="goBack" />
            <asp:PlaceHolder ID="TitleHolder" runat="server"></asp:PlaceHolder>
        </div>
       
        <asp:Button ID="delete" runat="server" Text="Delete" OnClick="deleteRec" />
        <div id="recipeCard">
            <asp:PlaceHolder ID="RecipeHolder" runat="server">
                <asp:Image ID="foodImage" runat="server" />
            </asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
