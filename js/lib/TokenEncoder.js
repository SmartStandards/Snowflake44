// ENCODING OF THIS FILE IS NOT UTF-8, BUT ISO 8859-1 (aka Windows-1252)!

class TokenEncoder {

  static decode(int64Value) {
    var rawToken = TokenEncoder.int64ToRaw(int64Value);
    var decodedToken = TokenEncoder.rawToReadable(rawToken);
    return decodedToken;
  }

  static encode(token){
    var rawToken = TokenEncoder.readableToRaw(token);
    return TokenEncoder.rawToInt64(rawToken);
  }

  static rawToInt64(rawToken) {

    if (rawToken.length > 12) {
      throw new Error('Maximum length of 12 characters was exceeded by "' + rawToken + '"!');
    }

    if (rawToken.endsWith("_")) {
      throw new Error('String must not end with underscore: "' + rawToken + '"!');
    }

    var toEncode = TokenEncoder.toUpperCaseFixed(rawToken);

    if (toEncode.length < 12) toEncode = toEncode.padEnd(12, '_');

    var encodedValue = 0n; // the n makes it BigInt
    var offset = 0n;
    var mappedCode = 0n;

    for (var i = 0; i <= 11; i++) {

      mappedCode = BigInt(TokenEncoder.getMappedCode(toEncode, i));

      if (mappedCode > 31) {
        throw new Error('Unsupported Character "' + toEncode[i] + '" in "' + rawToken + '"!');
      }
      encodedValue |= (mappedCode << offset);
      offset += 5n;
    }
    return encodedValue;
  }

  static int64ToRaw(int64Value) {

    if ((int64Value < 0)) int64Value *= -1n;

    var bitmask = 31n; // the n makes it BigInt
    var offset = 0n;
    var mappedCode = 0n;
    var unmappedCode = 0n;
    var decodedChars = [' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '];
    var decodedString = "";

    for (var i = 0; i <= 11; i++) {
      mappedCode = ((int64Value & bitmask) >> offset);

      if (mappedCode >= 1 && mappedCode <= 26) {
        unmappedCode = (mappedCode + 64n);
      } else if (mappedCode == 27) {
        unmappedCode = 196; // ƒ (ISO 8859-1 aka Windows-1252)
      } else if (mappedCode == 28) {
        unmappedCode = 214; // ÷ (ISO 8859-1 aka Windows-1252)
      } else if (mappedCode == 29) {
        unmappedCode = 220; // ‹ (ISO 8859-1 aka Windows-1252)
      } else if (mappedCode == 30) {
        unmappedCode = 223; // ﬂ (ISO 8859-1 aka Windows-1252)
      } else if (mappedCode == 31) {
        unmappedCode = 35; // #
      } else { 
        unmappedCode = 32; // Space
      }

      //decodedString += String.fromCharCode(Number(unmappedCode)); // concatenation is faster than array push!
      decodedChars[i] = String.fromCharCode(Number(unmappedCode)); // hopefully this is even faster

      bitmask <<= 5n;
      offset += 5n;
    }

    //decodedString = decodedString.trimEnd().replaceAll(' ','_');
    decodedString = decodedChars.join('').trimEnd().replaceAll(' ','_');

    return decodedString;
  }

  static readableToRaw(token) {

    if ((token) == null) return null;

    if ((token == "")) return "";

    var digitSurrogate = ' ';
    var escapedChar = ' ';
    var withinEscapeSequence = false;
    var upperCaseExpected = true;

    var sb = "";

    for (var i = 0; i < (token.length); i++) {

      var c = token[i];

      digitSurrogate = ' ';
      escapedChar = ' ';

      if (c == '1') { digitSurrogate = 'I'; }
      if (c == '2') { digitSurrogate = 'Z'; }
      if (c == '3') { digitSurrogate = 'E'; }
      if (c == '4') { digitSurrogate = 'H'; }
      if (c == '5') { digitSurrogate = 'S'; }
      if (c == '6') { digitSurrogate = 'G'; }
      if (c == '7') { digitSurrogate = 'L'; }
      if (c == '8') { digitSurrogate = 'B'; }
      if (c == '9') { digitSurrogate = 'P'; }
      if (c == '0') { digitSurrogate = 'O'; }

      if (c == '#') { escapedChar = '#'; }
      if (c == '+') { escapedChar = 'A'; }
      if (c == ',') { escapedChar = 'C'; }
      if (c == '.') { escapedChar = 'D'; }
      if (c == '?') { escapedChar = 'F'; }
      if (c == '(') { escapedChar = 'J'; }
      if (c == ')') { escapedChar = 'K'; }
      if (c == '-') { escapedChar = 'M'; }
      if (c == '&') { escapedChar = 'N'; }
      if (c == '@') { escapedChar = 'Q'; }
      if (c == ':') { escapedChar = 'R'; }
      if (c == '\'') { escapedChar = 'T'; }
      if (c == '\\') { escapedChar = 'U'; }
      if (c == '/') { escapedChar = 'V'; }
      if (c == '=') { escapedChar = 'W'; }
      if (c == '*') { escapedChar = 'X'; }
      if (c == '!') { escapedChar = 'Y'; }
      if (c == ']') { escapedChar = 'ƒ'; }
      if (c == '[') { escapedChar = '÷'; }
      if (c == '%') { escapedChar = '‹'; }
      if (c == '^') { escapedChar = 'ﬂ'; }

      // Terminate escaping state

      if (withinEscapeSequence && digitSurrogate == ' ' && (escapedChar == ' ' || escapedChar == '_')) {
        withinEscapeSequence = false;
        sb += '_';
        upperCaseExpected = true;
      }

      if (digitSurrogate != ' ') {
        if (!withinEscapeSequence) sb += '#';
        withinEscapeSequence = true;
        sb += digitSurrogate;
        continue;
      }

      if (escapedChar != ' ') {
        if (!withinEscapeSequence) {
          sb += '#';
          upperCaseExpected = true;
        }
        sb += escapedChar;
        continue;
      }

      var upperC = TokenEncoder.toUpperCaseFixed(c);

      var mappedCode = BigInt(TokenEncoder.getMappedCode(upperC,0));

      if (mappedCode > 31) {
        throw new Error('Unsupported Character "' + c + '" in "' + token + '"!');
      }

      var isAlreadyUpper = (c == upperC);

      if (isAlreadyUpper != upperCaseExpected) sb += '_';

      sb += upperC;

      upperCaseExpected = false;
    } // next

    return sb;
  }

  static rawToReadable(rawToken) {

    if (rawToken == null) return null;

    if (rawToken == "") return "";

    var pascalCaseString = "";

    var nextOneUp = true; // Der Erste immer Groﬂ
    var escaping = false;
    var withinDigits = false;

    for (var i = 0; i <= (rawToken.length - 1); i++) {

      var c = rawToken[i];

      if (withinDigits) {
        if (c == '_') {
          withinDigits = false;
          escaping = false;
          nextOneUp = true;
          continue;
        }
      }

      if (escaping) {
        if (c == 'I') { pascalCaseString += '1'; withinDigits = true; continue; }
        if (c == 'Z') { pascalCaseString += '2'; withinDigits = true; continue; }
        if (c == 'E') { pascalCaseString += '3'; withinDigits = true; continue; }
        if (c == 'H') { pascalCaseString += '4'; withinDigits = true; continue; }
        if (c == 'S') { pascalCaseString += '5'; withinDigits = true; continue; }
        if (c == 'G') { pascalCaseString += '6'; withinDigits = true; continue; }
        if (c == 'L') { pascalCaseString += '7'; withinDigits = true; continue; }
        if (c == 'B') { pascalCaseString += '8'; withinDigits = true; continue; }
        if (c == 'P') { pascalCaseString += '9'; withinDigits = true; continue; }
        if (c == 'O') { pascalCaseString += '0'; withinDigits = true; continue; }

        if (c == '#') { pascalCaseString += '#'; }
        if (c == 'A') { pascalCaseString += '+'; }
        if (c == 'C') { pascalCaseString += ','; }
        if (c == 'D') { pascalCaseString += '.'; }
        if (c == 'F') { pascalCaseString += '?'; }
        if (c == 'J') { pascalCaseString += '('; }
        if (c == 'K') { pascalCaseString += ')'; }
        if (c == 'M') { pascalCaseString += '-'; }
        if (c == 'N') { pascalCaseString += '&'; }
        if (c == 'Q') { pascalCaseString += '@'; }
        if (c == 'R') { pascalCaseString += ':'; }
        if (c == 'T') { pascalCaseString += '\''; }
        if (c == 'U') { pascalCaseString += '\\'; }
        if (c == 'V') { pascalCaseString += '/'; }
        if (c == 'W') { pascalCaseString += '='; }
        if (c == 'X') { pascalCaseString += '*'; }
        if (c == 'Y') { pascalCaseString += '!'; }
        if (c == 'ƒ') { pascalCaseString += ']'; }
        if (c == '÷') { pascalCaseString += '['; }
        if (c == '‹') { pascalCaseString += '%'; }
        if (c == 'ﬂ') { pascalCaseString += '^'; }

        if (!withinDigits) {
          escaping = false;
          nextOneUp = true;
        }
        continue;
      }

      if (c == '#') {
        escaping = true;
        continue;
      }

      if (c == '_') {
        nextOneUp = !nextOneUp;
        continue;
      }

      if (nextOneUp) {
        c = TokenEncoder.toUpperCaseFixed(c);
      } else {
        c = c.toLowerCase();
      }
      pascalCaseString += c;
      nextOneUp = false;
    }
    return pascalCaseString;
  }

  static toUpperCaseFixed(incoming){
    var outgoing = "";
    for (var i = 0; i <= (incoming.length - 1); i++) {
      var c = incoming[i];
      if (c != 'ﬂ') c = c.toUpperCase(); // JavaScript thinks that "ﬂ" should be capitalized to "SS" - we don't want this.
      outgoing += c;
    }
    return outgoing;
  }

  static getMappedCode(rawToken, i) {

    var unmappedCode = rawToken.charCodeAt(i);

    if (unmappedCode == 95) { // _
      return 0n;
    } else if (65 <= unmappedCode && unmappedCode <= 90) { // A-Z
      return (unmappedCode - 64);
    } else if (unmappedCode == 35) { // #
      return 31;
    } else if (unmappedCode == 196) { // ƒ,‰
      return 27;
    } else if (unmappedCode == 214) { // ÷,ˆ
      return 28;
    } else if (unmappedCode == 220) { // ‹,¸
      return 29;
    } else if (unmappedCode == 223) { // ﬂ
      return 30;
    } else {
      return 32;
    }
  }
}