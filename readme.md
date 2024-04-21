# Snowflake44 ID (UID64) & EncodedToken ID

**Snowflake44** is an algorithm to create a 64 bit integer ("Long", "BigInt") unique ID that can be used as SQL primary key, 
but is more compact than a GUID. It's actually a 44 bit UTC time stamp combined with 19 bit of random.
The topmost bit is not used in order to avoid negative values.

- Epoch is 1900..2456 (557 years)
- Precision is 1 millisecond
- Risk of collisions (when using parallel ID generators): 1:500.000 (within the same millisecond)

**EncodedToken** can create a 64 bit integer ID from a (maximum 12 chars) string ("Token"). 
The intended use is to make human readable (technical) names (such as enum element names) directly usable as SQL primary keys.
The integer ID can easily be decoded back to string representation (also in SQL statements), so you actually get human readable
IDs without using string types.
A "Token" string must follow **specific conventions** (see below)!

## Usage

For dotnet, Install the **Snowflake44 by SmartStandards Nuget Package**.
Then you can write:

    long uid = Snowflake44.Generate();

    DateTime transactionTime = Snowflake44.DecodeDateTime(uid);
    
## Version History & Build

See [Change Log](./vers/changelog.md) for current version information.

[![Build status](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_apis/build/status/Snowflake44)](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_build/latest?definitionId=3) | **[NPM-Package](https://www.npmjs.com/package/snowflake44?activeTab=versions)** | **[NuGet-Package](https://www.nuget.org/packages/snowflake44)**

## EncodedToken Conventions

- Allowed characters: Letters, german umlauts (äöüß), 