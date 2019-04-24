
/**
 * @description 被選出的21張牌
 * @private
 * @var {Array} __pokers 21張牌容器
 */
var __pokers = new Array();

/**
 * @description 第一列牌（7張）
 * @private
 * @var {Array} __deal1 7張牌容器
 */
var __deal1 = new Array();

/**
 * @description 第二列牌（7張）
 * @private
 * @var {Array} __deal2 7張牌容器
 */
var __deal2 = new Array();

/**
 * @description 第三列牌（7張）
 * @private
 * @var {Array} __deal3 7張牌容器
 */
var __deal3 = new Array();

/**
 * @file 撲克牌猜牌遊戲
 * @description 遊戲規則：
 * 1，隨機選出21張牌，七張一列，分為三列
 * 2，從三列牌中隨機選定一張牌
 * 3，詢問選中的牌在哪一列，讓後將牌全部收起並重新發牌（七張一列，分為三列）
 * 4，重複上述步驟一次
 * 5，將牌收起，答案牌已經得到
 * @author TonyZhangshi <tonyzhangshi@163.com>
 * @version 0.1
 * @copyright Tony Zhangshi 2019
 */
var MathSheets = MathSheets || {};
MathSheets.GamesPoker = MathSheets.GamesPoker || (function () {

    /**
     * @description 隨機排序處理
     * @private
     * @param max {int} 隨機數上限值
     * @returns {int} 隨機數
     */
    this._randomSort = function () {
        return Math.random() > .5 ? -1 : 1;
    },

        /**
         * @description 牌面初期化設置
         * @method pokerInitialize
         */
        pokerInitialize = function () {
            if (__pokers.length == 0) {
                return;
            }
            __pokers.sort(_randomSort);
        },

        /**
         * @description 收牌
         * @method doClearUp
         * @param line {int} 想定的牌面所在列數
         */
        doClearUp = function (line) {
            var poker = "";
            switch (line) {
                case 1:
                    poker += __deal2.join(',');
                    poker += ',';
                    poker += __deal1.join(',');
                    poker += ',';
                    poker += __deal3.join(',');
                    break;
                case 2:
                    poker += __deal1.join(',');
                    poker += ',';
                    poker += __deal2.join(',');
                    poker += ',';
                    poker += __deal3.join(',');
                    break;
                default:
                    poker += __deal1.join(',');
                    poker += ',';
                    poker += __deal3.join(',');
                    poker += ',';
                    poker += __deal2.join(',');
                    break;
            }
            __pokers.length = 0;
            __pokers = poker.split(',');
        },

        /**
         * @description 答案揭曉
         * @method getResult
         */
        getResult = function () {
            return __pokers[(__pokers.length - 1) / 2];
        },

        /**
         * @description 發牌
         * @method doDeal
         */
        doDeal = function () {
            __deal1.length = 0;
            __deal2.length = 0;
            __deal3.length = 0;

            var j = 0;
            for (var i = 0; i < __pokers.length; i++) {
                if (j == 0) {
                    __deal1.push(__pokers[i]);
                    j++;
                } else if (j == 1) {
                    __deal2.push(__pokers[i]);
                    j++;
                } else {
                    __deal3.push(__pokers[i]);
                    j = 0;
                }
            }
        };

    return {
        pokerInitialize: pokerInitialize,
        doDeal: doDeal,
        doClearUp: doClearUp,
        getResult: getResult
    };
}());