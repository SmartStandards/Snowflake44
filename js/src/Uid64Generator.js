_PreviousTimeFrame = 0;
_RandomsOfCurrentTimeFrame = new Set();

function generateUid(){
	return encodeDateTime(new Date());
}

function encodeDateTime(incomingDate){
	var elapsedMilliseconds = (incomingDate.valueOf() + 2208988800000); // Date.UTC(1900, 0, 1).valueOf() always returns -2208988800000;
	if (elapsedMilliseconds >= 17592186044416) { //  We don't want to use the 45th bit
    return -1;
	}

	if (elapsedMilliseconds != _PreviousTimeFrame){
		_RandomsOfCurrentTimeFrame.clear();
	}

  _PreviousTimeFrame = elapsedMilliseconds;

	var id = BigInt(elapsedMilliseconds) << 19n;

	var randomValue = BigInt(0);

	do {
		randomValue = BigInt(Math.floor(Math.random() * 524287));
		if (! _RandomsOfCurrentTimeFrame.has(randomValue)){
			_RandomsOfCurrentTimeFrame.add(randomValue);
			break;
		}
	} while (true);

	id = (id | randomValue);

	return id;
}

function decodeDateTime(dateAsBigInt){

	var dateAsBigInt = dateAsBigInt >> 19n;

	var elapsedMilliseconds = Number(dateAsBigInt) - 2208988800000;
	 
	var decodedDate = new Date();
	decodedDate.setTime(elapsedMilliseconds);
	return decodedDate;
}