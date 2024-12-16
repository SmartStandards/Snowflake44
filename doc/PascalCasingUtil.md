# PascalCasingUtil

This is an algorithm to convert a string of (space) separated Words to a PascalCase string (without spaces) and back.
The conversion is lossy.

# Get Started

    string pascalizedString = PascalCasingUtil.ToPascalCase("hello GmbH, the answer is: Value42!");

    // returns "HelloGmbhTheAnswerIsValue42"

    string unpascalizedString = PascalCasingUtil.SplitFromPascalCase("HelloGmbhTheAnswerIsValue42");
    
    // returns "Hello Gmbh The Answer Is Value42"
    
# The ToPascalCase Algorithm

- Read: Everything that is not a letter or a number will be interpreted as separator
- Read: Everything between separators is a token
- Write: First letter of a token is upper case, the rest is lower case 
  - (destroying the original casing of a token)
- Write: Tokens will be concatenated without any separator
  -  (dropping everything of the original string that is not a letter or a number)

# The SplitFromPascalCase Algorithm

- Insert a space before each upper case character, except the first one.
