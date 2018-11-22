
var MathSheets = MathSheets || {};

MathSheets.SchoolClock = MathSheets.SchoolClock || (function () {

	// 打印設置
	printSetting = function () {

	},

		// 打印后頁面設定
		printAfterSetting = function () {

		},

		// 答题验证(正确:true  错误:false)
		_schoolClockCorrecting = function (index, element) {
			// 验证输入值是否与答案一致
			if ($(element).val() == $('#hiddenAc' + index).val()
				|| (parseInt($('#hiddenAc' + index).val()) == -999 && $(element).val() != '')) {
				// 对错图片显示和隐藏
				$('#imgOKSchoolClock' + index).show();
				$('#imgNoSchoolClock' + index).hide();
				$(element).attr("disabled", "disabled");
				// 正确:true
				return true;
			} else {
				// 对错图片显示和隐藏
				$('#imgOKSchoolClock' + index).hide();
				$('#imgNoSchoolClock' + index).show();
				// 错误:false
				return false;
			}
		},

		// 设定页面所有输入域为可用状态(四则运算)
		ready = function () {
			var aryClocksAnswer = new Array();
			aryClocksAnswer = ($('#hidClocksAnswer').val() || '').split(';');


			$("svg[class='clock']").each(function (index, element) {
				var clock = Snap('#' + $(element).attr('id'));
				var hours = clock.rect(79, 35, 3, 55).attr({ fill: "#282828", transform: "r" + 10 * 30 + "," + 80 + "," + 80 });
				var minutes = clock.rect(79, 20, 3, 70).attr({ fill: "#535353", transform: "r" + 10 * 6 + "," + 80 + "," + 80 });
				var seconds = clock.rect(80, 10, 1, 80).attr({ fill: "#ff6400" });
				var middle = clock.circle(81, 80, 3).attr({ fill: "#535353" });

				_updateTime(hours, minutes, seconds, aryClocksAnswer[index]);
			});
		},

		_updateTime = function (clockHours, clockMinutes, clockSeconds, dateTime) {
			var currentTime, hour, minute, second;

			if (dateTime == '') {
				// 取系統時間
				currentTime = new Date();
				second = currentTime.getSeconds();
				minute = currentTime.getMinutes();
				hour = currentTime.getHours();
			} else {
				// 指定時間
				var ary = (dateTime || '').split(':');
				second = parseInt(ary[2]);
				minute = parseInt(ary[1]);
				hour = parseInt(ary[0]);
			}

			// 顯示12小時制
			if (hour > 12) {
				hour = hour - 12;
			}

			// 6度表示一分鐘
			var minangle = minute * 6;
			// 30度表示一小時
			var hourangle = (hour + (minute / 60.0)) * 30;
			// 6度表示一秒鐘
			var secrangel = second * 6;

			clockHours.animate({ transform: "r" + hourangle + "," + 80 + "," + 80 }, 200, mina.elastic);
			clockMinutes.animate({ transform: "r" + minangle + "," + 80 + "," + 80 }, 200, mina.elastic);
			if (clockSeconds) {
				clockSeconds.animate({ transform: "r" + secrangel + "," + 80 + "," + 80 }, 500, mina.elastic);
			}
		},

		// 订正(時鐘學習板)
		makeCorrections = function () {
			var fault = 0;
			$("input[id*='inputAc']").each(function (index, element) {
				// 答题验证
				if (!_schoolClockCorrecting(index, element)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});
			return fault;
		},

		// 四则运算交卷
		theirPapers = function () {
			$("input[id*='inputAc']").each(function (index, element) {
				// 答题验证
				if (!_schoolClockCorrecting(index, element)) {
					// 答题错误时,错误件数加一
					__isFault++;
				} else {
					__isRight++;
				}
			});
		};

	return {
		printSetting: printSetting,
		printAfterSetting: printAfterSetting,
		ready: ready,
		makeCorrections: makeCorrections,
		theirPapers: theirPapers
	};
}());
