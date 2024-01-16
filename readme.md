# Snowflake44 (UID64)

A 44-Bit Timestamp with some random salt that is sortable (SQL index 
compat.), decentralized generatable and fitting into a BigInt



**Information about the state of work can be found at the [Change-Log](./vers/changelog.md)**

[![Build status](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_apis/build/status/Snowflake44)](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_build/latest?definitionId=3) | **[NPM-Package](https://www.npmjs.com/package/snowflake44?activeTab=versions)** | **[NuGet-Package](https://www.nuget.org/packages/snowflake44)**

## Motivation:

TBD...




## Algorithm

      // 11
      // 98                 0
      // 1TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTRRRRRRRRRRRRRRRRRRR
      // [                 44                       ][        19       ]
