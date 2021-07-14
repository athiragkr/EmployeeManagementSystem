using EmployeeManagementSystem.Classes;
using EmployeeManagementSystem.Helper;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class FrmEmployee : Form
    {
        RestHelper restHelper = new RestHelper();
        Rootobject result = new Rootobject();
        int index;
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                BindGender();
                BindStatus();
                GetEmployeeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Save Employee Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtEmail.Text == "")
                {
                    // display popup box  
                    MessageBox.Show("Please fill in all fields", "Error");
                    txtName.Focus(); // set focus to NameTextBox  
                    return;
                }
                else
                {
                    Datum empData = new Datum();
                    empData.name = txtName.Text;
                    empData.email = txtEmail.Text;
                    empData.gender = cmbGender.SelectedValue.ToString();
                    empData.status = cmbStatus.SelectedValue.ToString();
                    result.data.Add(empData);
                    var inputData = new Dictionary<string, string>
                {
                    { "email", empData.email},
                    { "name", empData.name},
                    { "gender", empData.gender},
                    { "status", empData.status}
                };
                    var response = await restHelper.Post(inputData);
                    JObject json = JObject.Parse(response);
                    int statusValue = (int)json["code"];
                    if (statusValue == 201)
                    {
                        MessageBox.Show("User saved successfully", "Success");
                        ClearTextBoxes();
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Update Employee Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtEmail.Text == "")
                {
                    // display popup box  
                    MessageBox.Show("Please select any data to update", "Error");
                    txtName.Focus(); // set focus to NameTextBox  
                    return;
                }
                else
                {
                    DataGridViewRow newData = dataGridViewEmp.Rows[index];
                    newData.Cells[1].Value = txtName.Text;
                    newData.Cells[2].Value = txtEmail.Text;
                    newData.Cells[3].Value = cmbGender.SelectedValue.ToString();
                    newData.Cells[4].Value = cmbStatus.SelectedValue.ToString();

                    Datum empData = new Datum();
                    empData.name = txtName.Text;
                    empData.email = txtEmail.Text;
                    empData.gender = cmbGender.SelectedValue.ToString();
                    empData.status = cmbStatus.SelectedValue.ToString();
                    empData.id = Convert.ToInt32(newData.Cells[0].Value);
                    result.data.Add(empData);

                    var response = await restHelper.Put(empData);
                    JObject json = JObject.Parse(response); //this is thr string     
                    int statusValue = (int)json["code"];
                    if (statusValue == 200)
                    {
                        MessageBox.Show("User updated Successfully", "Success");
                        ClearTextBoxes();
                        //GetEmployeeData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ClearTextBoxes()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtSearch.Text = "";
            cmbGender.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Delete Employee data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow newData = dataGridViewEmp.Rows[index];
                Datum empData = new Datum();
                empData.name = txtName.Text;
                empData.email = txtEmail.Text;
                empData.gender = cmbGender.SelectedValue.ToString();
                empData.status = cmbStatus.SelectedValue.ToString();
                empData.id = Convert.ToInt32(newData.Cells[0].Value);
                result.data.Add(empData);

                var response = await restHelper.Delete(empData);
                JObject json = JObject.Parse(response);      
                int statusValue = (int)json["code"];
                if (statusValue == 204)
                {
                    MessageBox.Show("User deleted Successfully", "Success");
                    GetEmployeeData();
                    ClearTextBoxes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private async void GetEmployeeData()
        {
            try
            {
                var response = await restHelper.GetAllEmployees();
                result = JsonConvert.DeserializeObject<Rootobject>(response);
                dataGridViewEmp.DataSource = result.data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Data fetched By page No. If non existing page number is passed, then last page results will be returned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await restHelper.GetByPageNo(Convert.ToInt32(txtSearch.Text));
                result = JsonConvert.DeserializeObject<Rootobject>(response);

                if (result.data.Count == 0)
                {
                    response = await restHelper.GetByPageNo(1);
                    result = JsonConvert.DeserializeObject<Rootobject>(response);
                }

                dataGridViewEmp.DataSource = result.data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Bind Gender in ComboBox
        /// </summary>
        private void BindGender()
        {
            try
            {
                List<Gender> genders = new List<Gender> { new Gender("Male"),
                                     new Gender("Female") };

                var bindingSource = new BindingSource();
                bindingSource.DataSource = genders;

                cmbGender.DataSource = bindingSource.DataSource;

                cmbGender.DisplayMember = "Name";
                cmbGender.ValueMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Bind Status in Combobox
        /// </summary>
        private void BindStatus()
        {
            try
            {
                List<Status> status = new List<Status> { new Status("Active"),
                                     new Status("InActive") };

                var bindingSource = new BindingSource();
                bindingSource.DataSource = status;

                cmbStatus.DataSource = bindingSource.DataSource;

                cmbStatus.DisplayMember = "EStatus";
                cmbStatus.ValueMember = "EStatus";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Gender Selection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gender gender = cmbGender.SelectedItem as Gender;
        }

        /// <summary>
        /// Status Combobox value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Status status = cmbStatus.SelectedItem as Status;
        }

        /// <summary>
        /// Datagridview cell click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEmp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                index = e.RowIndex;
                DataGridViewRow row = dataGridViewEmp.Rows[index];
                txtName.Text = row.Cells[1].Value.ToString();
                txtEmail.Text = row.Cells[2].Value.ToString();
                cmbGender.Text = row.Cells[3].Value.ToString();
                cmbStatus.Text = row.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        /// <summary>
        /// Making DataGrid cell value readonly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewEmp.ReadOnly = true;
        }

        /// <summary>
        /// Serach Text Box Validation to accept only numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtExport_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = "C:";
                saveFileDialog1.Title = "Save as Excel File";
                saveFileDialog1.FileName = "Employee Details";
                saveFileDialog1.Filter = "Excel Files(2003)|*.xls|Excel Files(2007)|*.xlsx";
                if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                    ExcelApp.Application.Workbooks.Add(Type.Missing);
                    ExcelApp.Columns.ColumnWidth = 20;
                    //Storing header part in Excel
                    for (int i = 1; i < dataGridViewEmp.Columns.Count + 1; i++)
                    {
                        ExcelApp.Cells[1, i] = dataGridViewEmp.Columns[i - 1].HeaderText;
                    }
                    //Storing each row and column value to excel sheet
                    for (int i = 0; i < dataGridViewEmp.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridViewEmp.Columns.Count; j++)
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dataGridViewEmp.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                    ExcelApp.ActiveWorkbook.Saved = true;
                    ExcelApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }

    /// <summary>
    /// Class for Json Deserialization
    /// </summary>
    public class Rootobject
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
    public class Datum
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("gender")]
        public string gender { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
    }
}
