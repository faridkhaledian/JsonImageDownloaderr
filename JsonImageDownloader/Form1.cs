using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonImageDownloader
{
    public partial class Form1 : Form
    {
        private readonly string currentVersion = "1.0.0";

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var versions = LoadVersions();

            var newerVersions = versions
                .Where(v => !string.IsNullOrEmpty(v?.Version)
                            && new Version(v.Version) > new Version(currentVersion))
                .ToList();

            if (newerVersions.Count == 0)
            {
                MessageBox.Show("نسخه جدیدتری وجود ندارد.");
                return;
            }

            string message = "نسخه‌های جدیدتر پیدا شد:\n\n" +
                             string.Join("\n", newerVersions.Select(v => v.Version));

            MessageBox.Show(message);

            // دانلود نسخه‌های جدید
            await DownloadNewVersions(newerVersions);

            MessageBox.Show("دانلود تمام شد!");
            StopAppPool("JsonImageDownloaderAppPool");

        }
       
        private List<VersionInfo> LoadVersions()
        {
            string jsonPath = Path.Combine(Application.StartupPath, "versions.json");

            if (!File.Exists(jsonPath))
            {
                MessageBox.Show("فایل versions.json پیدا نشد!");
                return new List<VersionInfo>();
            }

            string jsonContent = File.ReadAllText(jsonPath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var versions = JsonSerializer.Deserialize<List<VersionInfo>>(jsonContent, options);
            foreach (var v in versions)
            {
                Console.WriteLine($"Version: '{v?.Version}', File: '{v?.File}'");
            }

            return versions ?? new List<VersionInfo>();
        }

        private async Task DownloadNewVersions(List<VersionInfo> newerVersions)
        {
            string baseFolder = Path.Combine(Application.StartupPath, "DownloadedVersions");

            if (!Directory.Exists(baseFolder))
                Directory.CreateDirectory(baseFolder);

            List<string> downloadedFiles = new List<string>();

            using (HttpClient client = new HttpClient())
            {
                foreach (var version in newerVersions)
                {
                    try
                    {
                        string versionFolder = Path.Combine(baseFolder, version.Version);
                        if (!Directory.Exists(versionFolder))
                            Directory.CreateDirectory(versionFolder);

                        string fileName = Path.GetFileName(new Uri(version.File).AbsolutePath);
                        string filePath = Path.Combine(versionFolder, fileName);

                        var data = await client.GetByteArrayAsync(version.File);
                        File.WriteAllBytes(filePath, data);

                        downloadedFiles.Add(filePath);
                        Console.WriteLine($"نسخه {version.Version} دانلود شد و در {filePath} قرار گرفت.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"خطا در دانلود نسخه {version.Version}: {ex.Message}");
                    }
                }
            }
            if (downloadedFiles.Count > 0)
            {
                string message = "فایل‌های دانلود شده در مسیرهای زیر ذخیره شدند:\n\n" +
                                 string.Join("\n", downloadedFiles);
                MessageBox.Show(message, "دانلود کامل شد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartAppPool("JsonImageDownloaderAppPool");
        }

        private void StartAppPool(string appPoolName)
        {
            try
            {
                using (Microsoft.Web.Administration.ServerManager serverManager = new Microsoft.Web.Administration.ServerManager())
                {
                    var appPool = serverManager.ApplicationPools[appPoolName];
                    if (appPool != null)
                    {
                        // Only start if not already running
                        if (appPool.State != Microsoft.Web.Administration.ObjectState.Started)
                        {
                            appPool.Start();
                            MessageBox.Show($"AppPool '{appPoolName}' started successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"AppPool '{appPoolName}' is already running.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"AppPool '{appPoolName}' not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting AppPool:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStopAppPool_Click(object sender, EventArgs e)
        {
            StopAppPool("JsonImageDownloaderAppPool");
        }

        private void StopAppPool(string appPoolName)
        {
            try
            {
                using (Microsoft.Web.Administration.ServerManager serverManager = new Microsoft.Web.Administration.ServerManager())
                {
                    Microsoft.Web.Administration.ApplicationPool appPool = serverManager.ApplicationPools[appPoolName];
                    if (appPool != null)
                    {
                        appPool.Stop();
                        MessageBox.Show($"AppPool '{appPoolName}' stopped successfully.");
                    }
                    else
                    {
                        MessageBox.Show($"AppPool '{appPoolName}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping AppPool:\n{ex.Message}");
            }
        }
    }
}
