using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
            List<string> list = new List<string>();
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

        private void UploadFiles(object sender, RoutedEventArgs e)
        {
            try
            {
                var message = new HttpRequestMessage();
                message.Method = HttpMethod.Post;
                message.RequestUri = new Uri("http://localhost:55961/api/files");

                var content = new MultipartFormDataContent();

                //string sendFolder = "../../SendFiles/";
                //string[] files = Directory.GetFiles(sendFolder);

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var file in ofd.FileNames)
                    {
                        var filestream = new FileStream(file, FileMode.Open);
                        var fileName = System.IO.Path.GetFileName(file);
                        content.Add(new StreamContent(filestream), "file", fileName);
                    }

                    message.Content = content;

                    var client = new HttpClient();
                    client.SendAsync(message).ContinueWith(task =>
                    {
                        if (task.Result.IsSuccessStatusCode)
                        {
                            System.Windows.MessageBox.Show("Files uploaded successfully!");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void DisplayUpload(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new HttpClient();

                var message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri("http://localhost:55961/api/files");

                HttpResponseMessage response = client.SendAsync(message).Result;
                HttpContent responseContent = response.Content;
                string responseContentAsString = responseContent.ReadAsStringAsync().Result;

                List<string> listOfFilesOnServer = ParseArrayOfJsonObjects(responseContentAsString);

                serverFiles.Items.Clear();
                foreach (string file in listOfFilesOnServer)
                    this.serverFiles.Items.Add(file);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void DownloadFiles(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new HttpClient();

                int selectedFileIndex = this.serverFiles.SelectedIndex;

                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                if (selectedFileIndex != -1)
                {
                    var message = new HttpRequestMessage();
                    message.Method = HttpMethod.Get;
                    message.RequestUri = new Uri("http://localhost:55961/api/files/" + selectedFileIndex.ToString());

                    Stream stream = client.GetStreamAsync(message.RequestUri).Result;
                    string fileName = serverFiles.Items[selectedFileIndex].ToString();

                    //FileStream fileStream = new FileStream("../../RecievedFiles/" + fileName, FileMode.Create, FileAccess.ReadWrite);
                    FileStream fileStream = new FileStream(fbd.SelectedPath + '/' + fileName, FileMode.Create, FileAccess.ReadWrite);
                    stream.CopyTo(fileStream);
                    fileStream.Close();
                    System.Windows.MessageBox.Show("File downloaded successfully!");
                }
                else
                    System.Windows.MessageBox.Show("Select a file to download!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}