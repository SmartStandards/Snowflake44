# Snowflake44 ID (UID64) & EncodedToken ID

[<img src="https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_apis/build/status/Snowflake44">](<https://dev.azure.com/SmartOpenSource/Smart%20Standards%20(Allgemein)/_build/latest?definitionId=3>) • **[NPM-Package](https://www.npmjs.com/package/snowflake44?activeTab=versions)** • **[NuGet-Package](https://www.nuget.org/packages/snowflake44)** • [Change Log](./vers/changelog.md)

This package contains:

- The **SnowFlake44** algorithm for creating UIDs (see below)
- The **EncodedToken** algorithm for creating numeric IDs from Strings (see below)
- The **PascalCasingUtil** for transforming separated tokens into PascalCase string (see [PascalCasingUtil Documentation](./doc/PascalCasingUtil.md))

## Snowflake44 - The better GUID

This is an algorithm to create a 64 bit integer ("Long", "BigInt") unique ID that can be used as SQL primary key, 
but is more compact than a GUID. It's actually a 44 bit UTC time stamp combined with 19 bit of random.
The topmost bit is not used in order to avoid negative values.

- Epoch is 1900..2456 (557 years)
- Precision is 1 millisecond
- Risk of collisions (when using parallel ID generators): 1:500.000 (within the same millisecond)

## EncodedToken - The Human Readable Integer ID ("HelloWorld" <=> 4946143410008232)

Converts a string ("Token") into a 64 bit integer ID and back. 
The intended use is to make human readable (technical, stable) names (such as enum element names) directly usable as SQL primary keys.

 😊 no need to use string type (ID) columns\
 😊 no need to maintain a lookup table to resolve IDs to text representation\

A "Token" string must follow **specific conventions** (see below)!

# Get Started

For dotnet, install the **Snowflake44 by SmartStandards Nuget Package**.
Then you can write:

    // C# for using Snowflake44:

    long uid = Snowflake44.Generate();

    DateTime transactionTime = Snowflake44.DecodeDateTime(uid); // extract the (UTC) time stamp from the ID
    
    // C# for using encoded token:

    long id = TokenEncoder.Encode("ExampleTenant");

    string token = TokenEncoder.Decode(id);
    
For SQL, install the **fn_DecodeToken** from the sources here.
Then you can write:

    SELECT id, fn_DecodeToken(id) AS 'DecodedId'

# EncodedToken Conventions

- Allowed characters: Letters, german umlauts (äöüß), dot. No digits, no spaces, no underscores, nothing else.
- True PascalCase (first letter uppercase)
- Max. 12 characters (each uppercase character occupies 2 places).
- If used as (primary) key: It's value must be stable forever (never to be renamed).

## Recommended Practices

- Never persist a token, never pass it as argument. Use the integer representation.
- In a human readable context, a token should be put into square brackets (e.g. [HelloWorld]), just to look different from regular (friendly) names. 
- When using tokens as technical names (file names, database, etc.), do not put them into square brackets.

# EncodedToken Internals

To convert a string into Int64, an "inverted" Base32 algorithm is used: Not the Payload (bytes) are encoded to a string,
but the other way round - the string is encoded into 8 Bytes which then represent the int64 value. 
To get 12 chars into 8 bytes, bit packing is used. This reduces the alphabet to 32 possible chars (5 bits per char).
This representation is called "raw encoded token", because it doesn't support PascalCase.

## Example

    Bits    [ 4][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ][ 5 ] 
    Index   0000BBBBBAAAAA99999888887777766666555554444433333222221111100000 
    Example               [ 20][ 12][ 5 ][ 23][ 0 ][ 15][ 12][ 12][ 1 ][ 8 ]
                          [ T ][ L ][ E ][ W ][ _ ][ O ][ L ][ L ][ A ][ H ]

## Bit Sematics

| #      | Count |       Semantic |
|--------|-------|----------------|
|     63 |     1 |         unused |
| 62..60 |     3 |       CodePage |
| 59..55 |     5 |  Last Char (B) |
|     …  |       |                |
|   4..0 |     5 | First Char (0) |

## Alphabet (CodePage 0 of 8 possible)

| Value | Symbol | 
|-------|--------|
|     0 |      _ |
| 1..26 |   A..Z |
|    27 |      Ä | 
|    28 |      Ö | 
|    29 |      Ü | 
|    30 |      ß | 
|    31 |      . |

## String End Convention 

In order not to loose one (valuable) character of the alphabet, the value 0 is mapped to "__" (underscore) instead of being ignored.
This causes some implications for the decoding behaviour of the underscore char (what you decode is not 1:1 what you encoded).
The decoded string will always have a length of 12 chars, padded to the right with underscores. To regain the original string,
we need to trim from the right, therefore it's not possible to have trailing underscores in the original string.

### Illustration

|     Original |        Encoded |      Decoded | Remarks                                                             |
|--------------|----------------|--------------|---------------------------------------------------------------------|
|`      "HALLO" `|` "HALLO_______" `|`      "HALLO" `|                                                                     |
|`     "HALLO_" `|` "HALLO_______" `|`      "HALLO" `| Trailing underscores get lost after decoding.                       |
|`     "_HALLO" `|` "_HALLO______" `|`     "_HALLO" `| Leading underscores are preserved.                                  |
|` "HALLO_WELT" `|` "HALLO_WELT__" `|` "HALLO_WELT" `| Inner underscores are preserved.                                    |
|` (Leerstring) `|` "____________" `|`           "" `| An empty string and 12 underscores are equally encoded...           | 
|`         "__" `|` "____________" `|`           "" `| ...therefore an underscore-only string will be decoded as empty.    | 

## PascalCase Convention 
  
To represent seperate words in a raw encoded token, you would have to use underscores as delimiter ("HELLO_WORLD") and you loose casing.
Therefore another convention is put on top of the "raw encoded token" standard:

- Original Token:
  - Must be PascalCase (first char uppercase)
  - Must not contain underscores or spaces

- Before encoding (pre processor): 
    - Insert an underscore before each uppercase char except the first one
        - Consequence: Uppercase chars (except the first one) occupy two places and reduce the possible length of a token

- After decoding (post processor): 
    - The first decoded char is always uppercase
    - Everything is lowercase by default, unless it's prefixed with an underscore
    - Underscores are removed

### Illustration

|     Original |   Decoded Raw |   Pascalized | Remarks                                                            |
|--------------|---------------|--------------|--------------------------------------------------------------------|
|`      "Hello" `|`       "HELLO" `|`      "Hello" `|                                                                    |
|`      "hello" `|`       "HELLO" `|`      "Hello" `| Original not PascalCase, 1st char became uppercase afted decoding. |
|` "HelloWorld" `|` "HELLO_WORLD" `|` "HelloWorld" `|                                                                    |
|` "helloWorld" `|` "HELLO_WORLD" `|` "HelloWorld" `| Original not PascalCase, 1st char became uppercase afted decoding. |
|`         "Id" `|`          "ID" `|`         "Id" `|                                                                    |
|`         "ID" `|`         "I_D" `|`         "ID" `|                                                                    |
|`       "Gmbh" `|`        "GMBH" `|`       "Gmbh" `|                                                                    |
|`       "GmbH" `|`         GMB_H `|`       "GmbH" `|                                                                    |
|`       "GMBH" `|`     "G_M_B_H" `|`       "GMBH" `| Original completely uppercase, halving the maximum length          |
