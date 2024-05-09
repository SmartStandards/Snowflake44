-- SmartStandards EncodedToken ID decoder
-- Limitation: Only decodes "raw encoded token" style (does not support PascalCase convention).
-- Taken from: https://github.com/SmartStandards/Snowflake44/tree/master

CREATE FUNCTION [dbo].[fn_DecodeToken] (@encoded bigint) RETURNS varchar(12) AS BEGIN
  
  DECLARE @unpackedByte AS bigint;
  DECLARE @decodedByte AS tinyint;
  DECLARE @bitmask AS bigint;
  DECLARE @rightShift AS bigint;
  DECLARE @decoded AS varchar(12);

  SET @bitmask = 31;
  SET @rightShift = 0;

  IF (@encoded <0) BEGIN
    SET @encoded = @encoded * -1;
  END

  DECLARE @base bigint = 2; -- Important to use bigint here, because this will define the return type of POWER()
  DECLARE @i int = 0;
  WHILE @i < 12 BEGIN

    SET @rightShift = POWER(@base,@i*5) -- 0, 32, 1024, 32768, 1048576, 33554432
    IF (@rightShift = 0) BEGIN
      SET @unpackedByte = @encoded & @bitmask;
    END ELSE BEGIN
      SET @unpackedByte = (@encoded & @bitmask) / @rightShift;
    END
            
    IF (@unpackedByte >= 1 AND @unpackedByte <= 26) BEGIN
      SET @decodedByte = @unpackedByte + 64;
    END ELSE BEGIN
      IF (@unpackedByte = 31) BEGIN
        SET @decodedByte = 46; -- .
      END ELSE BEGIN
        IF (@unpackedByte = 27) BEGIN
          SET @decodedByte = 196; -- Ä
        END ELSE BEGIN
          IF (@unpackedByte = 28) BEGIN
            SET @decodedByte = 214; -- Ö
          END ELSE BEGIN
            IF (@unpackedByte = 29) BEGIN
              SET @decodedByte = 220; -- Ü
            END ELSE BEGIN
              IF (@unpackedByte = 30) BEGIN
                SET @decodedByte = 223; -- ß
              END ELSE BEGIN
                SET @decodedByte = 32;
              END
            END
          END
        END
      END
    END
    
    SET @decoded = CONCAT(@decoded,CHAR(@decodedByte));
    
    IF (@i < 11) SET @bitmask = @bitmask * 32;
    SET @i = @i + 1;
  END;

  SET @decoded = LTRIM(RTRIM(@decoded));
  SET @decoded = REPLACE(@decoded,' ','_');

  RETURN @decoded;
END
