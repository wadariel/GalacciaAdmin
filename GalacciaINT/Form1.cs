using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace GalacciaINT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTableArgs();
        }


        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Galaccia";

        private void InitializeTableArgs()
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
            UserCredential credential;
            using (var stream =
            new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1t-enGK_DTcLygwNkjDs5mZ6B6b9IonqByeEHl03cCGQ";
            String range = "A2:G";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1t-enGK_DTcLygwNkjDs5mZ6B6b9IonqByeEHl03cCGQ/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = System.Drawing.Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 15, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;


            foreach (var row in values)
            {   
                dataGridView1.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5]);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
        private void button1_Click(object sender, System.EventArgs e)
        {
            dataGridView1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1t-enGK_DTcLygwNkjDs5mZ6B6b9IonqByeEHl03cCGQ/edit#gid=1377732151");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            InitializeTableArgs();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            chart1.Visible = false;
            chart2.Visible = false;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = true;


            this.chart1.Series[0].Points.Clear();

            for (int c = 0; c <= 30; c++)
            {
                int wd = 0;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    string f = dataGridView1[5, i].Value.ToString();
                    if (dataGridView1[5, i].Value.ToString().Substring(2, 3).Contains(","))
                    {
                        if (Convert.ToInt32(f.Substring(2, 2)) == c + 1) wd++;
                    }
                    else
                    {
                        if (Convert.ToInt32(f.Substring(2, 3)) == c + 1) wd++;
                    }
                }
                this.chart1.Series[0].Points.AddXY(c + 1, wd);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            chart2.Visible = true;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = true;

            this.chart2.Series[0].Points.Clear();
           
            for (int c = 0; c <= 6; c++)
            {
                int wd = 0;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    string f = dataGridView1[5, i].Value.ToString();
                    if (Convert.ToInt32(f.Substring(0, 1)) == c + 1) wd++;
                }
                this.chart2.Series[0].Points.AddXY(c + 1, wd);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = false;
            button4.Visible = true;
            chart1.Visible = false;
            chart2.Visible = false;
        }
    }
}

