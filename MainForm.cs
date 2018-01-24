using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoOS.Platform;
using VideoOS.Platform.ConfigurationItems;
using VideoOS.Platform.UI;

namespace TestConfigProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public Item SelectedItem { get; private set; }

        private void SelectCamera_Click(object sender, EventArgs e)
        {
            using (ItemPickerForm form = new ItemPickerForm())
            {

                form.KindFilter = Kind.Camera;
                form.AutoAccept = true;
                form.Init();
                if(form.ShowDialog()== DialogResult.OK)
                {
                    SelectedItem =  form.SelectedItem;
                    txtName.Text = SelectedItem.Name;
                }
            }
        }

        private void ReadConfig_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
                return;

            StringBuilder configText = new StringBuilder();

            var camera = new Camera(SelectedItem.FQID);

            var ddsettingFolder = camera.DeviceDriverSettingsFolder;
            configText.AppendFormat("DeviceDriverSettingsFolder DName = {0}\r\n", ddsettingFolder.DisplayName);
            foreach(var ddsetting in ddsettingFolder.DeviceDriverSettings)
            {
                configText.AppendFormat("==========================================\r\n");
                configText.AppendFormat("DeviceDriverSettings DName = {0}\r\n", ddsetting.DisplayName);
                foreach(var childStream in ddsetting.StreamChildItems)
                {
                    configText.AppendFormat("-------------------------------------------\r\n");
                    configText.AppendFormat("StreamChildItem DName = {0}\r\n", childStream.DisplayName);
                    foreach(var prop in childStream.Properties.Keys)
                    {
                        configText.AppendFormat("Prop {0} = {1}\r\n", prop, childStream.GetProperty(prop));
                    }
                }
            }

            textBox1.Text = configText.ToString();


        }
    }
}
