[![Build status](https://ci.appveyor.com/api/projects/status/99gs8ib4ucth6uj7?svg=true)](https://ci.appveyor.com/project/nvborisenko/logger-net-nlog)

# Installation

Install **ReportPortal.NLog** NuGet package.

[![NuGet version](https://badge.fury.io/nu/reportportal.nlog.svg)](https://badge.fury.io/nu/reportportal.nlog)


> PS> Install-Package ReportPortal.NLog

# Configuration

Add custom extension in NLog configuration.
```xml
<nlog>
  ...
  <extensions>
    <add assembly="ReportPortal.NLog"/>
  </extensions>
  ...
</nlog>
```

Define custom target.
```xml
<nlog>
  ...
  <targets>
    <target name="RP" xsi:type="ReportPortal" />
  </targets>
  ...
</nlog>
```

Specify rule for logging messages to custom target.
```xml
<nlog>
  ...
  <rules>
    <logger name="*" minlevel="Trace" writeTo="RP" />
  </rules>
  ...
</nlog>
```
