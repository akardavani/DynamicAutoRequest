using BusinessService;
using Infrastructure;
using Domain;
using DynamicAutoRequest.BusinessService;
using System.ComponentModel.DataAnnotations;

namespace DynamicAutoRequest
{
    public partial class frmSend : Form
    {
        RequestTimeData _requestTimeData;
        List<ComboBoxItem> _comboBoxItems;
        public frmSend()
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

            // اسم فرم
            var frmSend = _comboBoxItems.FirstOrDefault(e => (int)e.Value == _requestTimeData.OmsProvider).Text;
            Text = frmSend;
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
                BaseSaveData.SaveData(_requestTimeData.OmsProvider, txtRequest.Text);
                MessageBox.Show("حله !!!");
            }
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            var response = await StartWork.Test(_requestTimeData);

            var responseText = await response.Content.ReadAsStringAsync();

            var caption = $@"{response.StatusCode.ToString()} ==> StatusCode : {(int)response.StatusCode}";
            MessageBox.Show(responseText, caption);
        }

        private void SaveRequestTimeData()
        {
            GetRequestTimeData();
            string jsonFolderPath = Path.Combine(Environment.CurrentDirectory, "Json");
            JsonConvertor.WriteJsonData(_requestTimeData, JsonFileNames.RequestTimeData, jsonFolderPath);
        }

        private async void btnSendRequest_Click(object sender, EventArgs e)
        {
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

                // اسم فرم
                var frmSend = _comboBoxItems.FirstOrDefault(e => e.Value == selectedProvider).Text;
                Text = frmSend;
            }
        }

        private void ComboBox_Load()
        {
            _comboBoxItems = Enum.GetValues(typeof(OmsProvider))
                            .Cast<OmsProvider>()
                            .Select(x => new ComboBoxItem { Value = x, Text = GetEnumDisplayName(x) })
                            .ToList();

            cmbProvider.DataSource = _comboBoxItems;
            cmbProvider.DisplayMember = "Text";
            cmbProvider.ValueMember = "Value";
        }

        private class ComboBoxItem
        {
            public OmsProvider Value { get; set; }
            public string Text { get; set; }
        }
    }
}
