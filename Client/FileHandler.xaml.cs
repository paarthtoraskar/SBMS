using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Client
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FileHandler : Window
    {
        public FileHandler()
        {
            InitializeComponent();
        }

        private static List<string> ParseArrayOfJsonObjects(string str)
        {
            int end, begin = 0;
            var list = new List<string>();
            do
            {
                begin = str.IndexOf('\"', begin) + 1;
                if (begin == 0)
                    break;
                end = str.IndexOf('\"', begin + 1) - 1;
                string temp = str.Substring(begin, end - begin + 1);
                list.Add(temp);
                begin = end + 2;
            } while (begin < str.Count());
            return list;
        }

        private async void UploadFiles(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var requestMessage = new HttpRequestMessage();
                requestMessage.Method = HttpMethod.Post;
                requestMessage.RequestUri = new Uri("http://localhost:55961/api/files");
                var ofd = new OpenFileDialog { Multiselect = true };
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var content = new MultipartFormDataContent();
                    foreach (string file in ofd.FileNames)
                    {
                        var filestream = new FileStream(file, FileMode.Open, FileAccess.Read);
                        string fileName = Path.GetFileName(file);
                        content.Add(new StreamContent(filestream), "file", fileName);
                    }
                    requestMessage.Content = content;
                    var responseMessage = await client.SendAsync(requestMessage);
                    if (responseMessage.IsSuccessStatusCode)
                        MessageBox.Show("File upload succeeded!");
                    else
                        MessageBox.Show("File upload failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayCurrentFiles(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var requestMessage = new HttpRequestMessage();
                requestMessage.Method = HttpMethod.Get;
                requestMessage.RequestUri = new Uri("http://localhost:55961/api/files");
                HttpResponseMessage responseMessage = client.SendAsync(requestMessage).Result;
                string responseContentAsString = responseMessage.Content.ReadAsStringAsync().Result;
                List<string> listOfFilesOnServer = ParseArrayOfJsonObjects(responseContentAsString);
                serverFiles.Items.Clear();
                foreach (string file in listOfFilesOnServer)
                    serverFiles.Items.Add(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DownloadFiles(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedFileIndex = serverFiles.SelectedIndex;
                if (selectedFileIndex != -1)
                {
                    var fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var client = new HttpClient();
                        var requestMessage = new HttpRequestMessage();
                        requestMessage.Method = HttpMethod.Get;
                        requestMessage.RequestUri = new Uri("http://localhost:55961/api/files/" + selectedFileIndex);
                        var stream = await client.GetStreamAsync(requestMessage.RequestUri);
                        string fileName = serverFiles.Items[selectedFileIndex].ToString();
                        var fileStream = new FileStream(fbd.SelectedPath + '/' + fileName, FileMode.Create, FileAccess.Write);
                        stream.CopyTo(fileStream);
                        stream.Close();
                        fileStream.Close();
                        MessageBox.Show("File download succeeded!");
                    }
                }
                else
                    MessageBox.Show("Select a file to download!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}