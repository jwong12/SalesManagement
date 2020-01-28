using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLib.Business;
using BusinessLib.Common;
using BusinessLib.Data;

namespace ClientsManagement
{
    public partial class ClientEditDialog : Form
    {
        public ClientViewModel ClientVM { get; set; }
        public bool IsAddClient { get; set; }

        public ProvinceCollection provinces;

        public ClientEditDialog(bool isAddClient)
        {
            IsAddClient = isAddClient;
            InitializeComponent();
        }

        private void ClientEditDialog_Load(object sender, EventArgs e)
        {
            provinces = ProvinceRepository.GetProvinces();
            setBindings();
            populateComboBox();

            if (IsAddClient) // AddClient Dialog
            {
                maskedTextBoxClientCode.ReadOnly = false;
            }
            else // EditClient Dialog
            {
                selectProvince();
                textBoxCompanyName.Select();
                textBoxCompanyName.SelectAll();
            }
        }

        private void setBindings()
        {
            maskedTextBoxClientCode.DataBindings.Add("Text", ClientVM, "Client.ClientCode");
            textBoxCompanyName.DataBindings.Add("Text", ClientVM, "Client.CompanyName");
            textBoxAddress1.DataBindings.Add("Text", ClientVM, "Client.AddressOne");
            textBoxAddress2.DataBindings.Add("Text", ClientVM, "Client.AddressTwo");
            textBoxCity.DataBindings.Add("Text", ClientVM, "Client.City");
            comboBoxProvinces.DataBindings.Add("Text", ClientVM, "Client.Province");
            maskedTextBoxPostalCode.DataBindings.Add("Text", ClientVM, "Client.PostalCode");
            textBoxYTDSales.DataBindings.Add("Text", ClientVM, "Client.YTDSales", true, DataSourceUpdateMode.OnValidation, "0.00", "#,##0.00;(#,##0.00);0.00");
            checkBoxCreditHold.DataBindings.Add("Checked", ClientVM, "Client.CreditHold");
            textBoxNotes.DataBindings.Add("Text", ClientVM, "Client.Notes");
        }

        private void populateComboBox()
        {
            comboBoxProvinces.Items.Clear();

            foreach (Province province in provinces)
            {
                comboBoxProvinces.Items.Add(province.Abbreviation);
            }
        }

        // Display the Client's Province in the ComboBox
        private void selectProvince()
        {
            if (ClientVM.Client.Province != null)
            {
                for (int i = 0; i < provinces.Count; i++)
                {
                    if (provinces[i].Abbreviation == ClientVM.Client.Province)
                    {
                        comboBoxProvinces.SelectedIndex = i;
                        break;
                    }
                }
            }        
        }        

        private void buttonOK_Click(object sender, EventArgs e)
        {
            errorProviderClient.SetError(buttonOK, string.Empty);

            if (IsAddClient) // AddClient Dialog
            {
                Client dialogNewClient = ClientVM.GetDisplayClient();

                if (!ClientVM.Clients.ContainsClientCode(dialogNewClient))
                {
                    int rowsAffected = ClientValidation.AddClient(dialogNewClient);

                    if (rowsAffected == 0)
                    {
                        errorProviderClient.SetError(buttonOK, "No DB changes were made");
                    }
                    else if (rowsAffected < 0)
                    {
                        errorProviderClient.SetError(buttonOK, ClientValidation.ErrorMessage);
                    }
                    else 
                    {
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    errorProviderClient.SetError(buttonOK, "The ClientCode already exists");
                }
            }
            else // EditClient Dialog
            {
                Client dialogUpdatedClient = ClientVM.GetDisplayClient();
                int rowsAffected = ClientValidation.UpdateClient(dialogUpdatedClient); 

                if (rowsAffected == 0)
                {
                    errorProviderClient.SetError(buttonOK, "No DB changes were made");
                }
                else if (rowsAffected < 0)
                {
                    errorProviderClient.SetError(buttonOK, ClientValidation.ErrorMessage);
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }        
    }
}
