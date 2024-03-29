// <copyright file="PexAssemblyInfo.cs" company="Microsoft">Copyright © Microsoft 2016</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("HVTApp.Model")]
[assembly: PexInstrumentAssembly("System.Core")]
[assembly: PexInstrumentAssembly("HVTApp.Model.Services")]
[assembly: PexInstrumentAssembly("HVTApp.Infrastructure")]
[assembly: PexInstrumentAssembly("System.ComponentModel.DataAnnotations")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Core")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "HVTApp.Model.Services")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "HVTApp.Infrastructure")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.ComponentModel.DataAnnotations")]

