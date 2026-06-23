-- SmartStandards EncodedToken ID decoder
-- Taken from: https://github.com/SmartStandards/Snowflake44/tree/master

CREATE FUNCTION [dbo].[fn_Pascalize] (@rawString varchar(max)) RETURNS varchar(max) AS BEGIN
  
  DECLARE @peek AS char(1);
  DECLARE @nextOneUp AS bit;  
  DECLARE @pascalized AS varchar(max);

  SET @nextOneUp = 1; -- Firt char alwas capital
    
  DECLARE @i int = 1; -- 1-based indexing!

  WHILE @i <= LEN(@rawString) BEGIN

    SET @peek = SUBSTRING(@rawString, @i, 1);

    IF (@peek = '_') BEGIN

      SET @nextOneUp = 1;

    END ELSE BEGIN 

      IF (@nextOneUp = 1) BEGIN
        SET @pascalized = CONCAT(@pascalized, UPPER(@peek));
      END ELSE BEGIN 
        SET @pascalized = CONCAT(@pascalized, LOWER(@peek));
      END
      
      SET @nextOneUp = 0;
    END    
    
    SET @i = @i + 1;
  END;
   
  RETURN @pascalized;
END
