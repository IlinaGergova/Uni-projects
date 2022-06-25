<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRecipe.aspx.cs" Inherits="WebApplication2.AddRecipe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body{
            background-color:#e9c46a;
        }
        h1{
            color:#264653;
            font-size:50px;
            text-align:center;
        }
        div{
            display:grid;
            grid-template-columns: 45rem;
            grid-row-gap: 30px;
            justify-content:center;
        }
        #input-fields{
            background-color:white;
            border-radius:20px;
            padding:30px;
        }
        label{
            display:inline;
            color:#264653;
            font-size:20px;
            padding-left:30px;
            justify-self:center;
        }
        input, textarea{
            width:16rem;
            height:30px;
            border:2px solid #f4a261;
            border-radius:20px;
            margin-right:2rem;
            justify-self:center;
        }
        textarea{
            resize:vertical;
            margin-top:0;
        }
        #IngredientsList, #Steps{
            width:90%;
            height: 100px;
        }
        .textLabel{
            justify-self:start;
        }
        #DBsubmit, #back{
            text-align:center;
            background-color:#2a9d8f;
            color:white;
            padding:10px 0 10px 0;
            border:none;
            border-radius:3rem;
            width:150px;
            height:50px;
            justify-self:center;
            align-self:center;
            font-size:1.3rem;
        }
        #DBsubmit:hover, #back:hover{
            transform: scale(1.1);
            cursor:pointer;
            background-color:#264653;
            box-shadow:0em 0.5em 0.5em -0.5em #219ebc;
        }
        #nav{
            display:grid;
            grid-template-columns: 2rem 75rem;
        }
        section{
            display:grid;
            grid-template-columns:10rem 20rem 10rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="nav">
        <asp:Button ID="back" runat="server" Text="Back" OnClick="goBack" />
     <h1>Add new recipe</h1>
            </div>
        <div>
           
            <div id="input-fields">
                <section>
                     <label>Recipe name:</label>
                <asp:TextBox ID="RecipeName" runat="server" MaxLength="30" ValidateRequestMode="Enabled" ValidationGroup="validate" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="pass" runat="server" ValidationGroup="validate" ControlToValidate="RecipeName" 
                         ForeColor="Red"  Text="Input is required!" Enabled="true"></asp:RequiredFieldValidator>  
                </section>
                
            <section>
                <label>Image link:</label>
                <asp:TextBox ID="ImageURL" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
               
            </section>
               
           <section>
               <label>Calories:</label>
                <asp:TextBox ID="CaloriesNum" runat="server" ValidateRequestMode="Enabled" TextMode="Number" AutoCompleteType="Disabled"></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="validate" ControlToValidate="CaloriesNum"  
                    ForeColor="Red"  Text="Input is required!" Enabled="true"></asp:RequiredFieldValidator> 
               
           </section>
                
            <section>
                <label>Servings:</label>
                <asp:TextBox ID="ServingsNum" runat="server" ValidateRequestMode="Enabled" TextMode="Number" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="validate" ControlToValidate="ServingsNum"    
                    ForeColor="Red" Text="Input is required!" Enabled="true"></asp:RequiredFieldValidator> 
                
            </section>
                
                 
                <label class="textLabel">Ingredients:</label>
                <asp:TextBox ID="IngredientsList" runat="server" TextMode="MultiLine" ValidateRequestMode="Enabled" ValidationGroup="validate"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="validate" ControlToValidate="IngredientsList"  
                    ForeColor="Red" Text="Input is required!" Enabled="true"></asp:RequiredFieldValidator> 
              
                
                <label class="textLabel">Description:</label>
                <asp:TextBox ID="Steps" runat="server" TextMode="MultiLine" ValidateRequestMode="Enabled" ValidationGroup="validate"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Steps" ValidationGroup="validate"    
                    ForeColor="Red"  Text="Input is required!" Enabled="true"></asp:RequiredFieldValidator> 
                

                <asp:Button ID="DBsubmit" runat="server" Text="Add" OnClick="submitData" ValidationGroup="validate" ValidateRequestMode="Enabled" CausesValidation="true"/>
            </div>
        </div>
    </form>
</body>
</html>
