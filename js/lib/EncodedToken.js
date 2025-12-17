function decodeToken(int64Value) {
  var rawToken = encodedTokenToRawToken(int64Value);
  var decodedToken = rawTokenToReadableToken(rawToken);
  return decodedToken;
}

function encodedTokenToRawToken(int64Value) {

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
      unmappedCode = 196; // Ä (Windows-1252 aka ANSI)
    } else if (mappedCode == 28) {
      unmappedCode = 214; // Ö (Windows-1252 aka ANSI)
    } else if (mappedCode == 29) {
      unmappedCode = 220; // Ü (Windows-1252 aka ANSI)
    } else if (mappedCode == 30) {
      unmappedCode = 223; // ß (Windows-1252 aka ANSI)
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

function rawTokenToReadableToken(rawToken) {

  if (rawToken == null) return null;

  if (rawToken == "") return "";

  var pascalCaseString = "";

  var nextOneUp = true; // Der Erste immer Groß
  var withinDigits = false;

  for (var i = 0; i <= (rawToken.length - 1); i++) {

    var c = rawToken[i];

    var isLetter = (c.toLowerCase() != c.toUpperCase());

    if (!isLetter) {
      nextOneUp = true;
      withinDigits = (c == '#');
      continue;
    }

    if (withinDigits) {
      if (c == 'I') { pascalCaseString += '1'; continue; }
      if (c == 'Z') { pascalCaseString += '2'; continue; }
      if (c == 'E') { pascalCaseString += '3'; continue; }
      if (c == 'H') { pascalCaseString += '4'; continue; }
      if (c == 'S') { pascalCaseString += '5'; continue; }
      if (c == 'G') { pascalCaseString += '6'; continue; }
      if (c == 'L') { pascalCaseString += '7'; continue; }
      if (c == 'B') { pascalCaseString += '8'; continue; }
      if (c == 'P') { pascalCaseString += '9'; continue; }
      if (c == 'O') { pascalCaseString += '0'; continue; }
      // This would be an unexpected letter as Digit surrogate. We are robust and just continue => print the original letter.
      withinDigits = false;
    }

    if (nextOneUp) {
      if (c != 'ß') c = c.toUpperCase(); // JavaScript thinks that "ß" should be capitalized to "SS" - we don't want this.
    } else {
      c = c.toLowerCase();
    }
    pascalCaseString += c;
    nextOneUp = false;
  }
  return pascalCaseString;
}
