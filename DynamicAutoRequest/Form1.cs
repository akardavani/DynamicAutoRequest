using BusinessService;
using BusinessService.Json;
using BusinessService.SendRequest;
using Domain.Enum;
using Domain.Model;
using DynamicAutoRequest.BusinessService;
using System.ComponentModel.DataAnnotations;

namespace DynamicAutoRequest
{
    public partial class Form1 : Form
    {
        RequestTimeData _requestTimeData;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ComboBox_Load();

            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            _requestTimeData = JsonConvertor.ReadJsonData<RequestTimeData>(JsonFileNames.RequestTimeData, jsonFolderPath);

            txtBatchSize.Text = _requestTimeData.BatchSize.ToString();
            txtTotalRequests.Text = _requestTimeData.TotalRequests.ToString();
            txtDelay.Text = _requestTimeData.Delay.ToString();
            txtRequestTime.Text = _requestTimeData.StartRequestTime.ToString();
            txtStart.Text = _requestTimeData.StartTime.ToString();
            txtEnd.Text = _requestTimeData.EndTime.ToString();

            cmbProvider.SelectedValue = (OmsProvider)_requestTimeData.OmsProvider;
            cmbProvider.SelectedIndex = _requestTimeData.OmsProvider - 1;

        }

        private void ComboBox_Load()
        {
            var items = Enum.GetValues(typeof(OmsProvider))
                            .Cast<OmsProvider>()
                            .Select(x => new { Value = x, Text = GetEnumDisplayName(x) })
                            .ToList();

            cmbProvider.DataSource = items;
            cmbProvider.DisplayMember = "Text";
            cmbProvider.ValueMember = "Value";
        }

        public static string GetEnumDisplayName(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var displayAttribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
            return displayAttribute?.Name ?? value.ToString();
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            SaveRequestTimeData();

            if (!string.IsNullOrEmpty(txtRequest.Text))
            {
                //CreateDynamicData.SaveJson(txtRequest.Text, chkAdd.Checked, chkLog.Checked);               

                BaseSaveData.SaveData(_requestTimeData.OmsProvider, txtRequest.Text);

                MessageBox.Show("حله !!!");
            }
        }

        private void SaveRequestTimeData()
        {
            //_requestTimeData.BatchSize = Convert.ToInt32(txtBatchSize.Text);
            //_requestTimeData.TotalRequests = Convert.ToInt32(txtTotalRequests.Text);
            //_requestTimeData.Delay = Convert.ToInt32(txtDelay.Text);
            //_requestTimeData.StartRequestTime = txtRequestTime.Text;
            //_requestTimeData.StartTime = txtStart.Text;
            //_requestTimeData.EndTime = txtEnd.Text;
            //_requestTimeData.Log = chkLog.Checked;
            //_requestTimeData.OmsProvider = _requestTimeData.OmsProvider;
            GetRequestTimeData();
            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            JsonConvertor.WriteJsonData(_requestTimeData, JsonFileNames.RequestTimeData, jsonFolderPath);
        }

        private async void btnSendRequest_Click(object sender, EventArgs e)
        {
            //_requestTimeData.BatchSize = Convert.ToInt32(txtBatchSize.Text);
            //_requestTimeData.TotalRequests = Convert.ToInt32(txtTotalRequests.Text);
            //_requestTimeData.Delay = Convert.ToInt32(txtDelay.Text);
            //_requestTimeData.StartRequestTime = txtRequestTime.Text;
            //_requestTimeData.StartTime = txtStart.Text;
            //_requestTimeData.EndTime = txtEnd.Text;
            //_requestTimeData.Log = chkLog.Checked;
            GetRequestTimeData();
            await StartWork.FindStartTime(_requestTimeData);
        }

        private void GetRequestTimeData()
        {
            _requestTimeData.BatchSize = Convert.ToInt32(txtBatchSize.Text);
            _requestTimeData.TotalRequests = Convert.ToInt32(txtTotalRequests.Text);
            _requestTimeData.Delay = Convert.ToInt32(txtDelay.Text);
            _requestTimeData.StartRequestTime = txtRequestTime.Text;
            _requestTimeData.StartTime = txtStart.Text;
            _requestTimeData.EndTime = txtEnd.Text;
            _requestTimeData.Log = chkLog.Checked;
            _requestTimeData.OmsProvider = _requestTimeData.OmsProvider;
        }

        private void cmbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvider.SelectedValue is OmsProvider selectedProvider)
            {
                _requestTimeData.OmsProvider = (int)selectedProvider;
            }
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            var reponses = "";
            var res = await StartWork.Test(_requestTimeData);
            reponses += Environment.NewLine + res;

            MessageBox.Show(reponses);
        }
    }
}
