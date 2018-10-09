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

        public string maya19EnvPath = "";
        public string maya18EnvPath = "";
        public string maya19EnvFilePath = "";
        public string maya18EnvFilePath = "";
        public string dynPath = "";
        public string pkgPath = "";
        public string examplesLoc = "";
        private bool pkgPathExist = false;

        private bool DynDirValid = false;
        private bool maya19EnvExists = false;
        private bool maya19EnvFileExists = false;
        private bool maya18EnvExists = false;
        private bool maya18EnvFileExists = false;
        private bool exampleDirValid = false;
        private bool pkgPathDirValid = false;

        public bool v19install = false;
        public bool v18install = false;
        public bool pkgDirExist = false;

        public string errorMsg = "Please check the following directories, one or more are invalid ";

        private const string dynFldr = @"C:\Program Files\Dynamo\Dynamo Core\1.3\";

        public scrFilePath()
        {
            InitializeComponent();
            setInitialDirectories();
        }


        public void setInitialDirectories()
        {
            //env path
            if (v19install)
            {
                maya19EnvPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                maya19EnvPath += @"\maya\2019\";
                maya19EnvFilePath = maya19EnvPath + "Maya.env";
            }
            else
            {
                tbMaya19EnvLoc.IsEnabled = false;
                maya19EnvFileExists = true;
            }

            if (v18install)
            {
            
                maya18EnvPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                maya18EnvPath += @"\maya\2018\";
                maya18EnvFilePath = maya18EnvPath + "Maya.env";
            }
            else
            {
                tbMaya18EnvLoc.IsEnabled = false;
                maya18EnvFileExists = true;
            }


            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pkgPath = appDataPath + @"\Dynamo";

            string fullPkgPath = Path.Combine(pkgPath, @"\Dynamo Core\1.3\packages");
            pkgPathExist = Directory.Exists(fullPkgPath);

            dynPath = dynFldr;

            //examples
            examplesLoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //package folder
            pkgPathExist = Directory.Exists(pkgPath);
            if (!pkgPathExist) pkgPath = @"<--Specify the path to your Dynamo Core Package Folder--->";

 

            tbDynLoc.Text = dynPath;
            tbPackageMngrLoc.Text = pkgPath;
            tbMaya19EnvLoc.Text = maya19EnvPath;
            tbMaya18EnvLoc.Text = maya18EnvPath;
            tbExampleLoc.Text = examplesLoc;
        }

        public bool checkDirs()
        {
            errorMsg = "Please check the following directories, one or more are invalid ";

            DynDirValid = Directory.Exists(dynPath);

            if (v19install)
            {
                maya19EnvExists = Directory.Exists(maya19EnvPath);
                maya19EnvFileExists = File.Exists(maya19EnvFilePath);
            }
            else
            {
                tbMaya19EnvLoc.IsEnabled = false;
                maya19EnvFileExists = true;
            }

            if (v18install)
            {
                maya18EnvExists = Directory.Exists(maya18EnvPath);
                maya18EnvFileExists = File.Exists(maya18EnvFilePath);
            }
            else
            {
                tbMaya18EnvLoc.IsEnabled = false;
                maya18EnvFileExists = true;
            }


            exampleDirValid = Directory.Exists(examplesLoc);
            pkgPathDirValid = Directory.Exists(pkgPath);
            bool DirAllOk = true;

            //
            if (v19install && !maya19EnvFileExists)
            {
                errorMsg += " | Maya 2019 env file invalid";
                DirAllOk = false;
            }
            if (v18install && !maya18EnvFileExists)
            {
                errorMsg += " | Maya 2018 env file invalid";
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
     
        private void btChFldrMaya19Env_Click(object sender, RoutedEventArgs e)
        {
            var dirFider = new System.Windows.Forms.FolderBrowserDialog();
            dirFider.Description = "Select the directory containing the Maya.env file";
            dirFider.ShowDialog();

            maya19EnvPath = dirFider.SelectedPath;
            maya19EnvFilePath = maya19EnvPath + "Maya.env";
            tbMaya19EnvLoc.Text = maya19EnvPath;
        }
        private void tbMaya19EnvLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            maya19EnvPath = tbMaya19EnvLoc.Text;
        }

        private void tbMaya18EnvLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            maya18EnvPath = tbMaya18EnvLoc.Text;
        }
        private void btChFldrMaya18Env_Click(object sender, RoutedEventArgs e)
        {
            var dirFider = new System.Windows.Forms.FolderBrowserDialog();
            dirFider.Description = "Select the directory containing the Maya.env file";
            dirFider.ShowDialog();

            maya18EnvPath = dirFider.SelectedPath;
            maya19EnvFilePath = maya18EnvPath + "Maya.env";
            tbMaya18EnvLoc.Text = maya18EnvPath;
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
