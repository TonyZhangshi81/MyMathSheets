// 球門數組
var __goalsArray = new Array();
var __goalsCoordinateArray = new Array();
var __goalsHomeScoreArray = new Array();
var __goalsAwayScoreArray = new Array();
// 放置範圍上下限值(Left邊距上下限)
var __containerLeftUp = 0;
var __containerLeftDown = 0;
// 放置範圍上下限值(Top邊距上下限)
var __containerTopUp = 0;
var __containerTopDown = 0;
// 放置範圍上下限值的控件Id
var __goalsArrayHiddenControlId = '';

var MathSheets = MathSheets || {};
MathSheets.ScoreGoal = MathSheets.ScoreGoal || (function () {

	// 球門號數組化
	_goalsArrayInit = function () {
		// 球門號設置場所(eg:0,1,1,1,0,0,0 => [0,1,1,1,0,0,0])
		var list = ($('#' + __goalsArrayHiddenControlId).val() || "").split(',');
		$.each(list, function (index, value) {
			__goalsArray.push(value);
		});
	},

		// 设定页面所有输入域为可用状态(射門得分)
		ready = function (divBallPrefix, divGoalerPrefix) {
			// 座位號數組化
			_goalsArrayInit();

			// 可拖動對象的大小信息
			var divDrag = $('#' + divBallPrefix + "0");
			var divDragWidth = $(divDrag).width();
			var divDragHeight = $(divDrag).height();

			// 拖動對象的合理坐標範圍初期化設置
			_goalsUpperAndLowerLimitInit(divBallPrefix, divGoalerPrefix, divDragWidth, divDragHeight);
			// 球門坐標集合初期化
			_goalsCoordinateArrayLimitInit(divGoalerPrefix, divDragWidth, divDragHeight);
		},

		// 球門坐標集合初期化
		_goalsCoordinateArrayLimitInit = function (divGoalerPrefix, divDragWidth, divDragHeight) {
			$("div[id*='" + divGoalerPrefix + "']").each(function (index, element) {
				// 容器對象left上下限
				var leftup = $(element).position().left;
				var leftdown = leftup + ($(element).width() - divDragWidth);
				// 容器對象top上下限
				var topup = $(element).position().top;
				var topdown = $(element).height() + topup - divDragHeight;
				var coordinate = leftup + "," + leftdown + "," + topup + "," + topdown + "," + index;
				__goalsCoordinateArray.push(coordinate);

				// data-toggle="tooltip" title="默認的 Tooltip"
				$(element).attr("data-toggle", "tooltip");
				$(element).attr("title", coordinate);
			});
		},

		// 拖動對象的合理坐標範圍初期化設置
		_goalsUpperAndLowerLimitInit = function (divBallPrefix, divGoalerPrefix, divDragWidth, divDragHeight) {
			// 遍歷所有容器座位編號并製作範圍上下限值
			$.each(__goalsArray, function (index, value) {
				// 容器對象
				var element = $("div[id*='" + divGoalerPrefix + value + "']");
				// 容器對象left上下限
				var leftup = $(element).position().left;
				var leftdown = leftup + ($(element).width() - divDragWidth);
				// 容器對象top上下限
				var topup = $(element).position().top;
				var topdown = $(element).height() + topup - divDragHeight;
				// 將上下限值放於divBallDragPrefixXXInput控件中(eg:300,330,20,50,3[真確座位號->用於盤錯提示時能夠找到正確的座位號])
				$("#" + divBallPrefix + index + "Input").val(leftup + "," + leftdown + "," + topup + "," + topdown + "," + value);
				// 拖動有效
				$("#" + divBallPrefix + index).draggable();
			});
		},

		// 答题验证(正确:true  错误:false)
		_goalsLinkageCorrecting = function (index, element) {
			var result = $("#" + $(element).attr('id') + "Result").val();
			var seat = parseInt((($("#" + $(element).attr('id') + "Input").val() || "").split(','))[4]);
			// 验证输入值是否与答案一致
			if (result == 'OK') {
				// 正确:true
				return true;
			} else {
				// 错误:false
				return false;
			}
		},

		// 當控件拖動停止時觸發的事件
		onStopDrag = function (e) {
			// 當前控件對象的配置參數
			var data = e.data;
			// 放置範圍上下限值取得
			var array = ($("#" + $(this).attr('id') + "Input").val() || "").split(',');
			// 放置範圍上下限值(Left邊距上下限)
			__containerLeftUp = parseInt(array[0]);
			__containerLeftDown = parseInt(array[1]);
			// 放置範圍上下限值(Top邊距上下限)
			__containerTopUp = parseInt(array[2]);
			__containerTopDown = parseInt(array[3]);
			// 如果拖放對象的相對位置在容器放置範圍上下限內
			if (data.left >= __containerLeftUp && data.left <= __containerLeftDown && data.top >= __containerTopUp && data.top <= __containerTopDown) {
				$("#" + $(this).attr('id') + "Result").val('OK');
			} else {
				$("#" + $(this).attr('id') + "Result").val('ERROR');
			}

			// data-toggle="tooltip" title="默認的 Tooltip"
			$(this).attr("data-toggle", "tooltip");
			$(this).attr("title", data.left + "," + data.top);

			_setScore($(this).attr('id'), data.left, data.top);
		},

		_setScore = function (ballId, left, top) {
			var seat = -1;
			$.each(__goalsCoordinateArray, function (index, value) {
				var coordinates = (value || "").split(',');
				if (left >= parseInt(coordinates[0]) && left <= parseInt(coordinates[1]) && top >= parseInt(coordinates[2]) && top <= parseInt(coordinates[3])) {
					seat = parseInt(coordinates[4]);
				}
			});

			__goalsHomeScoreArray.remove(ballId);
			__goalsAwayScoreArray.remove(ballId);
			if (seat == 0) {
				__goalsHomeScoreArray.push(ballId);
			} else if (seat == 1) {
				__goalsAwayScoreArray.push(ballId);
			}

			$("#spanHomeScore").text(__goalsHomeScoreArray.length);
			$("#spanAwayScore").text(__goalsAwayScoreArray.length);
		},

		// 订正(射門得分)
		makeCorrections = function () {
			var fault = 0;
			$("div[id*='divBall']").each(function (index, element) {
				// 答题验证
				if (!_goalsLinkageCorrecting(index, element)) {
					// 答题错误时,错误件数加一
					fault++;
				}
			});

			// 验证输入值是否与答案一致
			if (fault == 0) {
				// 对错图片显示和隐藏
				$('#imgOKScoreGoal').show();
				$('#imgNoScoreGoal').hide();
			} else {
				// 对错图片显示和隐藏
				$('#imgOKScoreGoal').hide();
				$('#imgNoScoreGoal').show();
			}

			return fault;
		},

		// 射門得分交卷
		theirPapers = function () {
			var fault = 0;
			$("div[id*='divBall']").each(function (index, element) {
				// 答题验证
				if (!_goalsLinkageCorrecting(index, element)) {
					// 答题错误时,错误件数加一
					__isFault++;
					fault++;
				} else {
					__isRight++;
				}
			});

			// 验证输入值是否与答案一致
			if (fault == 0) {
				// 对错图片显示和隐藏
				$('#imgOKScoreGoal').show();
				$('#imgNoScoreGoal').hide();
			} else {
				// 对错图片显示和隐藏
				$('#imgOKScoreGoal').hide();
				$('#imgNoScoreGoal').show();
			}
		},

		// 當控件被拖動時觸發的事件
		onDrag = function (e) {
			// 當前控件對象的配置參數
			var data = e.data;
			if (data.left < 0) { data.left = 0 }
			if (data.top < 0) { data.top = 0 }
			if (data.left + $(data.target).outerWidth() > $(data.parent).width()) {
				data.left = $(data.parent).width() - $(data.target).outerWidth();
			}
			if (data.top + $(data.target).outerHeight() > $(data.parent).height()) {
				data.top = $(data.parent).height() - $(data.target).outerHeight();
			}
		},

		Array.prototype.indexOf = function (val) {
			for (var i = 0; i < this.length; i++) {
				if (this[i] == val) return i;
			}
			return -1;
		},

		Array.prototype.remove = function (val) {
			var index = this.indexOf(val);
			if (index > -1) {
				this.splice(index, 1);
			}
		};

	return {
		onDrag: onDrag,
		ready: ready,
		onStopDrag: onStopDrag,
		makeCorrections: makeCorrections,
		theirPapers: theirPapers
	};
}());