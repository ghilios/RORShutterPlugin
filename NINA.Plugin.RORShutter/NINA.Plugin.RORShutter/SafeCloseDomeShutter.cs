using Newtonsoft.Json;
using NINA.Core.Model;
using NINA.Sequencer.Validations;
using NINA.Equipment.Interfaces.Mediator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using NINA.Core.Locale;
using NINA.Sequencer.SequenceItem;
using NINA.Core.Utility;

namespace NINA.Plugin.RORShutter {

    [ExportMetadata("Name", "Safe Close ROR Shutter")]
    [ExportMetadata("Description", "Closes a roll off roof only if the telescope is parked")]
    [ExportMetadata("Icon", "ObservatorySVG")]
    [ExportMetadata("Category", "Lbl_SequenceCategory_Dome")]
    [Export(typeof(ISequenceItem))]
    [JsonObject(MemberSerialization.OptIn)]
    public class SafeCloseDomeShutter : SequenceItem, IValidatable {

        [ImportingConstructor]
        public SafeCloseDomeShutter(IDomeMediator domeMediator, ITelescopeMediator telescopeMediator) {
            this.domeMediator = domeMediator;
            this.telescopeMediator = telescopeMediator;
        }

        private SafeCloseDomeShutter(SafeCloseDomeShutter cloneMe) : this(cloneMe.domeMediator, cloneMe.telescopeMediator) {
            CopyMetaData(cloneMe);
        }

        public override object Clone() {
            return new SafeCloseDomeShutter(this);
        }

        private readonly IDomeMediator domeMediator;
        private readonly ITelescopeMediator telescopeMediator;
        private IList<string> issues = new List<string>();

        public IList<string> Issues {
            get => issues;
            set {
                issues = value;
                RaisePropertyChanged();
            }
        }

        public override async Task Execute(IProgress<ApplicationStatus> progress, CancellationToken token) {
            var telescopeInfo = telescopeMediator.GetInfo();
            var domeInfo = domeMediator.GetInfo();
            if (domeInfo.ShutterStatus == Equipment.Interfaces.ShutterState.ShutterClosed || domeInfo.ShutterStatus == Equipment.Interfaces.ShutterState.ShutterClosing) {
                Logger.Info($"SafeCloseDomeShutter: Doing nothing since dome shutter is closed or closing");
                return;
            }

            if (!telescopeInfo.Connected) {
                Logger.Error("SafeCloseDomeShutter: Telescope is not connected. Will not close dome shutter");
                throw new InvalidOperationException("Telescope is not connected. Will not close dome shutter");
            }
            if (!telescopeInfo.AtPark) {
                Logger.Error("SafeCloseDomeShutter: Telescope is not parked. Will not close dome shutter");
                throw new InvalidOperationException("Telescope is not parked. Will not close dome shutter");
            }
            await domeMediator.CloseShutter(token);
        }

        public bool Validate() {
            var i = new List<string>();
            if (!domeMediator.GetInfo().Connected) {
                i.Add(Loc.Instance["LblDomeNotConnected"]);
            }
            if (!domeMediator.GetInfo().CanSetShutter) {
                i.Add("No dome shutter");
            }
            if (!telescopeMediator.GetInfo().Connected) {
                i.Add(Loc.Instance["LblTelescopeNotConnected"]);
            }
            Issues = i;
            return i.Count == 0;
        }

        public override void AfterParentChanged() {
            Validate();
        }

        public override string ToString() {
            return $"Category: {Category}, Item: {nameof(SafeCloseDomeShutter)}";
        }
    }
}