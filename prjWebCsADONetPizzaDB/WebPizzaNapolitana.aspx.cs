using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace prjWebCsADONetPizzaDB
{
    
    public partial class WebPizzaNapolitana : System.Web.UI.Page
    {
        static SqlConnection maConnexion;
        string facture;
        static decimal prixBase = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Page.IsPostBack == false)
            {
                lblAdresse.Visible = false;
                txtAdresse.Visible = false;
                PanCreation.Visible = false;
                //PanFacture.Visible = false;

                maConnexion = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PizzaDB;Integrated Security=True");
                maConnexion.Open();

                
                SqlCommand cmdSelectPizza = new SqlCommand("SELECT RefPizza,Nom FROM Pizzas",maConnexion);
                SqlDataReader pizzaReader = cmdSelectPizza.ExecuteReader();
                while(pizzaReader.Read())
                {
                    ListItem listePizzas = new ListItem();
                    listePizzas.Text = pizzaReader["Nom"].ToString();
                    listePizzas.Value = pizzaReader["RefPizza"].ToString();
                    lstPizza.Items.Add(listePizzas);
                    lstPizza.SelectedIndex = 0;

                }
                pizzaReader.Close();

                SqlCommand cmdSelectTaille = new SqlCommand("SELECT RefTaille,Taille FROM Tailles", maConnexion);
                SqlDataReader tailleReader = cmdSelectTaille.ExecuteReader();
                while(tailleReader.Read())
                {
                    ListItem listeTaille = new ListItem();
                    listeTaille.Text = tailleReader["Taille"].ToString();
                    listeTaille.Value = tailleReader["RefTaille"].ToString();
                    cboTaille.Items.Add(listeTaille);
                }
                tailleReader.Close();

                SqlCommand cmdSelectExtra = new SqlCommand("SELECT RefIngredient,Ingredient FROM Ingredients WHERE Ingredient IN('Fromage','Sauce')",maConnexion);
                SqlDataReader extraReader = cmdSelectExtra.ExecuteReader();
                while(extraReader.Read())
                {
                    ListItem listeExtra = new ListItem();
                    listeExtra.Text = extraReader["Ingredient"].ToString();
                    listeExtra.Value = extraReader["RefIngredient"].ToString();
                    lstChkExtra.Items.Add(listeExtra);
                    
                }
                extraReader.Close();

                maConnexion.Close();

            }
            else
            {
                PanFacture.Visible = true;
                afficherPrix();
            }
        }

        private void afficherPrix()
        {
            maConnexion.Open();
            Int32 refPiz = Convert.ToInt32(lstPizza.SelectedItem.Value);
            SqlCommand cmdPizza = new SqlCommand("SELECT Prix FROM Pizzas WHERE RefPizza = " + refPiz, maConnexion);
            SqlDataReader pizReader = cmdPizza.ExecuteReader();
            if (pizReader.Read())
            {
                prixBase = Convert.ToDecimal(pizReader["Prix"]);
            }
            pizReader.Close();

            Int32 refT = Convert.ToInt32(cboTaille.SelectedItem.Value);
            SqlCommand cmdTaille = new SqlCommand("SELECT Prix FROM Tailles WHERE RefTaille = " + refT, maConnexion);
            SqlDataReader tReader = cmdTaille.ExecuteReader();
            if (tReader.Read())
            {
                prixBase = prixBase * Convert.ToDecimal(tReader["Prix"]);
            }
            tReader.Close();

            Int32 refIn = Convert.ToInt32(lstChkExtra.SelectedItem.Value);
            SqlCommand cmdExtra = new SqlCommand("SELECT Prix FROM Ingredients WHERE RefIngredient = " + refIn, maConnexion);
            SqlDataReader inReader = cmdExtra.ExecuteReader(); 
            if(inReader.Read())
            {
                foreach (ListItem ingre in lstChkExtra.Items)
                {
                    if(ingre.Selected == true)
                    {
                        prixBase += Convert.ToDecimal(ingre.Value);
                    }
                }
                
            }
            inReader.Close();

            /*Int32 refIn2 = Convert.ToInt32(lstChkTaille.SelectedItem.Value);
            SqlCommand cmdIngredient = new SqlCommand("SELECT Prix FROM Ingredients WHERE RefIngredient = " + refIn2, maConnexion);
            SqlDataReader inReader2 = cmdIngredient.ExecuteReader();
            if (inReader2.Read())
            {
                foreach (ListItem ingre in lstChkTaille.Items)
                {
                    if (ingre.Selected == true)
                    {
                        prixBase += Convert.ToDecimal(ingre.Value);
                    }
                }

            }
            inReader2.Close();*/


            facture = "<hr/><br/>Sous-total :" + prixBase + "<br>";
            LitFacture.Text = facture;

            maConnexion.Close();
            
        }

        protected void chkLivraison_CheckedChanged(object sender, EventArgs e)
        {
            lblAdresse.Visible = txtAdresse.Visible = chkLivraison.Checked;
        }

        protected void btnRechercher_Click(object sender, EventArgs e)
        {
            maConnexion.Open();
            //Int32 refClient = Convert.ToInt32(txtTelephone.Text.ToString());
            String tel = txtTelephone.Text;
            SqlCommand maCommande = new SqlCommand("SELECT Nom,Adresse,NumTelephone,Email FROM Clients WHERE NumTelephone = @refTel", maConnexion);
            maCommande.Parameters.AddWithValue("refTel", tel);
            SqlDataReader monReader = maCommande.ExecuteReader();
            if(monReader.Read())
            {
                txtAdresse.Text = monReader["Adresse"].ToString();
                txtNom.Text = monReader["Nom"].ToString();
                txtTelephone.Text = monReader["NumTelephone"].ToString();
                txtEmail.Text = monReader["Email"].ToString();
            }

            maConnexion.Close();
        }

        protected void btnEffacer_Click(object sender, EventArgs e)
        {
            txtTelephone.Text = "";
            txtNom.Text = "";
            txtEmail.Text = "";
            txtAdresse.Text = "";
            txtTelephone.Focus();
        }

        protected void btnCreer_Click(object sender, EventArgs e)
        {
            PanCreation.Visible = true;
            maConnexion.Open();

            SqlCommand cmdSelectTaille = new SqlCommand("SELECT RefTaille,Taille FROM Tailles", maConnexion);
            SqlDataReader tailleReader = cmdSelectTaille.ExecuteReader();
            while (tailleReader.Read())
            {
                ListItem listeTaille = new ListItem();
                listeTaille.Text = tailleReader["Taille"].ToString();
                listeTaille.Value = tailleReader["RefTaille"].ToString();
                lstChkTaille.Items.Add(listeTaille);
            }
            tailleReader.Close();

            SqlCommand cmdSelectCroute = new SqlCommand("SELECT RefCroute,Croute FROM Croutes", maConnexion);
            SqlDataReader crouteReader = cmdSelectCroute.ExecuteReader();
            while(crouteReader.Read())
            {
                ListItem listeCroute = new ListItem();
                listeCroute.Text = crouteReader["Croute"].ToString();
                listeCroute.Value = crouteReader["RefCroute"].ToString();
                lstChkCroute.Items.Add(listeCroute);
            }
            crouteReader.Close();

            SqlCommand cmdSelectIngredient = new SqlCommand("SELECT RefIngredient,Ingredient FROM Ingredients", maConnexion);
            SqlDataReader ingredientReader = cmdSelectIngredient.ExecuteReader();
            while (ingredientReader.Read())
            {
                ListItem listeIngredient = new ListItem();
                listeIngredient.Text = ingredientReader["Ingredient"].ToString();
                listeIngredient.Value = ingredientReader["RefIngredient"].ToString();
                lstChkIngredient.Items.Add(listeIngredient);
            }
            ingredientReader.Close();

            maConnexion.Close();
        }

        /*protected void lstPizza_SelectedIndexChanged(object sender, EventArgs e)
        {
            maConnexion.Open();
            Int32 refPiz = Convert.ToInt32(lstPizza.SelectedItem.Value);
            SqlCommand maCommande = new SqlCommand("SELECT Prix FROM Pizzas WHERE RefPizza = " + refPiz,maConnexion);
            SqlDataReader monReader = maCommande.ExecuteReader();
            if(monReader.Read())
            {
                prixBase = Convert.ToDouble( monReader["Prix"]);
                //facture = "<hr/><br/>Sous-total :" + prixBase + "<br>";
            }

            //LitFacture.Text = facture;
            maConnexion.Close();
        }

        protected void cboTaille_SelectedIndexChanged(object sender, EventArgs e)
        {
            maConnexion.Open();
            Int32 refT = Convert.ToInt32(lstPizza.SelectedItem.Value);
            SqlCommand maCommande = new SqlCommand("SELECT Prix FROM Tailles WHERE RefTaille = " + refT, maConnexion);
            SqlDataReader monReader = maCommande.ExecuteReader();
            if (monReader.Read())
            {
                prixBase = prixBase * Convert.ToDouble(monReader["Prix"]);
            }
            maConnexion.Close();
        }*/
    }
}