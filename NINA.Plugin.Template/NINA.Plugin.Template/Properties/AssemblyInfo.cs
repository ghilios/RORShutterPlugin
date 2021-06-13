using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// The name of your plugin
[assembly: AssemblyTitle("MyPlugin")]
// A short description of your plugin
[assembly: AssemblyDescription("This is the description of my plugin. It should contain all relevant description of what the plugin is about.")]
[assembly: AssemblyConfiguration("")]
//Your name
[assembly: AssemblyCompany("MyCompanyOrName")]
//The product name that this plugin is part of
[assembly: AssemblyProduct("NINA.Plugin.Template")]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is used as a unique identifier of the plugin
[assembly: Guid("141704e0-505c-492c-9e74-3b85f1c05d12")]

//The assembly versioning
//Should be incremented for each new release build of a plugin
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]

//The minimum Version of N.I.N.A. that this plugin is compatible with
[assembly: AssemblyMetadata("MinimumApplicationVersion", "1.11.0.1092")]

//Your plugin homepage - omit if not applicaple
[assembly: AssemblyMetadata("Homepage", "https://example.com/")]
//The license your plugin code is using
[assembly: AssemblyMetadata("License", "MPL-2.0")]
//The url to the license
[assembly: AssemblyMetadata("LicenseURL", "https://www.mozilla.org/en-US/MPL/2.0/")]
//The repository where your pluggin is hosted
[assembly: AssemblyMetadata("Repository", "https://bitbucket.org/Isbeorn/nina.plugin.template/")]

//Common tags that quickly describe your plugin
[assembly: AssemblyMetadata("Tags", "Template,Sequencer")]

//The featured logo that will be displayed in the plugin list next to the name
[assembly: AssemblyMetadata("FeaturedImageURL", "https://nighttime-imaging.eu/wp-content/uploads/2019/02/Logo_Nina.png")]
//An example screenshot of your plugin in action
[assembly: AssemblyMetadata("ScreenshotURL", "https://bitbucket.org/Isbeorn/nina.plugins/downloads/Starlock2.png")]
//An additional example screenshot of your plugin in action
[assembly: AssemblyMetadata("AltScreenshotURL", "https://bitbucket.org/Isbeorn/nina.plugins/downloads/Instruction.png")]
[assembly: AssemblyMetadata("LongDescription", @"An in-depth description of your plugin")]