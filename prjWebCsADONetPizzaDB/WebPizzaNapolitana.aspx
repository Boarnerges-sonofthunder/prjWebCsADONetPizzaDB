<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebPizzaNapolitana.aspx.cs" Inherits="prjWebCsADONetPizzaDB.WebPizzaNapolitana" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="App_Themes/Style/Style.css" rel="stylesheet" type="text/css"/>
    
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 327px;
        }
        .auto-style3 {
            width: 520px;
            height: 636px;
            background-color: khaki;
            border-radius: 1px;
        }
        .auto-style4 {
            text-align: center;
        }
        .auto-style6 {
            width: 800px;
            height: 300px;
            background-color: khaki;
            border-radius: 1px;
            margin-left: 200px;
            text-align: center;
        }
        .auto-style9 {
            text-align: justify;
        }
        .auto-style10 {
            width: 506px;
            text-align: left;
        }
        .auto-style12 {
            width: 506px;
            text-align: left;
            height: 54px;
        }
    </style>
    
    </head>
<body>
    <form id="form1" runat="server">
        <div class="titre">
            <h1>PIZZARIA NAPOLITANA</h1>
        </div>
    <marquee><b>Savourez les belle croutes napolitaine chez Pizzaria Napolitana</b></marquee>
            <div class="flex-container">
        <div class="commande">
            <div class="auto-style4">
                Placez une commande<br />
            </div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">Livraison:<asp:CheckBox ID="chkLivraison" runat="server" AutoPostBack="True" OnCheckedChanged="chkLivraison_CheckedChanged" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">Téléphone:<asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnRechercher" runat="server" Text="Rechercher" Width="163px" OnClick="btnRechercher_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Nom:
                        <asp:TextBox ID="txtNom" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnEffacer" runat="server" OnClick="btnEffacer_Click" Text="Effacer" Width="165px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Email:<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblAdresse" runat="server" Text="Adresse:"></asp:Label>
                        <asp:TextBox ID="txtAdresse" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button class="btn btn-primary" ID="btnCreer" runat="server" Text="Creer une pizza" Width="164px" OnClick="btnCreer_Click" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="PanCreation" runat="server" Height="428px">
                <table class="auto-style8">
                    <tr>
                        <td class="auto-style12">
                            <asp:Label ID="lblTaille" runat="server" Text="Taille:"></asp:Label>
                            <br />
                            <asp:CheckBoxList ID="lstChkTaille" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" RepeatColumns ="2">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10">
                            <asp:Label ID="lblCroute" runat="server" Text="Croutes:"></asp:Label>
                            <asp:CheckBoxList ID="lstChkCroute" runat="server" AutoPostBack="True" Width="496px" RepeatDirection="Horizontal" RepeatColumns="2">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style10">
                            <asp:Label ID="lblIngredient" runat="server" Text="Ingredient:"></asp:Label>
                            <br />
                            <asp:CheckBoxList ID="lstChkIngredient" runat="server" AutoPostBack="True" Width="494px" RepeatDirection="Horizontal" RepeatColumns="2">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <br />
            
        </div>
        <div class="choixPizza">
            <div class="auto-style4">
                Choix de la pizza</div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style9">Pizza:<asp:ListBox ID="lstPizza" runat="server" AutoPostBack="True" Width="420px" ></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>Taille:<asp:DropDownList ID="cboTaille" runat="server" AutoPostBack="True" Height="25px" Width="186px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Extra:<asp:CheckBoxList ID="lstChkExtra" runat="server" AutoPostBack="True">
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </table>
        </div>
                <div class="auto-style6">Facture<br />
            <br />
            <asp:Panel ID="PanFacture" runat="server" Height="210px">
                <asp:Literal ID="LitFacture" runat="server"></asp:Literal>
            </asp:Panel>
        </div>
        </div>
    </form>
    </body>
</html>
