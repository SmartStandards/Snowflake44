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

For dotnet, install the **Snowflake44 by SmartStandards Nuget Package**.
Then you can write:

    // C# for using Snowflake44:

    long uid = Snowflake44.Generate();

    DateTime transactionTime = Snowflake44.DecodeDateTime(uid);
    
    // C# for using encoded token:

    long id = TokenEncoder.Encode("ExampleTenant");

    string token = TokenEncoder.Decode(id);
    
For SQL, install the **fn_DecodeToken** from the sources here.
Then you can write:

    SELECT id, fn_DecodeToken(id) AS 'DecodedId'

## Version History & Build

See [Change Log](./vers/changelog.md) for current version information.

[![Build status](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_apis/build/status/Snowflake44)](https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_build/latest?definitionId=3) | **[NPM-Package](https://www.npmjs.com/package/snowflake44?activeTab=versions)** | **[NuGet-Package](https://www.nuget.org/packages/snowflake44)**

## EncodedToken Conventions

- Allowed characters: Letters, german umlauts (äöüß), dot. No digits, no spaces, nothing else.
- True PascalCase (first letter uppercase)
- Max. 12 characters (each uppercase character occupies 2 places).

## EncodedToken Internals

To convert a string into Int64, an "inverted" Base32 algorithm is used: Not the Payload (bytes) are encoded to a string,
but the other way round - the string is encoded into 8 Bytes which then represent the int64 value. 
To get 12 chars into 8 bytes, bit packing is used. This reduces the alphabet to 32 possible chars (5 bits per char).

### Example

    Bits    [ 4][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ] 
    Index   0000BBBBBAAAAA99999888887777766666555554444433333222221111100000 
    Example               [ 20][ 12][ 5 ][ 23][ 0 ][ 15][ 12][ 12][ 1 ][ 8 ]
                          [ T ][ L ][ E ][ W ][ _ ][ O ][ L ][ L ][ A ][ H ]

### Bit Sematics

| #      | Count |       Semantic |
|--------|-------|----------------|
|     63 |     1 |         unused |
| 62..60 |     3 |       CodePage |
| 59..55 |     5 |  Last Char (B) |
|     …  |       |                |
|   4..0 |     5 | First Char (0) |

### Alphabet (CodePage 0 of 8 possible)

| Value | Symbol | 
|-------|--------|
|     0 |      _ |
| 1..26 |   A..Z |
|    27 |      Ä | 
|    28 |      Ö | 
|    29 |      Ü | 
|    30 |      ß | 
|    31 |      . |

### String End Convention 

Da der Wert 0 als Underscore codiert wird, ist die codierte String-Länge immer fix 12 Zeichen - es entsteht implizit ein Underscore-Padding-rechts. 

Underscores werden von rechts abgeschnitten ("trim"). 

#### Illustration

|     Original |        Encoded |      Decoded | Bemerkungen                                                         |
|--------------|----------------|--------------|---------------------------------------------------------------------|
|      "HALLO" | "HALLO_______" |      "HALLO" |                                                                     |
|     "HALLO_" | "HALLO_______" |      "HALLO" | Der am Ende stehende Underscore geht durch das Decodieren verloren. |
|     "_HALLO" | "_HALLO______" |     "_HALLO" | Als Prefix funktionieren Underscores.                               |
| "HALLO_WELT" | "HALLO_WELT__" | "HALLO_WELT" |                                                                     |
| (Leerstring) | "____________" |           "" |                                                                     | 
|          "_" | "____________" |           "" | Alleinstehende Underscores gehen verloren.                          |
|         "__" | "____________" |           "" | Alleinstehende Underscores gehen verloren.                          |

