$(function () {
    console.log(navigator.userAgent);

    var os = function () {
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
    }();

    $(window).on("orientationchange", function (event) {
        console.log(window.orientation);
    });
});