/**
 * @description 當前設備的方向
 * @var {int} __orientation (豎屏:landscape 橫屏:portrait)
 * @default ""
 */
var __orientation;

/**
 * @file 提供系統環境相關的參數
 * @description 作為其他JS模塊的通用處理，提供統一的功能實現
 * @author TonyZhangshi <tonyzhangshi@163.com>
 * @version 0.1
 * @copyright Tony Zhangshi 2020
 */

var MathSheets = MathSheets || {};
MathSheets.System = MathSheets.System || (function () {

    /**
    * @description 系統環境參數
    * @method os
    * @return {object} 
    */
    os = function () {
        var ua = navigator.userAgent,
            isWindowsPhone = /(?:Windows Phone)/.test(ua),
            isSymbian = /(?:SymbianOS)/.test(ua) || isWindowsPhone,
            isAndroid = /(?:Android)/.test(ua),
            isFireFox = /(?:Firefox)/.test(ua),
            isChrome = /(?:Chrome|CriOS)/.test(ua),
            isTablet = /(?:iPad|PlayBook)/.test(ua) || (isAndroid && !/(?:Mobile)/.test(ua)) || (isFireFox && /(?:Tablet)/.test(ua)),
            isPhone = /(?:iPhone)/.test(ua) && !isTablet,
            isPc = !isPhone && !isAndroid && !isSymbian;
        return {
            isWindowsPhone: isWindowsPhone,
            isSymbian: isSymbian,
            isAndroid: isAndroid,
            isFireFox: isFireFox,
            isChrome: isChrome,
            isTablet: isTablet,
            isPhone: isPhone,
            isPc: isPc
        };
    };

    return {
        os: os
    };
}());

$(function () {
    console.log(navigator.userAgent);

    /**
     * @description 水平或者垂直翻轉設備（即方向發生變化）時觸發的事件
     * @param event {event} 事件對象
     */
    $(window).on("orientationchange", function (event) {
        if (window.orientation == 90 || window.orientation == -90) {
            // 豎屏
            __orientation = 'landscape';
        } else if (window.orientation == 0 || window.orientation == 180) {
            // 橫屏
            __orientation = 'portrait';
        }
        event.preventDefault();
    });
});