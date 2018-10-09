using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;


namespace DynaMaya
{
    /// <summary>
    /// Interaction logic for scrFilePath.xaml
    /// </summary>
    public partial class scrFilePath : UserControl
    {

        public string maya16EnvPath = "";
        public string maya17EnvPath = "";
        public string maya16EnvFilePath = "";
        public string maya17EnvFilePath = "";
        public string dynPath = "";
        public string pkgPath = "";
        public string examplesLoc = "";
        private bool pkgPathExist = false;

        private bool DynDirValid = false;
        private bool maya16EnvExists = false;
        private bool maya16EnvFileExists = false;
        private bool maya17EnvExists = false;
        private bool maya17EnvFileExists = false;
        private bool exampleDirValid = false;
        private bool pkgPathDirValid = false;

        public bool v16install = false;
        public bool v17install = false;

        public string errorMsg = "Please check the following directories, one or more are invalid ";

        private const string dynPkgFldr = @"\Dynamo\Dynamo Core\1.0\packages\";
        private const string dynFldr = @"C:\Program Files\Dynamo\Dynamo Core\1.0\";

        public scrFilePath()
        {
            InitializeComponent();
            setInitialDirectories();
        }


        public void setInitialDirectories()
        {
            //env path
            if (v16install)
            {
                maya16EnvPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                maya16EnvPath += @"\maya\2016\";
                maya16EnvFilePath = maya16EnvPath + "Maya.env";
            }
            else
            {
                tbMaya16EnvLoc.IsEnabled = false;
                maya16EnvFileExists = true;
            }

            if (v17install)
            {
            
                maya17EnvPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                maya17EnvPath += @"\maya\2017\";
                maya17EnvFilePath = maya17EnvPath + "Maya.env";
            }
            else
            {
                tbMaya17EnvLoc.IsEnabled = false;
                maya17EnvFileExists = true;
            }


            var appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pkgPath = appPath + dynPkgFldr;

            dynPath = dynFldr;

            //examples
            examplesLoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //package folder
            pkgPathExist = false;
            if (Directory.Exists(pkgPath)) pkgPathExist = true;
            else
            {
                pkgPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                pkgPath += dynPkgFldr;
                if (Directory.Exists(pkgPath)) pkgPathExist = true;
                else
                {
                    pkgPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                    pkgPath += dynPkgFldr;
                    if (Directory.Exists(pkgPath)) pkgPathExist = true;
                }
            }

            tbDynLoc.Text = dynPath;
            tbPackageMngrLoc.Text = pkgPath;
            tbMaya16EnvLoc.Text = maya16EnvPath;
            tbMaya17EnvLoc.Text = maya17EnvPath;
            tbExampleLoc.Text = examplesLoc;
        }

        public bool checkDirs()
        {
            errorMsg = "Please check the following directories, one or more are invalid ";

            DynDirValid = Directory.Exists(dynPath);

            if (v16install)
            {
                maya16EnvExists = Directory.Exists(maya16EnvPath);
                maya16EnvFileExists = File.Exists(maya16EnvFilePath);
            }
            else
            {
                tbMaya16EnvLoc.IsEnabled = false;
                maya16EnvFileExists = true;
            }

            if (v17install)
            {
                maya17EnvExists = Directory.Exists(maya17EnvPath);
                maya17EnvFileExists = File.Exists(maya17EnvFilePath);
            }
            else
            {
                tbMaya17EnvLoc.IsEnabled = false;
                maya17EnvFileExists = true;
            }


            exampleDirValid = Directory.Exists(examplesLoc);
            pkgPathDirValid = Directory.Exists(pkgPath);
            bool DirAllOk = true;

            //
            if (v16install && !maya16EnvFileExists)
            {
                errorMsg += " | Maya 2016 env file invalid";
                DirAllOk = false;
            }
            if (v17install && !maya17EnvFileExists)
            {
                errorMsg += " | Maya 2017 env file invalid";
                DirAllOk = false;
            }
            if (!DynDirValid)
            {
                errorMsg += " | Dynamo Directory invalid";
                DirAllOk = false;
            }
            if (!exampleDirValid)
            {
                errorMsg += " | Example Directory invalid";
                DirAllOk = false;
            }
            if (!pkgPathDirValid)
            {
                errorMsg += " | Dynamo Package Directory invalid";
                DirAllOk = false;
            }

            return DirAllOk; 
        }

        private void btChFldrDynDir_Click(object sender, RoutedEventArgs e)
        {
            var dirFider = new System.Windows.Forms.FolderBrowserDialog();
            dirFider.Description = "Select the directory where Dynamo is installed";
            dirFider.ShowDialog();

            dynPath = dirFider.SelectedPath;
            tbDynLoc.Text = dynPath;
        }
        private void btChFldrPkgDir_Click(object sender, RoutedEventArgs e)
        {
            var dirFider = new System.Windows.Forms.FolderBrowserDialog();
            dirFider.Description = "Select the directory where Dynamo packages are installed";
            dirFider.ShowDialog();

            pkgPath = dirFider.SelectedPath;
            tbPackageMngrLoc.Text = pkgPath;
        }
     
        private void btChFldrMaya16Env_Click(object sender, RoutedEventArgs e)
        {
            var dirFider = new System.Windows.Forms.FolderBrowserDialog();
            dirFider.Description = "Select the directory containing the Maya.env file";
            dirFider.ShowDialog();

            maya16EnvPath = dirFider.SelectedPath;
            maya16EnvFilePath = maya16EnvPath + "Maya.env";
            tbMaya16EnvLoc.Text = maya16EnvPath;
        }
        private void tbMaya16EnvLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            maya16EnvPath = tbMaya16EnvLoc.Text;
        }

        private void tbMaya17EnvLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            maya17EnvPath = tbMaya17EnvLoc.Text;
        }
        private void btChFldrMaya17Env_Click(object sender, RoutedEventArgs e)
        {
            var dirFider = new System.Windows.Forms.FolderBrowserDialog();
            dirFider.Description = "Select the directory containing the Maya.env file";
            dirFider.ShowDialog();

            maya17EnvPath = dirFider.SelectedPath;
            maya16EnvFilePath = maya17EnvPath + "Maya.env";
            tbMaya17EnvLoc.Text = maya17EnvPath;
        }
        private void btChFldrExamples_Click(object sender, RoutedEventArgs e)
        {
            var dirFider = new System.Windows.Forms.FolderBrowserDialog();
            dirFider.Description = "Select the directory where you would like the example files installed";
            dirFider.ShowDialog();

            examplesLoc = dirFider.SelectedPath;
            tbExampleLoc.Text = examplesLoc;
        }

        private void tbPackageMngrLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            pkgPath = tbPackageMngrLoc.Text;
        }

        private void tbDynLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            dynPath = tbDynLoc.Text;
        }
        private void tbExampleLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            examplesLoc = tbExampleLoc.Text ;
        }

       
    }
}
