// SNAPSVG.JS Clock Layout 

// CLOCK Timer
var updateTime = function (_clock, _hours, _minutes, _seconds, dateTime) {
	var currentTime, hour, minute, second;

	if (dateTime == '') {
		currentTime = new Date();
		second = currentTime.getSeconds();
		minute = currentTime.getMinutes();
		hour = currentTime.getHours();
	} else {
		var ary = (dateTime || '').split(':');
		second = ary[2];
		minute = ary[1];
		hour = ary[0];
	}

	hour = (hour > 12) ? hour - 12 : hour;
	hour = (hour == '00') ? 12 : hour;

	if (second == 0) {
		//got to 360deg at 60s
		second = 60;
	} else if (second == 1 && _seconds) {
		//reset rotation transform(going from 360 to 6 deg)
		_seconds.attr({ transform: "r" + 0 + "," + 80 + "," + 80 });
	}
	if (minute == 0) {
		minute = 60;
	} else if (minute == 1) {
		_minutes.attr({ transform: "r" + 0 + "," + 80 + "," + 80 });
	}

	_hours.animate({ transform: "r" + hour * 30 + "," + 80 + "," + 80 }, 200, mina.elastic);
	_minutes.animate({ transform: "r" + minute * 6 + "," + 80 + "," + 80 }, 200, mina.elastic);
	if (_seconds) {
		_seconds.animate({ transform: "r" + second * 6 + "," + 80 + "," + 80 }, 500, mina.elastic);
	}
}
var updateSeconds = function (_clock, _seconds) {
	var currentTime, second;
	currentTime = new Date();
	second = currentTime.getSeconds();

	if (second == 0) {
		//got to 360deg at 60s
		second = 60;
	} else if (second == 1 && _seconds) {
		//reset rotation transform(going from 360 to 6 deg)
		_seconds.attr({ transform: "r" + 0 + "," + 80 + "," + 80 });
	}
	if (_seconds) {
		_seconds.attr({ transform: "r" + second * 6 + "," + 80 + "," + 80 });
	}
}

openInterval = function () {
	setInterval(function () {
		//updateTime(clock1, hours1, minutes1, seconds1);
	}, 1000);
}

