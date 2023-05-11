
export function uid64(){
	return Uid64.generate();
}

export class Uid64 {
  
  private static _PreviousTimeFrame: number = 0;
  private static _RandomsOfCurrentTimeFrame = new Set();
  
  public static generate(): bigint {
    return Uid64.encodeDateTime(new Date());
  }
  
  private static encodeDateTime(incomingDate: any): bigint{
    var elapsedMilliseconds = (incomingDate.valueOf() + 2208988800000); // Date.UTC(1900, 0, 1).valueOf() always returns -2208988800000;
    if (elapsedMilliseconds >= 17592186044416) { //  We don't want to use the 45th bit
      return BigInt(-1);
    }
  
    if (elapsedMilliseconds != Uid64._PreviousTimeFrame){
      Uid64._RandomsOfCurrentTimeFrame.clear();
    }
  
    Uid64._PreviousTimeFrame = elapsedMilliseconds;
  
    var id = BigInt(elapsedMilliseconds) << 19n;
  
    var randomValue = BigInt(0);
  
    do {
      randomValue = BigInt(Math.floor(Math.random() * 524287));
      if (! Uid64._RandomsOfCurrentTimeFrame.has(randomValue)){
        Uid64._RandomsOfCurrentTimeFrame.add(randomValue);
        break;
      }
    } while (true);
  
    id = (id | randomValue);
  
    return id;
  }
  
  public static decodeDateTime(uid64: bigint): Date {

    var uid64 = uid64 >> 19n;
  
    var elapsedMilliseconds = Number(uid64) - 2208988800000;
     
    var decodedDate: Date = new Date();
    decodedDate.setTime(elapsedMilliseconds);
    return decodedDate;
  }

}
