using System.Collections.Generic;
using System.ComponentModel.Composition;
using UBIK.Injection;
using UBIK.Kernel;
using UBIK.Kernel.Classification;

namespace UBIKUserRights
{
    [Export(typeof(IUbikPlugin))]
    [ExportMetadata("ID", "E3F0B5F5-DC5F-4D83-9FB4-ADFDD9CD400A")]
    [ExportMetadata("Type", typeof(UBIKUserRightsPlugin))]
    [ExportMetadata("Name", "UBIKUserRights")]
    [ExportMetadata("Description", "User rights plugin for UBIK")]
    [ExportMetadata("Version", 3)]
    [ExportMetadata("Company", "Siemens s.r.o.")]
    [ExportMetadata("Copyright", "2023, Siemens s.r.o.")]
    [ExportMetadata("MinimumKernelVersion", "3.6.0.0")]
    public class UBIKUserRightsPlugin : IUbikPlugin
    {
        
    }
}
