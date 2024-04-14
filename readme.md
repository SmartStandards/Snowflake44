# Snowflake44 (UID64)

Snowflake44 is an algorithm to create a 64 bit integer ("Long", "BigInt") unique ID that can be used as SQL primary key, 
but is more compact than a GUID. It's actually a 44 bit UTC time stamp combined with 19 bit of random.
The topmost bit is not used in order to avoid negative values.

- Epoch is 1900..2456 (557 years)
- Precision is 1 millisecond
- Risk of collision: 1:500.000

See [Change Log](./vers/changelog.md) for current version information.

[![Build status](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_apis/build/status/Snowflake44)](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_build/latest?definitionId=3) | **[NPM-Package](https://www.npmjs.com/package/snowflake44?activeTab=versions)** | **[NuGet-Package](https://www.nuget.org/packages/snowflake44)**

## Ussage

    long uid = Snowflake44.Generate();

    DateTime transactionTime = Snowflake44.DecodeDateTime(uid);

