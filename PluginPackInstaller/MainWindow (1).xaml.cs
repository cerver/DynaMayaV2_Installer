using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Linq;
using System.Windows.Resources;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.IO.Compression ;

using System.Security.Permissions;
//using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Security;

namespace PluginPackInstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentPage = 1;

        private bool DynDirValid = false;
        private bool mayaEnvExists = false;
        private bool mayaEnvFileExists = false;
        private bool exampleDirValid = false;
        private bool pkgPathExist = false;

        public MainWindow()
        {
            InitializeComponent();
     		p0.Visibility = System.Windows.Visibility.Visible;
            p1.Visibility = System.Windows.Visibility.Hidden;
            PathScreen.Visibility = System.Windows.Visibility.Hidden;
            btExit.Visibility = System.Windows.Visibility.Hidden;
            btPrev.Visibility = System.Windows.Visibility.Hidden;

            var assembly = Assembly.GetExecutingAssembly();
            Stream file = assembly.GetManifestResourceStream("MyProject.Resources.configuration.ini"); // my resource


        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btPrev_Click(object sender, RoutedEventArgs e)
        {
            currentPage--;
            switch (currentPage)
            {
                case 0:

                case 1:
                    p0.Visibility = System.Windows.Visibility.Visible;
                    p1.Visibility = System.Windows.Visibility.Hidden;
                    PathScreen.Visibility = System.Windows.Visibility.Hidden;
                    btNext.IsEnabled = true;
                    btPrev.Visibility = System.Windows.Visibility.Hidden;
                    btNext.Content = "Next >>";
                    break;
                case 2:
                    p0.Visibility = System.Windows.Visibility.Hidden;
                    p1.Visibility = System.Windows.Visibility.Visible;
                    PathScreen.Visibility = System.Windows.Visibility.Hidden;
                    btNext.Content = "Next >>";
                    break;


            }
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            PathScreen.v17install = p0.v17checked;
            PathScreen.tbMaya16EnvLoc.IsEnabled = p0.v16checked;
            PathScreen.v16install = p0.v16checked;
            PathScreen.tbMaya17EnvLoc.IsEnabled = p0.v17checked;

            switch (currentPage)
            {
                case 0:
                    currentPage++;
                    break;

                case 1:
                    p0.Visibility = System.Windows.Visibility.Hidden;
                    p1.Visibility = System.Windows.Visibility.Visible;
                    btPrev.Visibility = System.Windows.Visibility.Visible;
                    btNext.IsEnabled = true;
                    if (p1.hasAccepted)
                    {
                        currentPage++;
                        p1.Visibility = System.Windows.Visibility.Hidden;
                        PathScreen.Visibility = System.Windows.Visibility.Visible;
                        btNext.Content = "CHECK";
                        PathScreen.setInitialDirectories();
                    }
                    break;
                case 2:
                    
                    if (PathScreen.checkDirs())
                    {
                        currentPage++;
                        btNext.Content = "INSTALL";
                    }
                    else
                    {

                        MessageBox.Show(PathScreen.errorMsg);
                    }
                    break;
                case 3:
                    if (doInstall())
                    {
                        btExit.Visibility = Visibility.Visible;
                        btNext.Visibility = Visibility.Hidden;
                        scrInstalled.Visibility = Visibility.Visible;
                    }
                    break;

            }
        }


        private bool doInstall()
        {

   
            btNext.IsEnabled = false;

            var envExists = Environment.GetEnvironmentVariable("PATH");
            if (!envExists.Contains(PathScreen.dynPath))
                Environment.SetEnvironmentVariable("Path", PathScreen.dynPath);

            if (PathScreen.v16install)
            {
                writeEnvInfo(PathScreen.maya16EnvFilePath);
                unZipFile("PluginPackInstaller.Content.DynaMayaEnv.zip", PathScreen.maya16EnvPath);

            }
            if (PathScreen.v17install)
            {
                writeEnvInfo(PathScreen.maya17EnvFilePath);
                unZipFile("PluginPackInstaller.Content.DynaMayaEnv.zip", PathScreen.maya17EnvPath);
            }


            unZipFile("PluginPackInstaller.Content.DynaMayaPlugin.zip", PathScreen.dynPath);
            unZipFile("PluginPackInstaller.Content.DynaMayaPackage.zip", PathScreen.pkgPath);
            unZipFile("PluginPackInstaller.Content.DynaMayaExamples.zip", PathScreen.examplesLoc);

            return true;
         

        }

        private void unZipFile(string file, string location)
        {
           
            Stream sFile =  Assembly.GetExecutingAssembly().GetManifestResourceStream("PluginPackInstaller.Content.DynaMayaExamples.zip");

            using (ZipArchive zipfile = new ZipArchive(sFile))
            {
               
                zipfile.ExtractToDirectory(location, true);
            }
        }


        private void writeEnvInfo(string mayaEnvFile)
        {
            string envpathvar = "MAYA_PLUG_IN_PATH = " + PathScreen.dynPath + "; //DynaMaya";
            FileIOPermission permissionAccess = new FileIOPermission(FileIOPermissionAccess.Write, mayaEnvFile);
            try
            {
                permissionAccess.Demand();
            }
            catch (SecurityException s)
            {
                MessageBox.Show(s.Message);
            }

            string existingPath = "//DynaMaya";


            List<string> envFileRead = new List<string>();
            envFileRead.AddRange(File.ReadAllLines(mayaEnvFile)); 

            bool needsNewPath = true;

            for (int i = 0; i < envFileRead.Count; i++)
            {
                if (envFileRead[i].Contains(existingPath))
                {
                    envFileRead[i] = "";

                }

            }


            if (needsNewPath)
            {
                envFileRead.Add(envpathvar);
                
                File.WriteAllLines(mayaEnvFile, envFileRead);
               
            }
        }


        private void cerverorg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cerver.org");
        }

        private void cervernet_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cerver.net");
        }

        
    }

    public static class ZipArchiveExtensions
    {
        public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }
}
