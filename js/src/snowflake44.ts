
export function snowflake44(){
	return Snowflake44.generate();
}

export class Snowflake44 {
  
  private static _PreviousTimeFrame: number = 0;
  private static _RandomsOfCurrentTimeFrame = new Set();
  
  public static generate(): bigint {
    return Snowflake44.encodeDateTime(new Date());
  }
  
  private static encodeDateTime(incomingDate: any): bigint{
    var elapsedMilliseconds = (incomingDate.valueOf() + 2208988800000); // Date.UTC(1900, 0, 1).valueOf() always returns -2208988800000;
    if (elapsedMilliseconds >= 17592186044416) { //  We don't want to use the 45th bit
      return BigInt(-1);
    }
  
    if (elapsedMilliseconds != Snowflake44._PreviousTimeFrame){
      Snowflake44._RandomsOfCurrentTimeFrame.clear();
    }
  
    Snowflake44._PreviousTimeFrame = elapsedMilliseconds;
  
    var id = BigInt(elapsedMilliseconds) << 19n;
  
    var randomValue = BigInt(0);
  
    do {
      randomValue = BigInt(Math.floor(Math.random() * 524287));
      if (!Snowflake44._RandomsOfCurrentTimeFrame.has(randomValue)){
        Snowflake44._RandomsOfCurrentTimeFrame.add(randomValue);
        break;
      }
    } while (true);
  
    id = (id | randomValue);
  
    return id;
  }
  
  public static decodeDateTime(snowflake44id: bigint): Date {

    var snowflake44id = snowflake44id >> 19n;
  
    var elapsedMilliseconds = Number(snowflake44id) - 2208988800000;
     
    var decodedDate: Date = new Date();
    decodedDate.setTime(elapsedMilliseconds);
    return decodedDate;
  }

}
