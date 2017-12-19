using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace GetImageService
{
    [RunInstaller(true)]
    public partial class WinServiceInstaller : System.Configuration.Install.Installer
    {
        public WinServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
