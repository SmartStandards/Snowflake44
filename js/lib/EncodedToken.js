function decodeToken(int64Value) {
  var rawToken = encodedTokenToRawToken(int64Value);
  var decodedToken = rawTokenToPascalCase(rawToken);
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
      unmappedCode = 46; // .
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

function rawTokenToPascalCase(rawString) {

  if (rawString == null) return null;

  if (rawString == "") return "";

  var pascalCaseString = "";
  var nextOneUp = true; // Der Erste immer Groß

  for (var i = 0; i <= (rawString.length - 1); i++) {

    var c = rawString[i];

    var isLetter = (c.toLowerCase() != c.toUpperCase());

    if (!isLetter) {
      nextOneUp = true;
      continue;
    }

    if (nextOneUp) {
      c = c.toUpperCase();
    } else {
      c = c.toLowerCase();
    }
    pascalCaseString += c;
    nextOneUp = false;
  }
  return pascalCaseString;
}
