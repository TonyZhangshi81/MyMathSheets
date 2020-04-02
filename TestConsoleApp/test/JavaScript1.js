
download: function(url, params, fileName) {
    var len, i = 0,
        temp, input, form, key;

    //处理参数
    var urlParams = [];
    if (isA(params)) {
        for (len = params.length; i < len; i++) {
            temp = params[i];
            urlParams.push(temp.key + "=" + temp.value);
        }
    } else if (isO(params)) {
        for (key in params) {
            temp = params[key];
            urlParams.push(key + "=" + temp);
        }
    } else if (isS(params)) {
        params = Utilities.parseQuery(params);
        for (key in params) {
            temp = params[key];
            urlParams.push(key + "=" + temp);
        }
    }

    //显示等待
    Dialogs.showWait('正在下载中，请稍候...');

    var xmlRequest = new XMLHttpRequest();
    xmlRequest.open("POST", url, true);
    xmlRequest.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlRequest.responseType = "blob";
    xmlRequest.onload = function (oEvent) {
        if ((xmlRequest.status >= 200 && xmlRequest.status < 300) || xmlRequest.status === 304) {
            if (!fileName) {
                //从header中获取
                fileName = decodeURI(xmlRequest.getResponseHeader('filename'));
            }

            console.log(fileName);

            //校验是否下载参数
            var content = xmlRequest.response;
            if (!fileName || fileName === 'null') {
                var myReader = new FileReader();
                myReader.addEventListener("loadend", function (e) {
                    var msg = e.srcElement.result;
                    Dialogs.showWarn(msg);
                });
                myReader.readAsText(content);
                return;
            }

            //数据转换为文件下载
            var elink = document.createElement('a');
            elink.download = fileName || 'demo.xlsx';
            elink.style.display = 'none';
            var blob = new Blob([content]);
            elink.href = URL.createObjectURL(blob);
            document.body.appendChild(elink);
            elink.click();
            document.body.removeChild(elink);

            //关闭等待
            Dialogs.hide();
        } else {
            Dialogs.showWarn('下载失败');
        }
    };

    try {
        //发送参数字符串， 但是formData需要后台支持
        xmlRequest.send(urlParams.join('&'));
    } catch (e) {
        Dialogs.showWarn('发送下载请求失败');
        console.error('发送失败', e);
    }

}