using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.IO.Compression;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string filepath;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            filepath = openFileDialog.FileName;
            Pathtxtbox.Text = filepath;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            byte[] filebytes = File.ReadAllBytes(filepath);
            try
            {
                using (FileStream filestream = new FileStream(filepath + ".gz", FileMode.Create))
                {
                    
                    using (GZipStream gZipStream = new GZipStream(filestream, CompressionMode.Compress))
                    {
                        gZipStream.Write(filebytes, 0, filebytes.Length);
                        
                    }
                }
                MessageBox.Show("Compressing Done");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            filepath = Path.GetFileName( filepath.Substring(0, filepath.Length - 3));
            
            try
            {
                using (FileStream filestreamOutput = new FileStream(filepath, FileMode.Create))
                {
                    using (FileStream filestream = new FileStream(Pathtxtbox.Text, FileMode.Open))
                    {
                        using (GZipStream gZipStream = new GZipStream(filestream, CompressionMode.Decompress))
                        {
                            byte[] bytes = new byte[1000];
                            while (gZipStream.Read(bytes, 0, 1000) != 0)
                            {
                                filestreamOutput.Write(bytes, 0, 1000);
                            }
                        }
                    }
                    MessageBox.Show("Decompression Done");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void ArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archiv files (*.gz)|*.gz";
            openFileDialog.ShowDialog();
            filepath = openFileDialog.FileName;
            Pathtxtbox.Text = filepath;
        }
    }
}
