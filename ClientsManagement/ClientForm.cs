using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLib.Common;
using BusinessLib.Business;

namespace ClientsManagement
{
    public partial class ClientForm : Form
    {
        private ClientViewModel clientVM;
        public ClientForm()
        {
            InitializeComponent();
        }
        

        private void ClientForm_Load(object sender, EventArgs e)
        {
            try
            {
                clientVM = new ClientViewModel();
                setBindings(); 
                setupDataGridView();
                refreshClientLabels();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void setBindings()
        {
            dataGridViewClients.AutoGenerateColumns = false;
            dataGridViewClients.DataSource = clientVM.Clients;
        }

        private void setupDataGridView()
        {
            dataGridViewClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClients.MultiSelect = false;
            dataGridViewClients.AllowUserToAddRows = false;
            dataGridViewClients.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridViewClients.AllowUserToOrderColumns = false;
            dataGridViewClients.AllowUserToResizeColumns = false;
            dataGridViewClients.AllowUserToResizeRows = false;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            DataGridViewTextBoxColumn code = new DataGridViewTextBoxColumn();
            code.Name = "code";
            code.DataPropertyName = "ClientCode";
            code.HeaderText = "Id";
            code.Width = 50;
            code.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(code);

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.Name = "name";
            name.DataPropertyName = "CompanyName";
            name.HeaderText = "Company Name";
            name.Width = 135;
            name.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(name);

            DataGridViewTextBoxColumn address1 = new DataGridViewTextBoxColumn();
            address1.Name = "address1";
            address1.DataPropertyName = "AddressOne";
            address1.HeaderText = "Address 1";
            address1.Width = 130;
            address1.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(address1);

            DataGridViewTextBoxColumn address2 = new DataGridViewTextBoxColumn();
            address2.Name = "address2";
            address2.DataPropertyName = "AddressTwo";
            address2.HeaderText = "Address 2";
            address2.Width = 85;
            address2.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(address2);

            DataGridViewTextBoxColumn city = new DataGridViewTextBoxColumn();
            city.Name = "city";
            city.DataPropertyName = "City";
            city.HeaderText = "City";
            city.Width = 75;
            city.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(city);

            DataGridViewTextBoxColumn province = new DataGridViewTextBoxColumn();
            province.Name = "province";
            province.DataPropertyName = "Province";
            province.HeaderText = "Prov";
            province.Width = 40;
            province.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(province);

            DataGridViewTextBoxColumn postal = new DataGridViewTextBoxColumn();
            postal.Name = "postal";
            postal.DataPropertyName = "PostalCode";
            postal.HeaderText = "Postal Code";
            postal.Width = 55;
            postal.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(postal);

            DataGridViewTextBoxColumn sales = new DataGridViewTextBoxColumn();
            sales.Name = "sales";
            sales.DataPropertyName = "YTDSales";
            sales.HeaderText = "YTD Sales";
            sales.Width = 90;
            sales.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            sales.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            sales.DefaultCellStyle.Format = "N2";
            sales.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(sales);

            DataGridViewCheckBoxColumn creditHold = new DataGridViewCheckBoxColumn();
            creditHold.Name = "creditHold";
            creditHold.DataPropertyName = "CreditHold";
            creditHold.HeaderText = "Credit Hold";
            creditHold.Width = 40;
            creditHold.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            creditHold.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(creditHold);

            DataGridViewTextBoxColumn notes = new DataGridViewTextBoxColumn();
            notes.Name = "notes";
            notes.DataPropertyName = "Notes";
            notes.HeaderText = "Notes";
            notes.Width = 120;
            notes.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewClients.Columns.Add(notes);
        } 
      
        private void buttonEditClient_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridViewClients.CurrentRow.Index;
                Client client = clientVM.Clients[index];
                clientVM.SetDisplayClient(client);
                ClientEditDialog dialog = new ClientEditDialog(false);
                dialog.Text = "Edit Client Information";
                dialog.ClientVM = clientVM;

                if (dialog.ShowDialog() == DialogResult.OK)
                {                
                    Client dialogUpdatedClient = dialog.ClientVM.GetDisplayClient();
                    clientVM.Clients[index] = dialogUpdatedClient; 
                    clientVM.Clients.ResetItem(index);
                    refreshClientLabels();
                }

                dialog.Dispose();
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddClient_Click(object sender, EventArgs e)
        {
            try
            {
                clientVM.SetDisplayClient(new Client { ClientCode = "" });
                ClientEditDialog dialog = new ClientEditDialog(true); 
                dialog.Text = "Add New Client";
                dialog.ClientVM = clientVM;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    refreshDataGridView();
                    refreshClientLabels();
                }
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void buttonDeleteClient_Click(object sender, EventArgs e)
        {
            try 
            {
                int index = dataGridViewClients.CurrentRow.Index;
                Client client = clientVM.Clients[index];

                if (checkBoxConfirmDelection.Checked)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this record?",
                                             "Record Deletion",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        ClientValidation.DeleteClient(client);
                        refreshDataGridView();
                        refreshClientLabels();
                    }
                }
                else
                {
                    ClientValidation.DeleteClient(client);
                    refreshDataGridView();
                    refreshClientLabels();
                }                               
            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Processing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

        private void refreshDataGridView()
        {
            clientVM.Clients = ClientValidation.GetClients();
            dataGridViewClients.DataSource = clientVM.Clients;
        }

        private void refreshClientLabels()
        {
            labelTotalRecords.Text = $"{"Total Records:",-18}{clientVM.Clients.Count.ToString(),3}";
            labelCreditHoldCount.Text = $"{"Total CreditHold:",-18}{clientVM.Clients.CreditHoldCount.ToString(),3}";
            labelTotalYTDSales.Text = "Total YTDSales: " + clientVM.Clients.TotalYTDSales.ToString("C");
        }
    }
}
