using NINA.Plugin;
using NINA.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPluginNamespace {
    /// <summary>
    /// This class exports the IPluginManifest interface and will be used for the general plugin information and options
    /// The base class "PluginBase" will populate all the necessary Manifest Meta Data out of the AssemblyInfo attributes. Please fill these accoringly
    /// 
    /// An instance of this class will be created and set as datacontext on the plugin options tab in N.I.N.A. to be able to configure global plugin settings
    /// The user interface for the settings will be defined by a DataTemplate with the key "<MyPlugin.Name>_Options" - In this template example it is found in the Options.xaml
    /// </summary>
    [Export(typeof(IPluginManifest))]
    public class MyPlugin : PluginBase {

        [ImportingConstructor]
        public MyPlugin() {
        }

        public string DefaultNotificationMessage {
            get {
                return Properties.Settings.Default.DefaultNotificationMessage;
            }
            set {
                Properties.Settings.Default.DefaultNotificationMessage = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
