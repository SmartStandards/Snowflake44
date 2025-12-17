# Change Log

(automatically maintained using the ['KornSW-VersioningUtil'](https://github.com/KornSW/VersioningUtil))



## Upcoming Changes

*(none)*



## v 3.4.1
released **2025-12-17**, including:
 - No changes to the Library. Only some changes in the demo.



## v 3.4.0
released **2025-09-03**, including:
 - **new Feature**: Added convenience Helper '*ConvertFromGuid*' and '*ConvertToGuid*' (endian aware) and extended Demo.
 - Converted UnitTest-/Demo-Project to .NET 8
 - Fixed not working MS-Test references
 - Fixed UnitTests which were red after DateTimeKind.Utc was explicitely required (*non transparent br.-change in 3.3.2*)



## v 3.3.3
released **2025-05-30**, including:
 - Removed .NET 4.6-Targets and enabled .NET 8.0-Targets (while switching build-runner from Win-2019 to WIN-2022)



## v 3.3.2
released **2025-05-27**, including:
 - Fix: DecodeDateTime will now return a DateTimeKind.Utc and added some argument-guards



## v 3.3.1
released **2025-04-02**, including:
 - Removed Target for .NET 5



## v 3.3.0
released **2025-04-02**, including:
 - **New Feature**: Target for .NET Framework 4.8



## v 3.2.0
released **2025-04-01**, including:
 - **new Feature**: added DateTimeUtility ToInteger10SecondsResolution and FromInteger10SecondsResolution
 - **new Feature**: PascalCasingUtil now public
 - Internal: Added TechDemo to solution. Added some more unit tests and documentation.
 - Internal: Removed implicite usings
 - Prepared .net8.0 target
 - Updated TestFramework to 3.0.4



## v 3.1.0
released **2024-07-24**, including:
 - **new Feature**: added convenience for filtering by day or time range (generating Linq-Expressions)



## v 3.0.2
released **2024-06-10**, including:
 - new revision without significant changes



## v 3.0.1
released **2024-05-09**, including:
 - new revision without significant changes



## v 3.0.0
released **2024-04-26**, including:
 - Breaking Change: Namespace "System.SmartStandards" instead of "System"
 - New Feature: TokenEncoder



## v 2.0.0
(released 2024-01-16)

 - Breaking Change: Renamed from 'UID64' to 'Snowflake44'

## v 1.0.1
(released 2023-05-24)

 - updated readme

## v 1.0.0
(released 2023-05-24)

 - moved from private repo (MVP state already reached)
