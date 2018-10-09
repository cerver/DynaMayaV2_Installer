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
using System.Security;
using System.Security.AccessControl;

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

        private bool installSucess = true;

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
            PathScreen.v18install = p0.v18checked;
            PathScreen.tbMaya19EnvLoc.IsEnabled = p0.v19checked;
            PathScreen.v19install = p0.v19checked;
            PathScreen.tbMaya18EnvLoc.IsEnabled = p0.v18checked;

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

            installSucess = true;
            btNext.IsEnabled = false;

            var envVar = "Path";
            var pathToAdd = ";" + PathScreen.dynPath;

            var envExists = Environment.GetEnvironmentVariable(envVar);
            var newPath = envExists + pathToAdd;

            if (!envExists.Contains(PathScreen.dynPath))
                try
                {
                    Environment.SetEnvironmentVariable(envVar, newPath, EnvironmentVariableTarget.Machine);
                }
                catch (Exception)
                {
                    installSucess = false;
                    return installSucess;
              
                }

            try
            {
                if (PathScreen.v19install)
                {
                    writeEnvInfo(PathScreen.maya19EnvFilePath);
                    unZipFile("PluginPackInstaller.Content.DynaMayaEnv.zip", PathScreen.maya19EnvPath);

                }
                if (PathScreen.v18install)
                {
                    writeEnvInfo(PathScreen.maya18EnvFilePath);
                    unZipFile("PluginPackInstaller.Content.DynaMayaEnv.zip", PathScreen.maya18EnvPath);
                }


                unZipFile("PluginPackInstaller.Content.DynaMayaPlugin.zip", PathScreen.dynPath);
                unZipFile("PluginPackInstaller.Content.DynaMayaPackage.zip", PathScreen.pkgPath);
                unZipFile("PluginPackInstaller.Content.DynaMayaExamples.zip", PathScreen.examplesLoc);
            }
            catch (Exception)
            {

                installSucess = false;
                return installSucess;
            }

            installSucess = true;
            return installSucess;
         

        }

        private void unZipFile(string file, string location)
        {
           //load zip into file stream
            Stream sFile =  Assembly.GetExecutingAssembly().GetManifestResourceStream(file);

            try
            {
                using (ZipArchive zipfile = new ZipArchive(sFile))
                {
                    //extract it
                    zipfile.ExtractToDirectory(location, true);
                }
            }
            catch (Exception e)
            {
                installSucess = false;
                MessageBox.Show(
                    "there was an error extracting some files, please check you have permission to write into the following directory. " +
                    location);
                
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
                installSucess = false;
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
            System.Diagnostics.Process.Start("http://www.cerver.io");
        }

        private void cervernet_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.cerver.design");
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


            string fpath;

            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
                if (file.Name == "")
                {
                    fpath = Path.GetDirectoryName(completeFileName);
                    Directory.CreateDirectory(fpath);
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }
}
