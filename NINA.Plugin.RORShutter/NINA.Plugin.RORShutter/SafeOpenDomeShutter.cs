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

    [ExportMetadata("Name", "Safe Open ROR Shutter")]
    [ExportMetadata("Description", "Opens a roll off roof only if the telescope is parked")]
    [ExportMetadata("Icon", "ObservatorySVG")]
    [ExportMetadata("Category", "Lbl_SequenceCategory_Dome")]
    [Export(typeof(ISequenceItem))]
    [JsonObject(MemberSerialization.OptIn)]
    public class SafeOpenDomeShutter : SequenceItem, IValidatable {

        [ImportingConstructor]
        public SafeOpenDomeShutter(IDomeMediator domeMediator, ITelescopeMediator telescopeMediator) {
            this.domeMediator = domeMediator;
            this.telescopeMediator = telescopeMediator;
        }

        private SafeOpenDomeShutter(SafeOpenDomeShutter cloneMe) : this(cloneMe.domeMediator, cloneMe.telescopeMediator) {
            CopyMetaData(cloneMe);
        }

        public override object Clone() {
            return new SafeOpenDomeShutter(this);
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
            if (domeInfo.ShutterStatus == Equipment.Interfaces.ShutterState.ShutterOpen || domeInfo.ShutterStatus == Equipment.Interfaces.ShutterState.ShutterOpening) {
                Logger.Info($"SafeOpenDomeShutter: Doing nothing since dome shutter is open or opening");
                return;
            }

            if (!telescopeInfo.Connected) {
                Logger.Error("SafeOpenDomeShutter: Telescope is not connected. Will not open dome shutter");
                throw new InvalidOperationException("Telescope is not connected. Will not open dome shutter");
            }
            if (!telescopeInfo.AtPark) {
                Logger.Error("SafeOpenDomeShutter: Telescope is not parked. Will not open dome shutter");
                throw new InvalidOperationException("Telescope is not parked. Will not open dome shutter");
            }
            await domeMediator.OpenShutter(token);
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
            return $"Category: {Category}, Item: {nameof(SafeOpenDomeShutter)}";
        }
    }
}