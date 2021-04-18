using NINA.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPluginNamespace {
    /// <summary>
    /// This class exports the IPlugin interface and will be used for the general plugin information and options
    /// An instance of this class will be created and set as datacontext on the plugin options tab in N.I.N.A. to be able to configure global plugin settings
    /// The user interface for the settings will be defined in the Options.xaml
    /// </summary>
    [Export(typeof(IPlugin))]
    public class MyPlugin : IPlugin {

        [ImportingConstructor]
        public MyPlugin() {
        }

        public string Name => "MyPlugin";
        public string Description => "This is the description of my plugin. It should contain all relevant description of what the plugin is about.";

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
