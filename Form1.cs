using GMap.NET;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IP_TmaP
{
    public partial class Form1 : Form
    {
        private double latitudefalse = 0.0;
        private double longitudefalse = 0.0;
        public Form1()
        {
            InitializeComponent();
            TextLabel.Text = Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region Настройки для компонента GMap
            gMapControl1.Visible = false;
            gMapControl1.Bearing = 0;
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.GrayScaleMode = true;
            gMapControl1.MaxZoom = 17;
            gMapControl1.MinZoom = 2;
            gMapControl1.NegativeMode = false;
            gMapControl1.PolygonsEnabled = true;
            gMapControl1.ShowTileGridLines = false;
            gMapControl1.Zoom = 5;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            #endregion
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (char line in "1234567890.".ToCharArray())
            {
                if (ipBox.Text.Contains(line))
                {

                    if (!ipBox.Text.Contains('.') && ipBox.Text.Length < 4)
                    {
                        ipBox.IconRight = null;
                    }

                    ipBox.IconRight = null;
                }
                else
                {

                    ipBox.IconRight = Properties.Resources._128px_warning;
                    if (ipBox.Text == string.Empty)
                    {
                        ipBox.IconRight = null;
                    }
                }
            }

        }

        private async void foundBtn_Click(object sender, EventArgs e)
        {
            string line = "";
            using (WebClient wc = new WebClient())
            {
                line = wc.DownloadString($"http://ipwhois.app/xml/{ipBox.Text}");
            }

            gMapControl1.Visible = true;
            Match match = Regex.Match(line, "<latitude>(.*?)</latitude>(.*?)<longitude>(.*?)</longitude>");



            string latitudetrue = match.Groups[1].Value.Replace('.', ',');
            string longitudetrue = match.Groups[3].Value.Replace('.', ',');
            latitudefalse = double.Parse(latitudetrue);
            longitudefalse = double.Parse(longitudetrue);
            TextLabel.Text = latitudetrue + " " + longitudetrue;



            gMapControl1.Position = new PointLatLng(latitudefalse, longitudefalse);
            gMapControl1.Zoom = 17;
        

        }
    }
}
